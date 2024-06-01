module LocalNetwork.Tests

open NUnit.Framework
open FsUnit
open LocalNetwork

[<Test>]
let ``Infection should spread to all if probability is 1`` () =
    let os = OperatingSystem("TestOS", 1.0)

    let computers =
        [ Computer(0, os, true) ] @ [ for i in 1..4 -> Computer(i, os, false) ]


    let adjacencyList =
        Map.ofList [ (0, [ 1 ]); (1, [ 0; 2 ]); (2, [ 1; 3 ]); (3, [ 2; 4 ]); (4, [ 3 ]) ]

    let network = Network(computers, adjacencyList)

    network.Run()

    computers |> List.forall (fun c -> c.IsInfected) |> should equal true

[<Test>]
let ``No infection should spread if probability is 0`` () =
    let os = OperatingSystem("TestOS", 0.0)

    let computers =
        [ Computer(0, os, true) ] @ [ for i in 1..4 -> Computer(i, os, false) ]

    let adjacencyList =
        Map.ofList [ (0, [ 1 ]); (1, [ 0; 2 ]); (2, [ 1; 3 ]); (3, [ 2; 4 ]); (4, [ 3 ]) ]

    let network = Network(computers, adjacencyList)

    network.Run()
    computers.[0].IsInfected |> should equal true

    computers
    |> List.skip 1
    |> List.forall (fun c -> not c.IsInfected)
    |> should equal true

[<Test>]
let ``Simple example`` () =
    let osWindows = OperatingSystem("Windows", 0.4)
    let osLinux = OperatingSystem("Linux", 0.2)
    let osMacOS = OperatingSystem("MacOS", 0.0)

    let computers =
        [ Computer(0, osWindows, false)
          Computer(1, osLinux, true)
          Computer(2, osMacOS, false)
          Computer(3, osWindows, false) ]

    let adjacencyList =
        Map.ofList [ (0, [ 1 ]); (1, [ 0; 2 ]); (2, [ 1; 3 ]); (3, [ 2 ]) ]

    let network = Network(computers, adjacencyList)
    network.Run()

    Assert.That(
        computers[0].IsInfected
        && computers[1].IsInfected
        && not computers[2].IsInfected
        && not computers[3].IsInfected
    )
