module HW1 = 
    let rec sum = function 
        | [] -> 0
        | h :: t -> h + sum t
    let rec append a b = 
        match a with 
        | [] -> b
        | h :: t -> h :: append t b
    let rec addToEnd x = function 
        | [] -> [x]
        | h :: tl -> h :: addToEnd x tl

    let rec filter f = function 
        | [] -> []
        | h :: tl -> (if f h then [h] else []) @ filter f tl
 
    let genSqr n = 
        Seq.initInfinite <| fun x -> (x + 1) * (x + 1)
        |> Seq.takeWhile ( (>=) n)
        |> Seq.toList
    let tests() =
        printfn "HW1.tests:" 
        sum [ 1 .. 10] 
        |> printfn "sum [ 1 .. 10]: %A"
        append [1 .. 5] [6..10] 
        |> printfn "append [1 .. 5] [6..10]: %A"
        addToEnd 6 [1 .. 5]
        |> printfn "addToEnd 6 [1 .. 5]: %A"
        filter ( (<=) 0 ) [ -5 .. 5 ]
        |> printfn "filter ( (<) 0 ) [ -5 .. 5 ]: %A" 
        genSqr 24
        |> printfn "genSqr 24: %A"
        genSqr 25
        |> printfn "genSqr 25: %A"
        printfn ""
module HW2 = 
    open System
    let biggestDelimiter n = 
        let isPrime n = 
            [ 2L .. (int64 << sqrt << float ) n + 1L ]
            |> List.fold (fun flag x -> flag && n % x <> 0L) true
        let delims = 
            [ 1L .. (int64 << sqrt << float ) n + 1L ]
            |> List.filter (fun x -> n % x = 0L)
        delims @ List.map (fun d -> n / d) delims
        |> List.filter isPrime
        |> List.sort
        |> List.rev
        |> List.head
    
    let sumfibs n = 
        Seq.concat [Seq.singleton 1; Seq.unfold (fun (x, y) -> Some(x + y, (y, x + y) ) ) (0, 1) ]
        |> Seq.takeWhile ((>=)n)
        |> Seq.sum
        
    let factorialDigitsSum n = 
        let chars (s:string) = s.ToCharArray() 
        seq { 1I .. n } 
        |> Seq.fold (*) 1I
        |> string
        |> chars
        |> Array.map (fun c -> Int32.Parse <| string c)
        |> Array.sum


    type Geom = 
        | NoPoint
        | Point of float * float
        | Line  of float * float   // уравнение прямой y = a*x+b
        | VerticalLine of float    // вертикальная прямая проходящая через x
        | LineSegment of (float * float) * (float * float) // отрезок
        | Intersect of Geom * Geom // пересечение двух множеств
    let rec resolveIntersections g = 
        let line x y x' y' = (y - y', x' - x, x * y' - x' * y)
        let onLine (A, B, C) x y = A * x + B * y + C = 0.0
        let boundedBy (x1, y1, x2, y2) x y =
            let x', y' = min x1 x2, min y1 y2
            let x'', y'' = max x1 x2, max y1 y2 
            x >= x' && x <= x'' && y >= y' && y <= y''
        let intersect' a' b'  =
            let a = resolveIntersections a'
            let b = resolveIntersections b'
            match a, b with
            | NoPoint, _ | _, NoPoint -> NoPoint
            | Point(x, y) as p, Point(x', y') -> if x = x' && y = y' then p else NoPoint
            | (Point(x, y), v) | (v, Point(x, y) ) -> 
                match v with 
                | VerticalLine x' -> if x = x' then Point(x, y) else NoPoint
                | Line (a, b) -> if y = a * x + b then Point(x, y) else NoPoint
                | LineSegment ( (x', y'), (x'', y'' ) )  -> 
                    if onLine (line x' y' x'' y'' ) x y && boundedBy (x', y', x'', y'') x y then Point(x, y) else NoPoint
            
                 
        match g with
        | Intersect (a, b) -> intersect' a b
        | x -> x
    let tests() = 
        printfn "HW2.tests()" 
        biggestDelimiter 600851475143L
        |> printfn "biggestDelimiter 600851475143L: %A"
        sumfibs 4000000
        |> printfn "sumfibs 4000000: %A" 
        factorialDigitsSum 100I
        |> printfn "factorialDigitsSum 100: %A" 
        printfn ""


[<EntryPoint>]
let main argv = 
    HW1.tests()
    HW2.tests()
    System.Console.ReadKey() |> ignore
    0 // return an integer exit code
