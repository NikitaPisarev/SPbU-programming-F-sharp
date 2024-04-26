module Test.Fibonacci

let evenFibonacciSum n =
    Seq.unfold (fun (x, y) -> Some(x, (y, x + y))) (0, 1)
    |> Seq.takeWhile (fun x -> x <= n)
    |> Seq.filter (fun x -> x % 2 = 0)
    |> Seq.sum

printfn "%d" (evenFibonacciSum 1_000_000)