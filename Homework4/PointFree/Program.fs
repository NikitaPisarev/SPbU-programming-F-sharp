module PointFree

let pointFreeFunc = List.map << (*)

// 1) List.map (fun y -> y * x) l
// 2) List.map ((*) x) l
// 3) List.map << (*)
