namespace DataStructures

open System
open Microsoft.VisualStudio.TestTools.UnitTesting

module QueueTests =
    [<TestClass>]
    type QueueTests() =
        [<TestMethod>]
        member this.``queue is empty`` () =
            Queue.empty |> Queue.isEmpty |> Assert.IsTrue

        [<TestMethod>]
        member this.``queue not empty`` () =
            Queue.empty |> Queue.enqueue 10 |> Queue.isEmpty |> Assert.IsFalse

        [<TestMethod>]
        member this.``enqueue`` () =
            let length = Queue.empty |> Queue.enqueue 1 |> Queue.enqueue 2 |> Queue.length
            Assert.AreEqual(2, length)

        [<TestMethod>]
        member this.``enqueue and dequeue`` () =
            let q1 = Queue.empty |> Queue.enqueue 1 |> Queue.enqueue 2
            let q2 = q1 |> Queue.dequeue |> Queue.enqueue 4 |> Queue.enqueue 5

            Assert.AreEqual(3, (Queue.length q2))

        [<TestMethod>]
        member this.``front`` () =
            let q = Queue.empty |> Queue.enqueue 1 |> Queue.enqueue 2 |> Queue.enqueue 3
            
            let f1 = q |> Queue.front
            Assert.AreEqual(1, f1)

            let f2 = q |> Queue.dequeue |> Queue.front 
            Assert.AreEqual(2, f2)
        
        [<TestMethod>]
        [<ExpectedException(typeof<Exception>)>]
        member this.``front when empty`` () =
            let f = Queue.empty |> Queue.front
            Assert.AreEqual(1, f)

        [<TestMethod>]
        member this.``tryFront when empty`` () =
            let f = Queue.empty |> Queue.tryFront
            
            match f with
            | None -> Assert.IsTrue(true)
            | _ -> Assert.Fail("queue should be empty")

        [<TestMethod>]
        member this.``tryFront when data`` () =
            let f = Queue.empty |> Queue.enqueue "Hello" |> Queue.tryFront
            
            match f with
            | None -> Assert.Fail("queue has data but none returned")
            | _ -> Assert.IsTrue(true)

        [<TestMethod>]
        [<ExpectedException(typeof<Exception>)>]
        member this.``dequeue empty queue`` () =
            let q = Queue.empty |> Queue.dequeue
            q |> ignore

        [<TestMethod>]
        member this.``dequeue`` () =
            let baseQ = Queue.empty |> Queue.enqueue "Hello" |> Queue.enqueue "world"
            Assert.AreEqual("Hello", (Queue.front baseQ))
            
            let q = Queue.dequeue baseQ
            Assert.AreEqual("world", (Queue.front q))

        [<TestMethod>]
        member this.``dequeue to empty`` () =
            let baseQ = Queue.empty |> Queue.enqueue "Hello" |> Queue.enqueue "world"
            let q1 = Queue.dequeue baseQ
            let q2 = Queue.dequeue q1

            match q2 with
            | Queue.Empty -> Assert.IsTrue(true)
            | _ -> Assert.Fail("should be empty")

        [<TestMethod>]
        member this.``tryDequeue an empty queue`` () =
            let q = Queue.empty
            match Queue.tryDequeue q with
            | None -> Assert.IsTrue(true)
            | _ -> Assert.Fail ("The queue should be empty")

        [<TestMethod>]
        member this.``tryDequeue a queue with data`` () =
            let q = Queue.empty |> Queue.enqueue "Jack" |> Queue.enqueue "Sawyer"
            match Queue.tryDequeue q with
            | None -> Assert.Fail("The queue shouldn't be empty")
            | _ -> Assert.IsTrue(true)

        [<TestMethod>]
            member this.``tryFrontAndDequeue an empty queue`` () =
                let q = Queue.empty
                match Queue.tryFrontAndDequeue q with
                | None -> Assert.IsTrue(true)
                | _ -> Assert.Fail ("The queue should be empty")            

        [<TestMethod>]
        member this.``tryFrontAndDequeue a queue with data`` () =
            let q = Queue.empty |> Queue.enqueue "Jack" |> Queue.enqueue "Sawyer" |> Queue.enqueue "Kate" |> Queue.enqueue "Hurley"
            match Queue.tryFrontAndDequeue q with
            | None -> Assert.Fail("The queue shouldn't be empty")
            | Some(front, q') -> 
                Assert.AreEqual("Jack", front)
                Assert.AreEqual(3, (Queue.length q'))
                Assert.AreEqual("Sawyer", (Queue.front q'))

        [<TestMethod>]
        member this.``length`` () =
            Assert.AreEqual(0, (Queue.empty |> Queue.length))

            let q = Queue.empty |> Queue.enqueue "Jack" |> Queue.enqueue "Sawyer" |> Queue.enqueue "Kate" |> Queue.enqueue "Hurley"
            Assert.AreEqual(4, (Queue.length q))

