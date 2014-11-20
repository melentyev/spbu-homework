open System
open System.Collections.Generic

let seed = 1488
let colors = 5
let moves = [| (-1, 1); (0, 1); (1, 0); (0, -1); (-1, -1); (-1, 0) |] //right-up,right-down,down,left-down,left-up,up
module Moves = 
    let ru (i, j) = let di, dj  = moves.[0] in i + di, j + dj
    let rd (i, j) = let di, dj  = moves.[1] in i + di, j + dj
    let d  (i, j) = let di, dj  = moves.[2] in i + di, j + dj
    let ld (i, j) = let di, dj  = moves.[3] in i + di, j + dj
    let lu (i, j) = let di, dj  = moves.[4] in i + di, j + dj
    let u  (i, j) = let di, dj  = moves.[5] in i + di, j + dj
let countPoints = function 
    | 1 -> 0
    | 2 -> 0
    | 3 -> 3
    | 4 -> 6
    | 5 -> 10
    | x -> x * (x + 1) / 2

let freeCell = 0

let game n m seed = 
    let rnd = new System.Random(seed)
    let field = Array2D.create n m 0
    let nextVal() = 
        rnd.Next(colors) + 1
    let cells = seq { for i in 0 .. n - 1 do for j in 0 .. m - 1 do yield (i, j) }
    let printField () = 
        for i in 0 .. n - 1 do
            for j in 0 .. m - 1 do
                printf "%d " field.[i, j]
            printfn ""
    let inside (i, j) = i >= 0 && j >= 0 && i < n && j < m
    let fillEmptyCells () = 
        cells |> Seq.iter (fun (i, j) -> if field.[i, j] = freeCell then  field.[i, j] <- nextVal() ) 
    let getRelative (i0, j0) = 
        let color = field.[i0, j0]
        let marks = new HashSet<_>()
        if(color = freeCell) then marks
        else 
            let rec dfs' (i, j) = 
                marks.Add (i,j) |> ignore
                moves |> 
                    Seq.iter (fun (di, dj) -> 
                        let ni, nj = i + di, j + dj
                        if inside (ni, nj) && field.[ni, nj] = color && not (marks.Contains (ni, nj) ) then dfs' (ni, nj) ) 
            dfs' (i0, j0)
            marks
            
    let killGroup (i, j) = 
        let cells = getRelative (i, j)
        Seq.iter (fun (i, j) -> field.[i,j] <- freeCell) cells
        countPoints <| cells.Count 
    let killAllGroups () = cells |> Seq.map killGroup |> Seq.sum
    fillEmptyCells ()
    while killAllGroups () > 0 do
        fillEmptyCells ()
    let mutable isDone = false
    let rotationCells1 p = (Moves.ru p, Moves.rd p, p)
    let rotationCells2 p = (Moves.ld p, Moves.lu p, p)
    let rotateCorrect1 p = let a, b, c = rotationCells1 p in List.forall inside [a; b; c]
    let rotateCorrect2 p = let a, b, c = rotationCells2 p in List.forall inside [a; b; c]
        
    let findBestRotate () = 
        cells |> Seq.iter (fun (i, j) -> if field.[i, j] = freeCell then  field.[i, j] <- nextVal() ) 
    while not isDone do
        let r = findBestRotate ()
        isDone <- true
        ()
    0



[<EntryPoint>]
let main argv = 
    printfn "%A" argv
    0 // return an integer exit code
