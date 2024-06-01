module Homework2.EvenNumbers

let countEvenNumbersMap list =
    list |> List.map (fun number -> if number % 2 = 0 then 1 else 0) |> List.sum

let countEvenNumbersFilter list =
    list |> List.filter (fun number -> number % 2 = 0) |> List.length

let countEvenNumbersFold list =
    list |> List.fold (fun acc number -> if number % 2 = 0 then acc + 1 else acc) 0