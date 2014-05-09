//module discr
open System.Collections.Generic


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
    let mtl = Array.create n -1
    mt |> Array.iteri (fun i v -> if (v <> -1) then mtl.[v] <- i)
    (mtl, mt)
            
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
let marks (mtl: int []) (mtr: int []) (g: int list list) = 
    let V1 = Array.zeroCreate n
    let V2 = Array.zeroCreate n
    [1 .. 20] 
    |> List.iter (fun _ -> 
        Array.iteri (fun i v -> 
            if ( v = -1 || V1.[i] = true) then 
                V1.[i] <- true
                List.iter (fun To -> V2.[To] <-  true) g.[i]
        ) mtl
        Array.iteri (fun i v -> 
            if (V2.[i] = true && v <> -1) then 
                V1.[v] <- true
        ) mtr
    )
    (V1, V2)

let printarr (m:int[][]) = 
    m 
    |> Array.map(fun inner -> 
        "  [| " + (inner 
        |> Array.map(fun x -> x.ToString() + "; " ) 
        |> Array.fold (+) ""
        ) + "|]\n" ) 
    |> Array.fold (+) ""  
    |> printfn "[|\n  %s\n|]" 
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
let mtl1, mtr1 = kuhn g1
let V11, V12 = marks mtl1 mtr1 g1
let M11 = 
    V11 
    |> Array.mapi (fun i v -> (i, v) ) 
    |> Array.filter (not << snd)
    |> Array.map fst
    |> Array.toList
let M12 = 
    V12
    |> Array.mapi (fun i v -> (i, v) ) 
    |> Array.filter snd
    |> Array.map fst
    |> Array.toList

let L1 = findL M11 M12 m1
addL L1 M11 M12 m1
printarr m1


let g2 = m_to_g m1
let mtl2, mtr2 = kuhn g2
let V21, V22 = marks mtl2 mtr2 g2
let M21 = 
    V21 
    |> Array.mapi (fun i v -> (i, v) ) 
    |> Array.filter (not << snd)
    |> Array.map fst
    |> Array.toList
let M22 = 
    V22
    |> Array.mapi (fun i v -> (i, v) ) 
    |> Array.filter snd
    |> Array.map fst
    |> Array.toList

let L2 = findL M21 M22 m1
addL L2 M21 M22 m1
printarr m1


let m4 = [|
  [| 11; 0; 0; 0; 0; 0; 0; 0; |];
  [| 4; 0; 0; 3; 3; 3; 3; 3; |];
  [| 0; 0; 9; 12; 15; 26; 27; 31; |];
  [| 0; 2; 2; 55; 64; 73; 82; 91; |];
  [| 1; 3; 3; 0; 4; 8; 12; 16; |];
  [| 0; 7; 7; 28; 32; 36; 40; 49; |];
  [| 0; 7; 8; 7; 16; 55; 74; 78; |];
  [| 0; 8; 8; 15; 16; 17; 26; 35; |];
|];

let act m1 = 
    let g3 = m_to_g m1
    printfn "%A" g3
    let mtl3, mtr3 = kuhn g3
    printfn "mtl3 mtr3: %A %A" mtl3 mtr3
    let V31, V32 = marks mtl3 mtr3 g3
    printfn "mtl3 mtr3: %A %A" mtl3 mtr3
    let M31 = 
        V31 
        |> Array.mapi (fun i v -> (i, v) ) 
        |> Array.filter (not << snd)
        |> Array.map fst
        |> Array.toList
    let M32 = 
        V32
        |> Array.mapi (fun i v -> (i, v) ) 
        |> Array.filter snd
        |> Array.map fst
        |> Array.toList
    printfn "M1 M2: %A %A" M31 M32
    let L3 = findL M31 M32 m1
    printfn "L3: %A" L3
    addL L3 M31 M32 m1
    printarr m1


act m4

act m4

let test = new Map<int,int> ([])


kuhn [[1; 2; 3; 4; 5; 6; 7]; [1; 2]; [1]; [0; 1; 2]; [3]; [0]; [0]; [0]]