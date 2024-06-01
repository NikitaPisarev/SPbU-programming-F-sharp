module BracketSequence

open System.Collections.Generic

let isBalanced (s: string) : bool =
    let matchingBracket =
        function
        | ')' -> Some '('
        | ']' -> Some '['
        | '}' -> Some '{'
        | _ -> None

    let isOpening =
        function
        | '('
        | '['
        | '{' -> true
        | _ -> false

    let stack = Stack<char>()
    let chars = s.ToCharArray()

    let processChar c =
        match matchingBracket c with
        | Some expectedOpen ->
            if stack.Count > 0 && stack.Peek() = expectedOpen then
                stack.Pop() |> ignore
                true
            else
                false
        | None ->
            if isOpening c then
                stack.Push(c)

            true

    chars |> Array.forall processChar && stack.Count = 0
