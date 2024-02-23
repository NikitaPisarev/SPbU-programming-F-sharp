let factorial n =
    if n < 0 then
        None
    else
        let rec recFactorial n acc =
            match n with
            | 0 -> Some(acc)
            | _ -> recFactorial (n - 1) (n * acc)

        recFactorial n 1

let fibonacci n =
    if n <= 0 then
        None
    else
        let rec recFibonacci n curr next =
            match n with
            | 1 -> Some(curr)
            | _ -> recFibonacci (n - 1) next (curr + next)

        recFibonacci n 0 1

let reverse list =
    let rec recReverse list acc =
        match list with
        | [] -> acc
        | h :: t -> recReverse t (h :: acc)

    recReverse list []

let powerSeries n m =
    let rec generateSeries curr i acc =
        if i > m then
            acc
        else
            generateSeries (curr * 2) (i + 1) (acc @ [ curr ])

    generateSeries (pown 2 n) 0 []

let findFirst list x =
    let rec recFindFirst list pos =
        match list with
        | [] -> -1
        | h :: t -> if h = x then pos else recFindFirst t (pos + 1)

    recFindFirst list 0
