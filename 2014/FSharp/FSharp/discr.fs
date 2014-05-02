//module discr
open System.Collections.Generic
(*
let n = 8

let kuhn (g: int list list) =  
    let mt = Array.create n -1
    let rec try_kuhn (used:bool []) (mt:int []) v = 
        if used.[v] then false
        else
            used.[v] <- true
            let found  = 
                g.[v] |> 
                List.tryFind (fun _to -> 
                    if (mt.[_to] = -1 || try_kuhn used mt mt.[_to]) then 
                        mt.[_to] <- v
                        true
                    else 
                        false)
            match found with None -> false | _ -> true
    List.iter(fun v -> 
        let used = Array.zeroCreate n
        try_kuhn used mt v |> ignore) [0 .. (n - 1)]
    
    let ls = 
        mt 
        |> Array.mapi(fun i v -> (i, v)) 
        |> Array.filter(fun (i, v) -> v <> -1) 
        |> Array.toList
    (ls, mt)
            
let m_to_g (m:int [][]) = 
    m 
        |> Array.mapi(fun i v ->
            v 
            |> Array.mapi(fun j v -> (j, v)) 
            |> Array.filter(fun (j, v) -> v = 0) 
            |> Array.map(fun (j, v) -> j)  
            |> Array.toList )
        |> Array.toList
             
let findL V1 V2 (m:int [][]) = 
    [ for i = 0 to n - 1 do 
        for j = 0 to n - 1 do 
            if (Option.isNone <| List.tryFind ( (=) i) V1 ) 
                && (Option.isNone <| List.tryFind ( (=) j) V2 ) then yield m.[i].[j] ]
        |> List.min
let addL L V1 V2 (m:int[][]) = 
    for i = 0 to n - 1 do
        for j = 0 to n - 1 do 
            if (Option.isSome <| List.tryFind ( (=) i) V1 ) then m.[i].[j] <- m.[i].[j] + L
            if (Option.isNone <| List.tryFind ( (=) j) V2 ) then m.[i].[j] <- m.[i].[j] - L            
    ()
let marks (p: (int*int) list) (mt: int []) (g: int list list) = 
    let V1 = Array.zeroCreate n
    let V2 = Array.zeroCreate n
    let recurse v = 
        g.[v] 
        |> List.iter(fun w -> 
            if not V2.[w] then 
                V2.[w] <- true
                 )
    for i = 0 to n - 1 do
        if mt.[i] <> -1 then V1.[mt.[i]] <- true 

let printarr n (m:int[][]) = 
    printfn "%A" m
let m1 = [| 
    [| 0; 0; 0; 0; 0; 0; 0; 0|]
    [| 0; 7; 7; 10; 10; 10; 10; 10|]
    [| 0; 11; 20; 23; 26; 37; 38; 42|]
    [| 0; 13; 13; 66; 75; 84; 93; 102|]
    [| 0; 13; 13; 10; 14; 18; 22; 26|]
    [| 0; 18; 18; 39; 43; 47; 51; 60|]
    [| 0; 18; 19; 18; 27; 66; 85; 89|]
    [| 0; 19; 19; 26; 27; 28; 37; 46|]
|]

let g1 = m_to_g m1
let p1, mt1 = kuhn g1
let V1 = marks p1 mt1 g1
*)
open System
[<STAThread>]
[<EntryPoint>]
let main argv = 
    0