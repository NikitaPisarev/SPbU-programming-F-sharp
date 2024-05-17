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

