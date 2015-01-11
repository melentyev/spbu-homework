open System
open System.Collections.Generic

// i - row, j - column

let seed = 1488
let freeCell = 0
let invalid = -1

let neighboursCount = 6;

let moves0 = [| (0, 1); (1, 1); (1, 0); (1, -1); (0, -1); (-1, 0) |]   //even col: right-up,right-down,down,left-down,left-up,up
let moves1 = [| (-1, 1); (0, 1); (1, 0); (0, -1); (-1, -1); (-1, 0) |] //odd  col: right-up,right-down,down,left-down,left-up,up

let even n = n % 2 = 0
let odd = not << even 

module Moves = 
    (*let ru (i, j) = let di, dj = (if even j then moves0 else moves1).[0] in i + di, j + dj
    let rd (i, j) = let di, dj = (if even j then moves0 else moves1).[1] in i + di, j + dj
    let d  (i, j) = let di, dj = (if even j then moves0 else moves1).[2] in i + di, j + dj
    let ld (i, j) = let di, dj = (if even j then moves0 else moves1).[3] in i + di, j + dj
    let lu (i, j) = let di, dj = (if even j then moves0 else moves1).[4] in i + di, j + dj
    let u  (i, j) = let di, dj = (if even j then moves0 else moves1).[5] in i + di, j + dj*)
    let neighbors (i, j) = (if even j then moves0 else moves1) |> Seq.map (fun (di, dj) -> i + di, j + dj) |> Seq.toArray

let countPoints = function 
    | x when x < 3 -> 0
    | 3 -> 3
    | 4 -> 6
    | 5 -> 10
    | x -> x * (x + 1) / 2

let game n m  colors seed  log = 
    let rnd = new System.Random(seed)
    let field = Array2D.create n m 0
    let nextVal() = rnd.Next(colors) + 1
    let cell (i, j) = field.[i, j]
    let setCell (i, j) v = field.[i, j] <- v
    let cells = seq { for i in (n - 1)  .. -1 .. 0 do for j in 0 .. m - 1 do yield (i, j) }
    let Array2DCopyTo (dest: int[,]) (src: int[,]) =
        cells |> Seq.iter (fun (i, j) -> dest.[i, j] <- src.[i, j])
    let printFieldM l = 
        if log then 
            for i in 0 .. n - 1 do
                for j in 0 .. m - 1 do
                    if odd j then printf (if List.tryFind ((=) (i,j)) l |> Option.isSome then "%d." else "%d ") field.[i, j] else printf "  "
                printfn ""
                for j in 0 .. m - 1 do 
                    if even j then printf (if List.tryFind ((=) (i,j)) l |> Option.isSome then "%d." else "%d ") field.[i, j] else printf "  "
                printfn ""
            printfn ""
        else ()
    let printField () = printFieldM List.empty
    let inside (i, j) = i >= 0 && j >= 0 && i < n && j < m
    
    let fillDown () = 
        for j in 0 .. m - 1 do
            let d = seq { (n-1) .. -1 .. 1 } |> Seq.tryFind (fun i -> cell (i, j) = freeCell)
            if Option.isSome d then
                let d = d.Value |> ref
                seq { (!d - 1) .. -1 .. 0 } 
                |> Seq.iter (fun i -> if cell (i, j) <> freeCell then field.[!d, j] <- cell (i, j); d := !d - 1)
                seq { !d .. -1 .. 0 } |> Seq.iter (fun i -> field.[i, j] <- freeCell)
    
    let fillEmptyCells () = 
        fillDown()
        printField()
        cells |> Seq.iter (fun (i, j) -> if field.[i, j] = freeCell then field.[i, j] <- nextVal() )

    let getRelative (i0, j0) = 
        let color = field.[i0, j0]
        let marks = new HashSet<_>()
        if(color = freeCell) then marks
        else 
            let rec dfs' (i, j) = 
                marks.Add (i,j) |> ignore
                Moves.neighbors (i, j) |> 
                    Seq.iter (fun (ni, nj) -> 
                        if inside (ni, nj) && field.[ni, nj] = color && not (marks.Contains (ni, nj) ) then dfs' (ni, nj) ) 
            dfs' (i0, j0)
            marks
         
    let rotateCells1 (a, b, c) = (c, a, b)
    let rotateCells2 (a, b, c) = (b, c, a)
    let validTriple (a, b, c) = List.forall inside [a; b; c]
    let goodTriple (a, b, c) = validTriple (a, b, c) && (cell a = cell b) && (cell b = cell c)

    let triples (i, j) =  
        let ns = Moves.neighbors (i, j)
        seq { for k in 0 .. neighboursCount - 1 -> (i, j), ns.[k], ns.[(k + 1) % neighboursCount] }
    let validTriples (i, j) = triples (i, j) |> Seq.filter validTriple     
    let goodTriples (i, j) = triples (i, j) |> Seq.filter goodTriple
                
    let killGroup (i, j) = 
        let relative = getRelative (i, j)
        if relative.Count >= 3 then 
            Seq.iter (fun (i, j) -> field.[i,j] <- freeCell) relative
            countPoints <| relative.Count 
        else 0
    
    let tryKill () = 
        let s = 
            cells 
            |> Seq.map (fun (i, j) -> 
                let s = goodTriples (i, j) 
                if Seq.isEmpty s then 0 
                else 
                    let value = killGroup (i, j)
                    printField ()
                    //fillEmptyCells ()
                    //printField ()
                    value)
            |> Seq.toArray
        printField ()
        s |> Seq.sum
    let multiKillTriples = tryKill
    
    let multiKillTriplesAll () = 
        Seq.initInfinite (fun _ -> let v = multiKillTriples () in fillEmptyCells (); v) |> Seq.takeWhile ((<) 0) |> Seq.sum
        //Seq.unfold (fun () -> let points = tryKill () in if points > 0 then Some (points, () ) else None) () |> Seq.sum
    let setCells (a, b, c) (na, nb, nc) = 
        let vna, vnb, vnc = cell na, cell nb, cell nc
        setCell a vna
        setCell b vnb
        setCell c vnc

    let calcTriple (a, b, c) = 
        let rotateCalcAndReturn (na, nb, nc) = 
            let field' = Array2D.copy field
            setCells (a, b, c) (na, nb, nc)
            printFieldM [a; b; c]
            let result = multiKillTriples()
            printField ()
            Array2DCopyTo field field'
            printField ()
            result
        let rot1 = rotateCells1 (a, b, c)
        let rot2 = rotateCells2 (a, b, c)
        let res1 = rotateCalcAndReturn rot1
        let res2 = rotateCalcAndReturn rot2
        [res1, rot1; res2, rot2] |> List.maxBy fst
                
    let findBestRotate () =  // ((a, b, c), (a', b', c'), value) - возвращает тройку, как ее повернуть, и значение
        let s = 
            cells 
            |> Seq.choose
                (fun (i, j) ->
                    let ns = validTriples (i, j) |> Seq.toArray
                    let s = 
                        ns 
                        |> Seq.mapi (fun k tr -> 
                            //if i = 2 & j = 3 && k = 2 then ()
                            k, calcTriple tr)
                        |> Seq.filter (fun (_, (bestval, _ ) ) -> bestval > 0) 
                        |> Seq.toArray
                    if Seq.isEmpty s then None 
                    else 
                        let (k, (bestval, (na, nb, nc) ) ) = Seq.maxBy snd s
                        printFieldM [na; nb; nc]
                        let from = ns.[k]
                        Some <| ( (from, (na, nb, nc) ), bestval) 
                ) 
            |> Seq.toArray 
        if Array.isEmpty s then None else Some <| Array.maxBy snd s
    
    printField () 
    fillEmptyCells ()
    printField ()
    multiKillTriplesAll() |> ignore
    printField ()

    Seq.initInfinite (fun _ -> findBestRotate ()) 
    |> Seq.takeWhile Option.isSome
    |> Seq.map Option.get
    |> Seq.map  
        (fun (((a, b, c), (na, nb, nc)), value) -> 
            printFieldM [a; b; c]
            setCells (a, b, c) (na, nb, nc) 
            printFieldM [a; b; c]
            let value = multiKillTriplesAll ()
            printField ()
            printfn "Step done"
            value)
    |> Seq.sum

[<EntryPoint>]
let main argv = 
    //let res = game 7 6 7 1487 false
    let a = [|1; 2; 3|]
    let s = String.Join(",", a)
    //let di = System.IO.Directory.CreateDirectory(@"C:\Users\user\mdir1\");
    //printfn "res: %d" res
    System.Console.ReadKey() |> ignore
    0