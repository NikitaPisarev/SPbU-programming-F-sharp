module Homework2.EvenNumbers.Test

open NUnit.Framework
open FsUnit
open FsCheck

let testCases () =
    [ [ 1; 2; 3; 4; 5 ], 2
      [], 0
      [ -1; -2; -3 ], 1
      [ 2; 4; 6; 8; 10 ], 5
      [ 1; 3; 5; 7; 9 ], 0 ]
    |> List.map (fun (list, expected) -> TestCaseData(list, expected))


[<TestCaseSource(nameof testCases)>]
let ``countEvenNumbersMap should return correct count`` list expected =
    list |> countEvenNumbersMap |> should equal expected

[<Test>]
let ``countEvenNumbersMap and countEvenNumbersFilter should be equivalent`` () =
    Check.QuickThrowOnFailure(fun list -> countEvenNumbersMap list = countEvenNumbersFilter list)

[<Test>]
let ``countEvenNumbersMap and countEvenNumbersFold should be equivalent`` () =
    Check.QuickThrowOnFailure(fun list -> countEvenNumbersMap list = countEvenNumbersFold list)
