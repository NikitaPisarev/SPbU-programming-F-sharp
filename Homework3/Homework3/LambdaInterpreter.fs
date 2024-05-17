module Homework3.LambdaInterpreter

type LambdaTerm =
    | Var of string
    | App of LambdaTerm * LambdaTerm
    | Lam of string * LambdaTerm

let rec freeVars term =
    match term with
    | Var x -> Set.singleton x
    | App(t1, t2) -> Set.union (freeVars t1) (freeVars t2)
    | Lam(x, t) -> Set.remove x (freeVars t)

let isFreeVar var term = Set.contains var (freeVars term)

let rec freshVar usedVars =
    let rec nextVar v =
        if Set.contains v usedVars then nextVar (v + "'") else v
    nextVar "x"

let rec substitute var replacement term =
    match term with
    | Var x when x = var -> replacement
    | Var _ -> term
    | App(t1, t2) -> App(substitute var replacement t1, substitute var replacement t2)
    | Lam(x, t) when x = var -> term
    | Lam(x, t) when not (isFreeVar x replacement) || not (isFreeVar var t) ->
        Lam(x, substitute var replacement t)
    | Lam(x, t) ->
        let newVar = freshVar (Set.union (freeVars t) (freeVars replacement))
        Lam(newVar, substitute var (substitute x t (Var newVar)) replacement)

let rec betaReduce term =
    match term with
    | Var x -> term
    | Lam(x, t) -> Lam(x, betaReduce t)
    | App(t1, t2) ->
        match t1 with
        | Lam(x, t) -> substitute x t2 t
        | _ -> App(betaReduce t1, t2)

let rec eval term =
    let reduced = betaReduce term
    if reduced = term then term else eval reduced

let printTerm term =
    let rec print t =
        match t with
        | Var x -> x
        | Lam(x, e) -> sprintf "λ%s.%s" x (print e)
        | App(e1, e2) ->
            let s1 = match e1 with
                        | Lam _ -> sprintf "(%s)" (print e1)
                        | _ -> print e1
            let s2 = match e2 with
                        | App _ | Lam _ -> sprintf "(%s)" (print e2)
                        | _ -> print e2
            sprintf "%s %s" s1 s2
    print term

let K = Lam ("x", Lam ("y", Var "x"))
let S = Lam ("x", Lam ("y", Lam ("z", App (App (Var "x", Var "z"), App (Var "y", Var "z")))))
let example = App (App (S, K), K)

printfn "Original: %s" (printTerm example)
let reducedExample = eval example
printfn "Reduced: %s" (printTerm reducedExample)