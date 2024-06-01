module PointFree.Tests

open NUnit.Framework
open FsUnit
open FsCheck

let func x l = List.map (fun y -> y * x) l

[<Test>]
let ``PointFree function should be equal actual``() =
    let equality x l = 
        pointFreeFunc x l = func x l
    Check.Quick equality