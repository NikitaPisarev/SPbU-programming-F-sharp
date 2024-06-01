module Homework3.LambdaInterpreter.Test

open FsUnit
open NUnit.Framework
open Homework3.LambdaInterpreter

[<Test>]
let ``Beta Reduction of simple application`` () =
    let expr = App(Lam("x", Var "x"), Var "y")
    let result = eval expr
    result |> should equal (Var "y")

[<Test>]
let ``Beta Reduction of application`` () =
    let functionExpr = Lam("x", Lam("y", App(Var "x", Var "y")))
    let I = Lam("z", Var "z")
    let expr = App(App(functionExpr, I), Var "a")
    let result = eval expr
    let expectedResult = Var "a"
    result |> should equal expectedResult

[<Test>]
let ``Alpha Conversion to avoid capture`` () =
    let expr = App(Lam("x", Lam("y", Var "x")), Var "y")
    let result = eval expr
    result |> should equal (Lam("x'", Var "y"))

[<Test>]
let ``Printing Lambda terms correctly`` () =
    let expr = Lam("x", App(Lam("y", Var "y"), Var "x"))
    let result = printTerm expr
    result |> should equal "λx.(λy.y) x"
