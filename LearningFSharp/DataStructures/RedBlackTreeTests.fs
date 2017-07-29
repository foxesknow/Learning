namespace DataStructures

open System
open Microsoft.VisualStudio.TestTools.UnitTesting

module RedBlackTreeTests =
    [<TestClass>]
    type RedBlackTreeTests() =
        [<TestMethod>]
        member this.``check balance`` () =
            //let tree = [1; 6; 8; 11; 13; 15; 17; 22; 25; 27] |> List.fold(fun state i -> RedBlackTree.add i state) RedBlackTree.empty
            let tree = [1; 2; 3; 4; 5; 6; 7; 8] |> List.fold(fun state i -> RedBlackTree.add i state) RedBlackTree.empty
            tree |> RedBlackTree.isEmpty |> Assert.IsFalse

        [<TestMethod>]
        [<ExpectedException(typeof<Exception>)>]
        member this.``head of empty`` () =
            RedBlackTree.empty |> RedBlackTree.head |> ignore

        [<TestMethod>]
        member this.``head of tree`` () =
            RedBlackTree.empty |> RedBlackTree.add 50 |> RedBlackTree.head |> Expect.equalTo 50

        [<TestMethod>]
        member this.``tryHead of empty`` () =
            RedBlackTree.empty |> RedBlackTree.tryHead |> Expect.none

        [<TestMethod>]
        member this.``tryHead of something`` () =
            match RedBlackTree.empty |> RedBlackTree.add(50) |> RedBlackTree.tryHead with
            | None -> Assert.Fail("tree is not empty")
            | Some(x) -> Assert.AreEqual(50, x)

        [<TestMethod>]
        [<ExpectedException(typeof<Exception>)>]
        member this.``left of empty`` () =
            RedBlackTree.empty |> RedBlackTree.left|> ignore

        [<TestMethod>]
        member this.``left of tree, empty`` () =
            RedBlackTree.empty |> RedBlackTree.add 50 |> RedBlackTree.left |> RedBlackTree.isEmpty |> Assert.IsTrue

        [<TestMethod>]
        member this.``left of tree, something there`` () =
            let tree = RedBlackTree.empty |> RedBlackTree.add 50 |> RedBlackTree.add 20
            tree |> RedBlackTree.left |> RedBlackTree.isEmpty |> Assert.IsFalse
            tree |> RedBlackTree.left |> RedBlackTree.head |> Expect.equalTo 20

        [<TestMethod>]
        member this.``tryLeft of empty`` () =
            RedBlackTree.empty |> RedBlackTree.tryLeft |> Expect.none

        [<TestMethod>]
        member this.``tryLeft of something #1`` () =
            RedBlackTree.empty |> RedBlackTree.add 50 |> RedBlackTree.tryLeft |> Expect.none

        [<TestMethod>]
        member this.``tryLeft of something #2`` () =
            RedBlackTree.empty |> RedBlackTree.add 50 |> RedBlackTree.add 20 |> RedBlackTree.tryLeft |> Expect.some

        [<TestMethod>]
        member this.``right of tree, empty`` () =
            RedBlackTree.empty |> RedBlackTree.add 50 |> RedBlackTree.right |> RedBlackTree.isEmpty |> Assert.IsTrue

        [<TestMethod>]
        member this.``right of tree, something there`` () =
            let tree = RedBlackTree.empty |> RedBlackTree.add 50 |> RedBlackTree.add 70
            tree |> RedBlackTree.right |> RedBlackTree.isEmpty |> Assert.IsFalse
            tree |> RedBlackTree.right |> RedBlackTree.head |> Expect.equalTo 70

        [<TestMethod>]
        member this.``tryRight of empty`` () =
            RedBlackTree.empty |> RedBlackTree.tryRight |> Expect.none
              
        [<TestMethod>]
        member this.``tryRight of something #1`` () =
            RedBlackTree.empty |> RedBlackTree.add 50 |> RedBlackTree.tryRight |> Expect.none

        [<TestMethod>]
        member this.``tryRight of something #2`` () =
            RedBlackTree.empty |> RedBlackTree.add 50 |> RedBlackTree.add 75 |> RedBlackTree.tryRight |> Expect.some
        
        [<TestMethod>]
        member this.``length of empty`` () =
            RedBlackTree.empty |> RedBlackTree.length |> Expect.equalTo 0

        [<TestMethod>]
        member this.``length of tree with data`` () =
            let tree = [100; 50; 120; 40; 130; 110; 60] |> List.fold (fun state i -> RedBlackTree.add i state) RedBlackTree.empty
            tree |> RedBlackTree.length |> Expect.equalTo 7

        [<TestMethod>]
        member this.``maxDepth of empty`` () =
            RedBlackTree.empty |> RedBlackTree.maxDepth |> Expect.equalTo 0

        [<TestMethod>]
        member this.``maxDepth of single node`` () =
            RedBlackTree.empty |> RedBlackTree.add 20 |> RedBlackTree.maxDepth |> Expect.equalTo 1

        [<TestMethod>]
        member this.``maxDepth`` () =
            RedBlackTree.empty |> RedBlackTree.add 20 |> RedBlackTree.add 10 |> RedBlackTree.add 30 |> RedBlackTree.maxDepth |> Expect.equalTo 2
            RedBlackTree.empty |> RedBlackTree.add 20 |> RedBlackTree.add 10 |> RedBlackTree.add 30 |> RedBlackTree.add 40 |> RedBlackTree.maxDepth |> Expect.equalTo 3

        [<TestMethod>]
            member this.``asInfixSequqnce`` () =
                let tree = [100; 50; 120; 40; 130; 110; 60] |> List.fold (fun state i -> RedBlackTree.add i state) RedBlackTree.empty
                let sequence = RedBlackTree.asInfixSequence tree
                let array = Seq.toArray sequence

                Assert.AreEqual(40, array.[0])
                Assert.AreEqual(50, array.[1])
                Assert.AreEqual(60, array.[2])
                Assert.AreEqual(100, array.[3])
                Assert.AreEqual(110, array.[4])
                Assert.AreEqual(120, array.[5])
                Assert.AreEqual(130, array.[6])

