using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace FunctionalCSharp
{
    public class ImmutableQueue<T> : IEnumerable<T>
    {
        public static readonly ImmutableQueue<T> Empty = new ImmutableQueue<T>();

        private ImmutableQueue()
        {
            this.FrontStack = ImmutableStack<T>.Empty;
            this.BackStack = ImmutableStack<T>.Empty;
        }

        private ImmutableQueue(ImmutableStack<T> front, ImmutableStack<T> back)
        {
            this.FrontStack = front;
            this.BackStack = back;
        }

        private ImmutableStack<T> FrontStack{get;}
        private ImmutableStack<T> BackStack{get; }

        private ImmutableStack<T> EmptyStack
        {
            get{return ImmutableStack<T>.Empty;}
        }

        public bool IsEmpty
        {
            get{return this.FrontStack.IsEmpty && this.BackStack.IsEmpty;}
        }

        public T Front()
        {
            if(this.IsEmpty) throw new InvalidOperationException();

            return this.FrontStack.Top();
        }

        public ImmutableQueue<T> Enqueue(T value)
        {
            if(this.IsEmpty)
            {
                return Rebalance(this.EmptyStack, this.EmptyStack.Push(value));
            }

            return Rebalance(this.FrontStack, this.BackStack.Push(value));
        }

        public ImmutableQueue<T> Dequeue()
        {
            if(this.IsEmpty) throw new InvalidOperationException();

            return Rebalance(this.FrontStack.Pop(), this.BackStack);
        }

        private ImmutableQueue<T> Rebalance(ImmutableStack<T> front, ImmutableStack<T> back)
        {
            return (front, back) switch
            {
                var (f, b) when f.IsEmpty && b.IsEmpty => Empty,
                var (f, b) when f.IsEmpty => new ImmutableQueue<T>(b.Reverse(), this.EmptyStack),
                _   => new ImmutableQueue<T>(front, back)
            };
        }

        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            foreach(var value in this.FrontStack)
            {
                yield return value;
            }

            foreach(var value in this.BackStack.Reverse())
            {
                yield return value;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable<T>)this).GetEnumerator();
        }
    }
}
