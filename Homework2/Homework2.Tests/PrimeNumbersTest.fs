module Homework2.PrimeNumbers.Test

open NUnit.Framework
open FsUnit

[<Test>]
let ``generatePrimeNumbers should produce first 20 primes correctly`` () =
    let expectedPrimes =
        [ 2; 3; 5; 7; 11; 13; 17; 19; 23; 29; 31; 37; 41; 43; 47; 53; 59; 61; 67; 71 ]

    let primes = generatePrimeNumbers () |> Seq.take 20 |> Seq.toList
    primes |> should equal expectedPrimes
