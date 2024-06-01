module StringCalculation.Tests

open NUnit.Framework
open FsUnit
open StringCalculation

[<Test>]
let ``Calculation with correct data should return correct answer1`` () =
    let calculate = StringCalculationBuilder()

    let result =
        calculate {
            let! x = "1"
            let! y = "2"
            let z = x + y
            return z
        }

    result |> should equal (Some 3)

[<Test>]
let ``Calculation with correct data should return correct answer2`` () =
    let calculate = StringCalculationBuilder()

    let result =
        calculate {
            let! x = "3211"
            let! y = "412"
            let! z = "7"
            let w = x + y + z
            return w
        }

    result |> should equal (Some 3630)


[<Test>]
let ``Calculation with wrong data should return none`` () =
    let calculate = StringCalculationBuilder()

    let result =
        calculate {
            let! x = "123"
            let! y = "56b"
            let z = x + y
            return z
        }

    result |> should equal None
