using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

namespace FunctionalCSharp
{
    public class ImmutableStack<T> : IEnumerable<T>
    {
        public static readonly ImmutableStack<T> Empty = new ImmutableStack<T>();

        private ImmutableStack()
        {
        }

        private ImmutableStack(T value, ImmutableStack<T> tail)
        {
            this.Value = value;
            this.Tail = tail;
        }

        private ImmutableStack<T> Tail{get;}

        private T Value{get;}

        public bool IsEmpty
        {
            get{return this.Tail is null;}
        }

        public ImmutableStack<T> Push(T value)
        {
            return new ImmutableStack<T>(value, this);
        }

        public ImmutableStack<T> Pop()
        {
            if(this.IsEmpty) throw new InvalidOperationException();

            return this.Tail;
        }

        public T Top()
        {
            if(this.IsEmpty) throw new InvalidOperationException();

            return this.Value;
        }

        public ImmutableStack<T> Reverse()
        {
            return this.Aggregate(Empty, (acc, value) => acc.Push(value));
        }

        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            for(var next = this; next.IsEmpty == false; next = next.Tail)
            {
                yield return next.Value;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable<T>)this).GetEnumerator();
        }
    }
}
