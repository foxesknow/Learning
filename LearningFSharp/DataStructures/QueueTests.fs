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

