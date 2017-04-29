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
                BinaryTree.empty |> BinaryTree.add 50 |> BinaryTree.head |> (=) 50 |> Assert.IsTrue

            [<TestMethod>]
            member this.``tryHead of empty`` () =
                match BinaryTree.empty |> BinaryTree.tryHead with
                | None -> Assert.IsTrue(true)
                | Some(_) -> Assert.Fail("head should be missing")

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
                tree |> BinaryTree.left |> BinaryTree.head |> (=) 20 |> Assert.IsTrue

            [<TestMethod>]
            member this.``tryLeft of empty`` () =
                match BinaryTree.empty |> BinaryTree.tryLeft with
                | None -> Assert.IsTrue(true)
                | Some(_) -> Assert.Fail("tree should be empty")
              

            [<TestMethod>]
            member this.``tryLeft of something`` () =
                match BinaryTree.empty |> BinaryTree.add 50 |> BinaryTree.tryLeft with
                | None -> Assert.Fail("should be something")
                | Some(_) -> Assert.IsTrue(true)

            [<TestMethod>]
            member this.``right of tree, empty`` () =
                BinaryTree.empty |> BinaryTree.add 50 |> BinaryTree.right |> BinaryTree.isEmpty |> Assert.IsTrue

            [<TestMethod>]
            member this.``right of tree, something there`` () =
                let tree = BinaryTree.empty |> BinaryTree.add 50 |> BinaryTree.add 70
                tree |> BinaryTree.right |> BinaryTree.isEmpty |> Assert.IsFalse
                tree |> BinaryTree.right |> BinaryTree.head |> (=) 70 |> Assert.IsTrue

            [<TestMethod>]
            member this.``tryRight of empty`` () =
                match BinaryTree.empty |> BinaryTree.tryRight with
                | None -> Assert.IsTrue(true)
                | Some(_) -> Assert.Fail("tree should be empty")
              

            [<TestMethod>]
            member this.``tryRight of something`` () =
                match BinaryTree.empty |> BinaryTree.add 50 |> BinaryTree.tryRight with
                | None -> Assert.Fail("should be something")
                | Some(_) -> Assert.IsTrue(true)

            [<TestMethod>]
            member this.``add`` () =
                let t1 = BinaryTree.empty |> BinaryTree.add 20
                t1 |> BinaryTree.isEmpty |> Assert.IsFalse

                let t2 = t1|> BinaryTree.add 10
                t2 |> BinaryTree.length |> (=) 2 |> Assert.IsTrue

                let t3 = t2|> BinaryTree.add 30
                t3 |> BinaryTree.length |> (=) 3 |> Assert.IsTrue


            [<TestMethod>]
            member this.``addCP #1`` () =
                let t1 = BinaryTree.empty |> BinaryTree.addCP 20
                t1 |> BinaryTree.isEmpty |> Assert.IsFalse

                let t2 = t1|> BinaryTree.addCP 10
                t2 |> BinaryTree.length |> (=) 2 |> Assert.IsTrue

                let t3 = t2|> BinaryTree.addCP 30
                t3 |> BinaryTree.length |> (=) 3 |> Assert.IsTrue

                let t4 = t3|> BinaryTree.addCP 5
                t4 |> BinaryTree.length |> (=) 4 |> Assert.IsTrue

                let t5 = t4|> BinaryTree.addCP 10
                t5 |> BinaryTree.length |> (=) 4 |> Assert.IsTrue

            [<TestMethod>]
            member this.``addCP #2`` () =
                let tree = [100; 50; 120; 40; 130; 110; 60] |> List.fold (fun state i -> BinaryTree.addCP i state) BinaryTree.empty
                tree |> BinaryTree.length |> (=) 7 |> Assert.IsTrue

            [<TestMethod>]
            member this.``add duplicate`` () =
                let t1 = BinaryTree.empty |> BinaryTree.add 20
                t1 |> BinaryTree.length |> (=) 1 |> Assert.IsTrue

                t1 |> BinaryTree. add 20 |> BinaryTree.length |> (=) 1 |> Assert.IsTrue

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

