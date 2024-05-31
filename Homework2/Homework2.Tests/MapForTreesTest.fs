module Homework2.MapForTrees.Test

open NUnit.Framework
open FsUnit
open MapForTrees

let testCases () =
    [ (Empty, (fun (x: int) -> x + 1), Empty)

      (Node(1, Empty, Empty), (fun (x: int) -> x + 1), Node(2, Empty, Empty))

      (Node(1, Node(2, Empty, Empty), Node(3, Empty, Empty)),
       (fun (x: int) -> x * 2),
       Node(2, Node(4, Empty, Empty), Node(6, Empty, Empty))) ]
    |> List.map (fun (tree, func, expected) -> TestCaseData(tree, func, expected))

[<TestCaseSource(nameof testCases)>]
let ``map should apply function to each element correctly`` (tree: Tree<int>) (func: int -> int) (expected: Tree<int>) =
    map tree func |> should equal expected
