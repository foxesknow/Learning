using System;
using System.Linq;

using FunctionalCSharp;

using NUnit.Framework;

namespace UnitTests.FunctionalCSharp
{
    public class ImmutableStackTests
    {
        [Test]
        public void Empty()
        {
            var stack = ImmutableStack<int>.Empty;
            Assert.That(stack.IsEmpty, Is.True);
        }

        [Test]
        public void IsEmpty()
        {
            var stack = ImmutableStack<int>.Empty;
            Assert.That(stack.IsEmpty, Is.True);

            var stack2 = stack.Push(10);
            Assert.That(stack.IsEmpty, Is.True);
            Assert.That(stack2.IsEmpty, Is.False);
        }

        [Test]
        public void Push()
        {
            var stack = ImmutableStack<int>.Empty
                        .Push(10);

            Assert.That(stack.Top(), Is.EqualTo(10));
        }

        [Test]
        public void Pop()
        {
            var stack = ImmutableStack<int>.Empty
                        .Push(10);

            var stack2 = stack.Pop();
            Assert.That(stack2, Is.EqualTo(ImmutableStack<int>.Empty));
        }

        [Test]
        public void Pop_EmptyStack()
        {
            Assert.Catch(() => ImmutableStack<int>.Empty.Pop());
        }

        [Test]
        public void Top()
        {
            var stack = ImmutableStack<int>.Empty
                        .Push(10);

            var value = stack.Top();
            Assert.That(value, Is.EqualTo(10));
            Assert.That(stack.IsEmpty, Is.False);
        }

        [Test]
        public void Peek_EmptyStack()
        {
            Assert.Catch(() => ImmutableStack<int>.Empty.Top());
        }

        [Test]
        public void Reverse()
        {
            var stack = ImmutableStack<int>.Empty
                        .Push(1)
                        .Push(2)
                        .Push(3)
                        .Reverse();

            Assert.That(stack.Top(), Is.EqualTo(1));
            stack = stack.Pop();

            Assert.That(stack.Top(), Is.EqualTo(2));
            stack = stack.Pop();

            Assert.That(stack.Top(), Is.EqualTo(3));
            stack = stack.Pop();
        }

        [Test]
        public void Enumeration()
        {
            var stack = ImmutableStack<int>.Empty
                        .Push(1)
                        .Push(1)
                        .Push(2)
                        .Push(3)
                        .Push(5);

            var flattened = stack.ToList();

            Assert.That(flattened[0], Is.EqualTo(5));
            Assert.That(flattened[1], Is.EqualTo(3));
            Assert.That(flattened[2], Is.EqualTo(2));
            Assert.That(flattened[3], Is.EqualTo(1));
            Assert.That(flattened[4], Is.EqualTo(1));
        }
    }
}