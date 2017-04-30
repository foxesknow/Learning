namespace DataStructures

open System
open Microsoft.VisualStudio.TestTools.UnitTesting

module BinaryTreeTests =
     [<TestClass>]
        type BinaryTreeTests() =
            [<TestMethod>]
            member this.``tree is empty`` () =
                BinaryTree.empty |> BinaryTree.isEmpty |> Assert.IsTrue

            [<TestMethod>]
            member this.``tree not empty`` () =
                BinaryTree.empty |> BinaryTree.add 1 |> BinaryTree.isEmpty |> Assert.IsFalse

            [<TestMethod>]
            [<ExpectedException(typeof<Exception>)>]
            member this.``head of empty`` () =
                BinaryTree.empty |> BinaryTree.head |> ignore

            [<TestMethod>]
            member this.``head of tree`` () =
                BinaryTree.empty |> BinaryTree.add 50 |> BinaryTree.head |> Expect.equalTo 50

            [<TestMethod>]
            member this.``tryHead of empty`` () =
                BinaryTree.empty |> BinaryTree.tryHead |> Expect.none

            [<TestMethod>]
            member this.``tryHead of something`` () =
                match BinaryTree.empty |> BinaryTree.add(50) |> BinaryTree.tryHead with
                | None -> Assert.Fail("tree is not empty")
                | Some(x) -> Assert.AreEqual(50, x)

            [<TestMethod>]
            [<ExpectedException(typeof<Exception>)>]
            member this.``left of empty`` () =
                BinaryTree.empty |> BinaryTree.left|> ignore

            [<TestMethod>]
            member this.``left of tree, empty`` () =
                BinaryTree.empty |> BinaryTree.add 50 |> BinaryTree.left |> BinaryTree.isEmpty |> Assert.IsTrue

            [<TestMethod>]
            member this.``left of tree, something there`` () =
                let tree = BinaryTree.empty |> BinaryTree.add 50 |> BinaryTree.add 20
                tree |> BinaryTree.left |> BinaryTree.isEmpty |> Assert.IsFalse
                tree |> BinaryTree.left |> BinaryTree.head |> Expect.equalTo 20

            [<TestMethod>]
            member this.``tryLeft of empty`` () =
                BinaryTree.empty |> BinaryTree.tryLeft |> Expect.none
              

            [<TestMethod>]
            member this.``tryLeft of something`` () =
                BinaryTree.empty |> BinaryTree.add 50 |> BinaryTree.tryLeft |> Expect.some

            [<TestMethod>]
            member this.``right of tree, empty`` () =
                BinaryTree.empty |> BinaryTree.add 50 |> BinaryTree.right |> BinaryTree.isEmpty |> Assert.IsTrue

            [<TestMethod>]
            member this.``right of tree, something there`` () =
                let tree = BinaryTree.empty |> BinaryTree.add 50 |> BinaryTree.add 70
                tree |> BinaryTree.right |> BinaryTree.isEmpty |> Assert.IsFalse
                tree |> BinaryTree.right |> BinaryTree.head |> Expect.equalTo 70

            [<TestMethod>]
            member this.``tryRight of empty`` () =
                BinaryTree.empty |> BinaryTree.tryRight |> Expect.none
              

            [<TestMethod>]
            member this.``tryRight of something`` () =
                BinaryTree.empty |> BinaryTree.add 50 |> BinaryTree.tryRight |> Expect.some

            [<TestMethod>]
            member this.``add`` () =
                let t1 = BinaryTree.empty |> BinaryTree.add 20
                t1 |> BinaryTree.isEmpty |> Assert.IsFalse

                let t2 = t1|> BinaryTree.add 10
                t2 |> BinaryTree.length |> Expect.equalTo 2

                let t3 = t2|> BinaryTree.add 30
                t3 |> BinaryTree.length |> Expect.equalTo 3


            [<TestMethod>]
            member this.``addCP #1`` () =
                let t1 = BinaryTree.empty |> BinaryTree.addCP 20
                t1 |> BinaryTree.isEmpty |> Assert.IsFalse

                let t2 = t1|> BinaryTree.addCP 10
                t2 |> BinaryTree.length |> Expect.equalTo 2

                let t3 = t2|> BinaryTree.addCP 30
                t3 |> BinaryTree.length |> Expect.equalTo 3

                let t4 = t3|> BinaryTree.addCP 5
                t4 |> BinaryTree.length |> Expect.equalTo 4

                let t5 = t4|> BinaryTree.addCP 10
                t5 |> BinaryTree.length |> Expect.equalTo 4

            [<TestMethod>]
            member this.``addCP #2`` () =
                let tree = [100; 50; 120; 40; 130; 110; 60] |> List.fold (fun state i -> BinaryTree.addCP i state) BinaryTree.empty
                tree |> BinaryTree.length |> Expect.equalTo 7

            [<TestMethod>]
            member this.``add duplicate`` () =
                let t1 = BinaryTree.empty |> BinaryTree.add 20
                t1 |> BinaryTree.length |> Expect.equalTo 1

                t1 |> BinaryTree. add 20 |> BinaryTree.length |> Expect.equalTo 1

            [<TestMethod>]
            member this.``asInfixSequqnce`` () =
                let tree = [100; 50; 120; 40; 130; 110; 60] |> List.fold (fun state i -> BinaryTree.addCP i state) BinaryTree.empty
                let sequence = BinaryTree.asInfixSequence tree
                let array = Seq.toArray sequence

                Assert.AreEqual(40, array.[0])
                Assert.AreEqual(50, array.[1])
                Assert.AreEqual(60, array.[2])
                Assert.AreEqual(100, array.[3])
                Assert.AreEqual(110, array.[4])
                Assert.AreEqual(120, array.[5])
                Assert.AreEqual(130, array.[6])

            [<TestMethod>]
            member this.``asPrefixSequence`` () =
                let tree = [100; 50; 120; 40; 130; 110; 60] |> List.fold (fun state i -> BinaryTree.addCP i state) BinaryTree.empty
                let sequence = BinaryTree.asPrefixSequence tree
                let array = Seq.toArray sequence

                Assert.AreEqual(100 ,array.[0])
                Assert.AreEqual(50, array.[1])
                Assert.AreEqual(40, array.[2])
                Assert.AreEqual(60, array.[3])
                Assert.AreEqual(120, array.[4])
                Assert.AreEqual(110 ,array.[5])
                Assert.AreEqual(130, array.[6])

            [<TestMethod>]
            member this.``asPostfixSequence`` () =
                let tree = [100; 50; 120; 40; 130; 110; 60] |> List.fold (fun state i -> BinaryTree.addCP i state) BinaryTree.empty
                let sequence = BinaryTree.asPostfixSequence tree
                let array = Seq.toArray sequence

                Assert.AreEqual(40, array.[0])
                Assert.AreEqual(60, array.[1])
                Assert.AreEqual(50, array.[2])
                Assert.AreEqual(110, array.[3])
                Assert.AreEqual(130, array.[4])
                Assert.AreEqual(120, array.[5])
                Assert.AreEqual(100, array.[6])

            [<TestMethod>]
            member this.``breadthFirst`` () =
                let tree = [100; 50; 120; 40; 130; 110; 60] |> List.fold (fun state i -> BinaryTree.add i state) BinaryTree.empty
                let sequence = BinaryTree.breadthFirst tree
                let array = Seq.toArray sequence

                Assert.AreEqual(100, array.[0])
                Assert.AreEqual(50, array.[1])
                Assert.AreEqual(120, array.[2])
                Assert.AreEqual(40, array.[3])
                Assert.AreEqual(60, array.[4])
                Assert.AreEqual(110, array.[5])
                Assert.AreEqual(130, array.[6])

