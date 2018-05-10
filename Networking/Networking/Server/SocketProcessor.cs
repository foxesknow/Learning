using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Collections.Concurrent;
using System.IO;

using Networking.Threading.Tasks;

namespace Networking.Server
{
    public abstract class SocketProcessor : IDisposable
    {
        private static long s_SystemID;
		
		private readonly long m_SystemID;

        private readonly object m_SyncRoot = new object();
        private long m_NextID;
        private long m_CorrelationID;

        private readonly ConcurrentDictionary<long, ClientData> m_Clients = new ConcurrentDictionary<long, ClientData>();
        private readonly StopSource m_StopSource = new StopSource();

        private TcpListener m_Listener;

        protected SocketProcessor()
        {
            m_SystemID = Interlocked.Increment(ref s_SystemID);
        }
        
        private Task m_SocketTask;

        public void Start(int port)
        {
            m_SocketTask = StartAsync(port);
        }

        public async Task Stop()
        {
            if(m_Listener != null)
            {
                m_StopSource.Stop();
                m_Listener.Stop();

                try
                {
                    await m_SocketTask.ConfigureAwait(false);
                }
                catch
                {
                    // Ignore any errors
                }
            }
        }

        private async Task StartAsync(int port)
        {
            try
            {
                if(m_Listener != null) return;

                m_Listener = new TcpListener(IPAddress.Any, port);
                m_Listener.Start();

                while(true)
                {
                    var tcpClient = await m_Listener.AcceptTcpClientAsync().ConfigureAwait(false);
                    Process(tcpClient);
                }
            }
            catch
            {
                if(m_StopSource.CancellationToken.IsCancellationRequested == false)
                {
                    // We've stopped unexpectidly
                    throw;
                }
            }
        }

        public bool IsStopping
        {
            get{return m_StopSource.CancellationToken.IsCancellationRequested;}
        }

        public async Task Reply(long id, MessageEnvelope template, byte[] buffer)
        {
            var envelope = new MessageEnvelope()
            {
                MessageSystemID = m_SystemID,
                MessageCorrelationID = AllocateCorrelationID(),
                ResponseSystemID = template.MessageSystemID,
                ResponseCorrelationID = template.MessageCorrelationID,
                SessionHigh = template.SessionHigh,
                SessionLow = template.SessionLow,
                DataLength = buffer.Length
            };

            if(m_Clients.TryGetValue(id, out var clientData))
            {
                using(var stream = new MemoryStream())
                {
                    using(var dataEncoder = new DataEncoder(stream))
                    {
                        dataEncoder.WriteNeverNull(envelope);
                    }

                    var position = stream.Position;
                    await clientData.Stream.WriteAsync(stream.GetBuffer(), 0, (int)position).ConfigureAwait(false);
                }

                await clientData.Stream.WriteAsync(buffer, 0, buffer.Length).ConfigureAwait(false);
            }
            else
            {
                throw new IOException("connection closed");
            }
        }

        private async void Process(TcpClient tcpClient)
        {
            var stopTask = m_StopSource.Task;

            var clientData = AllocateClient(tcpClient);

            try
            {
                await ReadEnvelope(clientData);
                HandleDisconnect(clientData.ID);
            }
            catch(Exception e)
            {
                if(m_StopSource.CancellationToken.IsCancellationRequested)
                {
                    HandleDisconnect(clientData.ID);
                }
                else
                {
                    HandleDisconnect(clientData.ID, e);
                }
            }
            finally
            {
                ReleaseClient(clientData);
            }
        }

        private async Task ReadEnvelope(ClientData clientData)
        {
            while(clientData.IsOpen != 0)
            {
                var envelopeBuffer = new byte[MessageEnvelope.EnvelopeSize];
                int offset = 0;
                int bytesRead = 0;

                while(bytesRead != MessageEnvelope.EnvelopeSize)
                {
                    var read = await clientData.Stream.ReadAsync(envelopeBuffer, offset, MessageEnvelope.EnvelopeSize - offset);

                    if(read == 0)
                    {
                        // The connection has closed
                        return;
                    }

                    bytesRead += read;
                }

                using(var stream = new MemoryStream(envelopeBuffer))
                using(var decoder = new DataDecoder(stream))
                {
                    var messageEnvelope = new MessageEnvelope(decoder);

                    var bodyRead = await ReadBody(clientData, messageEnvelope);
                    if(bodyRead == false)
                    {
                        return;
                    }
                }
            }
        }

        private async Task<bool> ReadBody(ClientData clientData, MessageEnvelope messageEnvelope)
        {
            var dataBuffer = new byte[messageEnvelope.DataLength];
            int offset = 0;
            int bytesRead = 0;

            while(bytesRead != dataBuffer.Length)
            {
                var read = await clientData.Stream.ReadAsync(dataBuffer, offset, dataBuffer.Length - offset);

                if(read == 0)
                {
                    // The connection has closed
                    return false;
                }

                bytesRead += read;
            }

            HandleMessage(clientData.ID, messageEnvelope, dataBuffer);

            return true;
        }

        private ClientData AllocateClient(TcpClient tcpClient)
        {
            var id = Interlocked.Increment(ref m_NextID);

            var clientData = new ClientData(id, tcpClient);

            m_Clients.GetOrAdd(id, clientData);
            return clientData;
        }

        private void ReleaseClient(ClientData clientData)
        {
            ReleaseClient(clientData.ID);
        }

        private void ReleaseClient(long id)
        {
            if(m_Clients.TryRemove(id, out var clientData))
            {
                clientData.Dispose();
            }
        }

        protected long AllocateCorrelationID()
		{
			return Interlocked.Increment(ref m_CorrelationID);
		}

        protected abstract void HandleMessage(long id, MessageEnvelope messageEnvelope, byte[] data);

        protected abstract void HandleDisconnect(long id);

        protected abstract void HandleDisconnect(long id, Exception exception);

        public void Dispose()
        {
            var stopTask = Stop();
            stopTask.Wait();

            m_StopSource.Dispose();
        }

        class ClientData : IDisposable
        {
            public ClientData(long id, TcpClient tcpClient)
            {
                ID = id;
                TcpClient = tcpClient;
                Stream = tcpClient.GetStream();
                IsOpen = 1;
            }

            public long ID;
            public TcpClient TcpClient;
            public Stream Stream;
            public long IsOpen;

            public void Dispose()
            {
                if(Interlocked.Exchange(ref IsOpen, 0) == 1)
                {
                    Stream.Dispose();
                }
            }
        }
    }
}
