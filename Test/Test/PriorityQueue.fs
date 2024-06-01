module Test.PriorityQueue

open System

// Define a generic PriorityQueue type
type PriorityQueue<'a> = 
    { mutable elements: ('a * int) list; mutable head: int }

// Create a new, empty PriorityQueue
let createPriorityQueue() = 
    { elements = []; head = 0 }

// Add a new element to the PriorityQueue with priority
let enqueue (pq: PriorityQueue<'a>) value priority = 
    pq.elements <- (value, priority) :: pq.elements

// Remove and return the highest-priority element from the PriorityQueue
let dequeue (pq: PriorityQueue<'a>) = 
    if pq.elements = [] then
        raise (InvalidOperationException "Empty priority queue.")
    else
        let sortedElements = pq.elements |> List.sortBy snd
        let (value, _) = List.head sortedElements
        pq.elements <- sortedElements |> List.tail
        value
