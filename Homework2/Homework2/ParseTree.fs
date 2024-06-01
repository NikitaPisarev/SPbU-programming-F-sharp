module Homework2.ParseTree

type ParseTree<'a> =
    | Operand of 'a
    | Add of ParseTree<'a> * ParseTree<'a>
    | Subtract of ParseTree<'a> * ParseTree<'a>
    | Multiply of ParseTree<'a> * ParseTree<'a>
    | Modulo of ParseTree<'a> * ParseTree<'a>

let rec calculate parseTree = 
    match parseTree with
    | Operand value -> value
    | Add (left, right) -> calculate left + calculate right
    | Subtract (left, right) -> calculate left - calculate right
    | Multiply (left, right) -> calculate left * calculate right
    | Modulo (left, right) -> calculate left % calculate right
