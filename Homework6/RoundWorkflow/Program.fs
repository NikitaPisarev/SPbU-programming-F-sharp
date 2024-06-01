module RoundWorkflow

open System

type RoundBuilder(precision: int) =
    member this.Bind(x: double, f) = f (Math.Round(x, precision))
    member this.Return(x: double) = Math.Round(x, precision)

let rounding precision = RoundBuilder(precision)
