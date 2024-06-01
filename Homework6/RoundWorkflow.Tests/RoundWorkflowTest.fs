module RoundWorkflow.Tests

open NUnit.Framework
open FsUnit
open RoundWorkflow

[<Test>]
let ``Correct calculation with positive precision should give the correct answer`` () =
    let result =
        rounding 3 {
            let! a = 2.0 / 12.0
            let! b = 3.5
            return a / b
        }

    result |> should equal 0.048

[<Test>]
let ``Correct calculation with zero precision should give the correct answerr`` () =
    let result =
        rounding 0 {
            let! a = 25.0 / 4.0
            let! b = 2.5
            return a / b
        }

    result |> should equal 3


[<Test>]
let ``Calculation with negative precision should throw exception`` () =
    (fun () ->
        rounding -2 {
            let! a = 2.0 / 12.0
            let! b = 3.5
            return a / b
        }
        |> ignore)
    |> should throw typeof<System.ArgumentOutOfRangeException>
