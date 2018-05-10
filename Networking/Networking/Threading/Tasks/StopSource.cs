using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Networking.Threading.Tasks
{
    public class StopSource : IDisposable
    {
        private readonly TaskCompletionSource<bool> m_StopTask = new TaskCompletionSource<bool>();
        private readonly CancellationTokenSource m_CancellationToken = new CancellationTokenSource();

        public void Stop()
        {
            m_CancellationToken.Cancel();
            m_StopTask.TrySetResult(true);
        }

        public Task Task
        {
            get{return m_StopTask.Task;}
        }

        public CancellationToken CancellationToken
        {
            get{return m_CancellationToken.Token;}
        }

        public void Dispose()
        {
            m_CancellationToken.Dispose();
        }
    }
}
