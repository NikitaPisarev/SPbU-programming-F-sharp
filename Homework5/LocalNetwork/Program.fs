module NetworkSimulation

open System

type OperatingSystem(name: string, infectionProbability: float) =
    member this.Name = name
    member this.InfectionProbability = infectionProbability

type Computer(id: int, os: OperatingSystem, initiallyInfected: bool) =
    let mutable isInfected = initiallyInfected
    member this.ID = id
    member this.OS = os
    member this.IsInfected = isInfected
    member this.Infect() = isInfected <- true

type Network(computers: list<Computer>, adjacencyList: Map<int, list<int>>) =
    member val Computers = computers with get
    member val AdjacencyList = adjacencyList with get

    member this.Step() =
        let random = new Random()

        let tryInfect (source: Computer) (target: Computer) =
            if source.IsInfected && not target.IsInfected then
                if random.NextDouble() < target.OS.InfectionProbability then
                    target.Infect()

        for source in this.Computers do
            match this.AdjacencyList.TryGetValue(source.ID) with
            | (true, neighbors) ->
                neighbors
                |> List.map (fun id -> this.Computers |> List.find (fun c -> c.ID = id))
                |> List.iter (fun target -> tryInfect source target)
            | _ -> ()

    member this.RunSimulation() =
        let mutable stillChanging = true

        while stillChanging do
            this.PrintStatus()
            this.Step()

            let canAnyInfect =
                this.Computers |> List.exists (fun pc -> not pc.IsInfected && this.CanInfect pc)

            if not canAnyInfect then
                stillChanging <- false

            printfn "------------------------------"

        this.PrintStatus()


    member private this.CanInfect(computer: Computer) =
        if computer.OS.InfectionProbability = 0.0 then
            false
        else
            match this.AdjacencyList.TryGetValue(computer.ID) with
            | (true, neighbors) ->
                neighbors
                |> List.exists (fun neighborId ->
                    let neighbor = this.Computers |> List.find (fun c -> c.ID = neighborId)
                    neighbor.IsInfected)
            | _ -> false

    member this.PrintStatus() =
        for computer in this.Computers do
            printfn "Computer %d: OS = %s, Infected = %b" computer.ID computer.OS.Name computer.IsInfected

let setupAndRunSimulation () =
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
    network.RunSimulation()

setupAndRunSimulation ()
