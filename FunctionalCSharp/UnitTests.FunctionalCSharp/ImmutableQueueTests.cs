using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

using FunctionalCSharp;

using NUnit.Framework;

namespace UnitTests.FunctionalCSharp
{
    [TestFixture]
    public class ImmutableQueueTests
    {
        [Test]
        public void Empty()
        {
            var queue = ImmutableQueue<int>.Empty;
            Assert.That(queue.IsEmpty, Is.True);
        }

        [Test]
        public void IsEmpty()
        {
            var queue = ImmutableQueue<int>.Empty;
            Assert.That(queue.IsEmpty, Is.True);

            var queue2 = queue.Enqueue(10);
            Assert.That(queue.IsEmpty, Is.True);
            Assert.That(queue2.IsEmpty, Is.False);
        }

        [Test]
        public void Enqueue_SingleItem()
        {
            var queue = ImmutableQueue<int>.Empty.Enqueue(5);
            Assert.That(queue.Front(), Is.EqualTo(5));
        }

        [Test]
        public void Enqueue_MultipleItems()
        {
            var queue = ImmutableQueue<int>.Empty
                        .Enqueue(5)
                        .Enqueue(10)
                        .Enqueue(15);

            Assert.That(queue.Front(), Is.EqualTo(5));
        }

        [Test]
        public void Dequeue_SingleItem()
        {
            var queue = ImmutableQueue<int>.Empty.Enqueue(5);
            var queue2 = queue.Dequeue();
            
            Assert.That(queue.Front(), Is.EqualTo(5));
            Assert.That(queue2.IsEmpty, Is.True);
        }

        [Test]
        public void Dequeue_MultipleItems()
        {
            var queue = ImmutableQueue<int>.Empty
                        .Enqueue(5)
                        .Enqueue(10)
                        .Enqueue(15);

            var queue2 = queue.Dequeue();

            Assert.That(queue.Front(), Is.EqualTo(5));
            Assert.That(queue2.Front(), Is.EqualTo(10));
        }

        [Test]
        public void Dequeue_NoData()
        {
            Assert.Catch(() => ImmutableQueue<int>.Empty.Dequeue());
        }

        [Test]
        public void Front_NoData()
        {
            Assert.Catch(() => ImmutableQueue<int>.Empty.Front());
        }

        [Test]
        public void Enumeration()
        {
            var queue = ImmutableQueue<int>.Empty
                        .Enqueue(1)
                        .Enqueue(1)
                        .Enqueue(2)
                        .Enqueue(3)
                        .Enqueue(5);

            var flattened = queue.ToList();

            Assert.That(flattened[0], Is.EqualTo(1));
            Assert.That(flattened[1], Is.EqualTo(1));
            Assert.That(flattened[2], Is.EqualTo(2));
            Assert.That(flattened[3], Is.EqualTo(3));
            Assert.That(flattened[4], Is.EqualTo(5));
        }
    }
}
