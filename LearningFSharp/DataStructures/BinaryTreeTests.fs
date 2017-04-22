﻿namespace DataStructures

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
