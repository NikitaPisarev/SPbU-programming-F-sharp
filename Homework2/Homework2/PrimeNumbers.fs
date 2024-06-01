module Homework2.PrimeNumbers

let generatePrimeNumbers() = 
    let isPrime number = 
        let root = float number |> sqrt |> int 
        seq{2 .. root} |> Seq.forall(fun i -> number % i <> 0)

    Seq.initInfinite(fun elem -> elem + 2) |> Seq.filter isPrime