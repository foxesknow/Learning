namespace DataStructures

open System
open Microsoft.VisualStudio.TestTools.UnitTesting

module StackTests =
    [<TestClass>]
    type StackTests() =
        [<TestMethod>]
        member this.``stack is empty`` () =
            Stack.empty |> Stack.isEmpty |> Assert.IsTrue

        [<TestMethod>]
        member this.``stack not empty`` () =
            Stack.empty |> Stack.push 10 |> Stack.isEmpty |> Assert.IsFalse

        [<TestMethod>]
        member this.``push onto empty`` () =
            let s = Stack.empty |> Stack.push 10
            s |> Stack.isEmpty |> Assert.IsFalse
            Assert.IsTrue((Stack.top s) = 10)

        [<TestMethod>]
        member this.``push onto something`` () =
            let s = Stack.empty |> Stack.push 10 |> Stack.push 99
            Assert.IsTrue((Stack.top s) = 99)

        [<TestMethod>]
        [<ExpectedException(typeof<Exception>)>]
        member this.``top of empty stack`` () =
            let s = Stack.empty
            Stack.top s |> ignore

        [<TestMethod>]
        member this.``top of non-empty stack `` () =
            let s = Stack.empty |> Stack.push 10
            Assert.IsTrue((Stack.top s) = 10)

        [<TestMethod>]
        member this.``tryTop of empty stack `` () =
            let s = Stack.empty
            match Stack.tryTop s with
            | Some _ -> Assert.Fail()
            | None -> Assert.IsTrue(true)

        [<TestMethod>]
        member this.``tryTop of non-empty stack `` () =
            let s = Stack.empty |> Stack.push 58
            match Stack.tryTop s with
            | Some value -> Assert.IsTrue((value = 58))
            | None -> Assert.Fail()

        [<TestMethod>]
        [<ExpectedException(typeof<Exception>)>]
        member this.``pop of empty stack`` () =
            let s = Stack.empty
            Stack.pop s |> ignore

        [<TestMethod>]
        member this.``pop of 1 item stack`` () =
            let s = Stack.empty |> Stack.push 12
            Stack.pop s |> Stack.isEmpty |> Assert.IsTrue

        [<TestMethod>]
        member this.``pop of many item stack`` () =
            let s = Stack.empty |> Stack.push 12 |> Stack.push 60
            Stack.pop s |> Stack.isEmpty |> Assert.IsFalse

            match Stack.pop s |> Stack.top with
            | 12 -> Assert.IsTrue(true)
            | _ -> Assert.Fail()

        [<TestMethod>]
        member this.``tryPop of empty stack`` () =
            let s = Stack.empty
            match Stack.tryPop s with
            | Some _ -> failwith "expected none"
            | None -> Assert.IsTrue(true)

        [<TestMethod>]
        member this.``tryPop of non-empty stack`` () =
            let s = Stack.empty |> Stack.push 42
            match Stack.tryPop s with
            | Some p -> Assert.IsTrue(true)
            | None -> failwith "expected some"

        [<TestMethod>]
        [<ExpectedException(typeof<Exception>)>]
        member this.``topAndPop of empty stack`` () =
            let s = Stack.empty
            Stack.topAndPop s |> ignore

        [<TestMethod>]
        member this.``topAndPop of non-empty stack`` () =
            let s = Stack.empty |> Stack.push 61
            
            let (top, rest) = Stack.topAndPop s
            top = 61 |> Assert.IsTrue
            Stack.isEmpty rest |> Assert.IsTrue

        [<TestMethod>]
        member this.``tryTopAndPop of empty stack`` () =
            let s = Stack.empty
            match Stack.tryTopAndPop s with
            | Some(top, rest) -> failwith "should be empty"
            | None -> Assert.IsTrue(true)

        [<TestMethod>]
        member this.``tryTopAndPop of non-empty stack`` () =
            let s = Stack.empty |> Stack.push 3 |> Stack.push 5
            match Stack.tryTopAndPop s with
            | Some(top, rest) -> 
                top = 5 |> Assert.IsTrue
                Stack.isEmpty rest |> Assert.IsFalse
                Assert.IsTrue((Stack.top rest) = 3)
            
            | None -> failwith "expected some"

        [<TestMethod>]
        member this.``length of an empty stack`` () =
            let length = Stack.empty |> Stack.length
            Assert.IsTrue((length = 0))

        [<TestMethod>]
        member this.``fold an empty stack`` () =
            let value = Stack.empty |> Stack.fold (fun seed value -> seed + value) 0
            Assert.IsTrue((value = 0))

        [<TestMethod>]
        member this.``fold a non-empty stack`` () =
            let value = Stack.empty 
                        |> Stack.push 3 
                        |> Stack.push 5 
                        |> Stack.push 7 
                        |> Stack.fold (fun seed value -> seed + value) 0
            Assert.IsTrue((value = 15))

        [<TestMethod>]
        member this.``length of a non-empty stack`` () =
            let length = Stack.empty |> Stack.push 1 |> Stack.push 2 |> Stack.length
            Assert.IsTrue((length = 2))

        [<TestMethod>]
        member this.``reverse of an empty stack`` () =
            let s = Stack.empty |> Stack.reverse
            Stack.isEmpty s |> Assert.IsTrue

        [<TestMethod>]
        member this.``reverse of a non-empty stack`` () =
            let s = Stack.empty |> Stack.push 1 |> Stack.push 2 |> Stack.reverse
            Assert.IsTrue((Stack.top s) = 1)
