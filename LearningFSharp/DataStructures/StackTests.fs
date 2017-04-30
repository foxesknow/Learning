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
            s |> Stack.top |> Expect.equalTo 10

        [<TestMethod>]
        member this.``push onto something`` () =
            let s = Stack.empty |> Stack.push 10 |> Stack.push 99
            s |> Stack.top |> Expect.equalTo 99

        [<TestMethod>]
        [<ExpectedException(typeof<Exception>)>]
        member this.``top of empty stack`` () =
            let s = Stack.empty
            Stack.top s |> ignore

        [<TestMethod>]
        member this.``top of non-empty stack `` () =
            let s = Stack.empty |> Stack.push 10
            s |> Stack.top |> Expect.equalTo 10

        [<TestMethod>]
        member this.``tryTop of empty stack `` () =
            let s = Stack.empty
            s |> Stack.tryTop |> Expect.none

        [<TestMethod>]
        member this.``tryTop of non-empty stack `` () =
            let s = Stack.empty |> Stack.push 58
            Stack.tryTop s |> Expect.some

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
            s |> Stack.pop |> Stack.top |> Expect.equalTo 12

        [<TestMethod>]
        member this.``tryPop of empty stack`` () =
            let s = Stack.empty
            s |> Stack.tryPop |> Expect.none

        [<TestMethod>]
        member this.``tryPop of non-empty stack`` () =
            let s = Stack.empty |> Stack.push 42
            s |> Stack.tryPop |> Expect.some

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
            s |> Stack.tryTopAndPop |> Expect.none

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
            Stack.empty |> Stack.length |> Expect.equalTo 0

        [<TestMethod>]
        member this.``fold an empty stack`` () =
            Stack.empty |> Stack.fold (fun seed value -> seed + value) 0 |> Expect.equalTo 0

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
            Stack.empty |> Stack.push 1 |> Stack.push 2 |> Stack.length |> Expect.equalTo 2

        [<TestMethod>]
        member this.``reverse of an empty stack`` () =
            let s = Stack.empty |> Stack.reverse
            Stack.isEmpty s |> Assert.IsTrue

        [<TestMethod>]
        member this.``reverse of a non-empty stack`` () =
            let s = Stack.empty |> Stack.push 1 |> Stack.push 2 |> Stack.reverse
            s |> Stack.top |> Expect.equalTo 1
