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
            Assert.AreEqual(length, 2)

        [<TestMethod>]
        member this.``enqueue and dequeue`` () =
            let q1 = Queue.empty |> Queue.enqueue 1 |> Queue.enqueue 2
            let q2 = q1 |> Queue.dequeue |> Queue.enqueue 4 |> Queue.enqueue 5

            Assert.AreEqual((Queue.length q2), 3)

        [<TestMethod>]
        member this.``front`` () =
            let q = Queue.empty |> Queue.enqueue 1 |> Queue.enqueue 2 |> Queue.enqueue 3
            
            let f1 = q |> Queue.front
            Assert.AreEqual(f1, 1)

            let f2 = q |> Queue.dequeue |> Queue.front 
            Assert.AreEqual(f2, 2)
        
        [<TestMethod>]
        [<ExpectedException(typeof<Exception>)>]
        member this.``front when empty`` () =
            let f = Queue.empty |> Queue.front
            Assert.AreEqual(f, 1)

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
            Assert.AreEqual((Queue.front baseQ), "Hello")
            
            let q = Queue.dequeue baseQ
            Assert.AreEqual((Queue.front q), "world")

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
                Assert.AreEqual(front, "Jack")
                Assert.AreEqual((Queue.length q'), 3)
                Assert.AreEqual((Queue.front q'), "Sawyer")

        [<TestMethod>]
        member this.``length`` () =
            Assert.AreEqual((Queue.empty |> Queue.length), 0)

            let q = Queue.empty |> Queue.enqueue "Jack" |> Queue.enqueue "Sawyer" |> Queue.enqueue "Kate" |> Queue.enqueue "Hurley"
            Assert.AreEqual((Queue.length q), 4)

