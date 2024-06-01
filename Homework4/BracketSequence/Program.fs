module BracketSequence

let bracets = [ ('(', ')'); ('[', ']'); ('{', '}') ]

let isOpeningBracket x =
    bracets |> List.exists (fun (o, _) -> o = x)

let isClosingBracket x =
    bracets |> List.exists (fun (_, c) -> c = x)

let isBracetsPair x y = List.contains (x, y) bracets

let isBalanced (str: string) =
    let rec helper (stack, seq) =
        match seq with
        | [] -> List.isEmpty stack
        | h :: seqTail when isOpeningBracket h -> helper (h :: stack, seqTail)
        | h :: seqTail when isClosingBracket h ->
            match stack with
            | [] -> false
            | stackHead :: stackTail when isBracetsPair stackHead h -> helper (stackTail, seqTail)
            | _ -> false
        | _ :: seqTail -> helper (stack, seqTail)

    helper ([], List.ofSeq str)
