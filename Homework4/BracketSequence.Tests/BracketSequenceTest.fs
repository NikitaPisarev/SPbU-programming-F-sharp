module BracketSequence.Test

open NUnit.Framework
open FsUnit

let bracketTestCases () =
    [ "", true
      "()", true
      "[]", true
      "{}", true
      "({[]})", true
      "(", false
      ")", false
      "(]", false
      "({[)})", false
      "({[]){[()]})", false
      "({[ab]})", true
      "({[}])", false
      "{[(])}", false
      "{[()]", false
      "{[(])}", false
      "{[()]}", true ]
    |> List.map (fun (input, expected) -> TestCaseData(input, expected))

[<TestCaseSource(nameof bracketTestCases)>]
let ``Bracket sequence should be evaluated correctly`` (input: string, expected: bool) =
    let result = isBalanced input
    result |> should equal expected
