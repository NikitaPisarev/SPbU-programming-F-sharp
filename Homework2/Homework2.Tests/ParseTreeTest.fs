module Homework2.ParseTree.Test

open NUnit.Framework
open FsUnit

let testCases () =
    [ Operand 5, 5
      Add(Operand 2, Operand 3), 5
      Subtract(Operand 10, Operand 4), 6
      Multiply(Operand 3, Operand 4), 12
      Modulo(Operand 10, Operand 3), 1
      Subtract(Add(Operand 10, Operand 5), Multiply(Operand 2, Operand 3)), 9 ]
    |> List.map (fun (tree, expected) -> TestCaseData(tree, expected))

[<TestCaseSource(nameof testCases)>]
let ``calculate should return correct value`` tree expected = calculate tree |> should equal expected
