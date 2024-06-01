module Test.Tests

open System
open NUnit.Framework
open FsUnit
open Test.Fibonacci
open Test.PriorityQueue

[<TestFixture>]
type EvenFibonacciSumTests() =
    [<Test>]
    member this.``EvenFibonacciSum returns correct sum for small inputs``() =
        evenFibonacciSum 3 |> should equal 2
        evenFibonacciSum 8 |> should equal 10
        evenFibonacciSum 34 |> should equal 44

    [<Test>]
    member this.``EvenFibonacciSum returns correct sum for inputs less than 2``() =
        evenFibonacciSum 1 |> should equal 0
        evenFibonacciSum 0 |> should equal 0


[<TestFixture>]
type PriorityQueueTests() =
    [<Test>]
    member this.``Enqueue and dequeue single element``() =
        let pq = createPriorityQueue()
        enqueue pq 1 1
        pq |> dequeue |> should equal 1

    [<Test>]
    member this.``Enqueue and dequeue multiple elements``() =
        let pq = createPriorityQueue()
        enqueue pq "High" 10
        enqueue pq "Low" 1
        enqueue pq "Medium" 5
        pq |> dequeue |> should equal "Low"
        pq |> dequeue |> should equal "Medium"
        pq |> dequeue |> should equal "High"

    [<Test>]
    member this.``Dequeue from empty queue throws exception``() =
        let pq = createPriorityQueue()
        (fun () -> pq |> dequeue) |> should throw typeof<InvalidOperationException>
