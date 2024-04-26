module Test.Fibonacci

let evenFibonacciSum =
    Seq.unfold (fun (x, y) -> Some(x, (y, x + y))) (0, 1)
    |> Seq.takeWhile (fun x -> x <= 1_000_000)
    |> Seq.filter (fun x -> x % 2 = 0)
    |> Seq.sum

printfn "%d" evenFibonacciSum