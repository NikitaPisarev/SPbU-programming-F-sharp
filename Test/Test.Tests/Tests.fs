module Test.Tests

open NUnit.Framework
open FsUnit
open Test.Fibonacci

[<TestFixture>]
type EvenFibonacciSumTests() =
    [<Test>]
    member this.``EvenFibonacciSum returns correct sum for small inputs``() =
        evenFibonacciSum 3 |> should equal 2
        evenFibonacciSum 8 |> should equal 10
        evenFibonacciSum 34 |> should equal 44

    [<Test>]
    member this.``EvenFibonacciSum returns correct sum for inputs less than 2``() =
        evenFibonacciSum 1 |> should equal 0
        evenFibonacciSum 0 |> should equal 0