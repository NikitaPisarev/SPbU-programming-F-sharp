module Test.Square

let printSquare n =
    let row i =
        if i = 1 || i = n then
            String.replicate n "*"
        else
            "*" + String.replicate (n - 2) " " + "*"
    seq { 1 .. n } |> Seq.map row |> Seq.iter (fun s -> printfn "%s" s)

printSquare 5
