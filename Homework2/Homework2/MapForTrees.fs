module MapForTrees

type Tree<'a> =
    | Node of 'a * Tree<'a> * Tree<'a>
    | Empty

let rec map tree func =
    match tree with 
    | Empty -> Empty
    | Node (value, l, r) -> Node(func value, map l func, map r func)