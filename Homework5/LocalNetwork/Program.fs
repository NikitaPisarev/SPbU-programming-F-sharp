module LocalNetwork

open System

type OperatingSystem(name: string, infectionProbability: float) =
    member val Name = name with get
    member val InfectionProbability = infectionProbability with get

type Computer(id: int, os: OperatingSystem, isInfected: bool) =
    member val ID = id
    member val OS = os
    member val IsInfected = isInfected with get, set

type Network(computers: list<Computer>, adjacencyList: Map<int, list<int>>) =
    member val Computers = computers with get
    member val AdjacencyList = adjacencyList with get

    member this.Step() =
        let random = new Random()
        let mutable toBeInfected = []

        for source in this.Computers do
            match this.AdjacencyList.TryGetValue(source.ID) with
            | (true, neighbors) ->
                neighbors
                |> List.map (fun id -> this.Computers |> List.find (fun c -> c.ID = id))
                |> List.iter (fun target ->
                    if source.IsInfected && not target.IsInfected then
                        if random.NextDouble() < target.OS.InfectionProbability then
                            toBeInfected <- target :: toBeInfected)
            | _ -> ()

        toBeInfected |> List.iter (fun computer -> computer.IsInfected <- true)

    member this.Run() =
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
