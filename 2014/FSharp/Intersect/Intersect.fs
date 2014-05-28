module Intersect
open System


let (>>=) m f = Option.bind f m

let eps = 1e-10
let (?=) (x:float) (y:float) = abs(x - y) < eps
let (?==) (x:float*float) (y:float*float) = fst x ?= fst y && snd x ?= snd y

type GenLine = float * float * float // Ax + By + C = 0

type Geom = 
    | NoPoint
    | Point of float * float
    | Line  of float * float   // уравнение прямой y = a*x+b
    | VerticalLine of float    // вертикальная прямая проходящая через x
    | LineSegment of (float * float) * (float * float) // отрезок
    | Intersect of Geom * Geom // пересечение двух множеств 

let (==) (g1: Geom) (g2: Geom) = 
    match g1, g2 with 
    | NoPoint, NoPoint -> true
    | Point(x, y), Point(x', y') when x ?= x' && y ?= y' -> true
    | Line(a, b), Line(a', b') when a ?= a' && b ?= b' -> true
    | VerticalLine x, VerticalLine x' when x ?= x' -> true
    | LineSegment(a, b), LineSegment(c, d) when a ?== c && b ?== d || a ?== d && b ?== c -> true
    | _ -> false
    

let line (x, y) (x', y') = (y - y', x' - x, x * y' - x' * y)

let isOnLine (A, B, C) x y = A * x + B * y + C ?= 0.0

let normalizeRect (x1, y1) (x2, y2) = (min x1 x2, min y1 y2), (max x1 x2, max y1 y2 )

let insideRect (x1, y1, x2, y2) x y =
    let (x', y'), (x'', y'') = normalizeRect (x1, y1) (x2, y2)
    x >= x' && x <= x'' && y >= y' && y <= y''

let segInt1d a b c d = 
    let x, y = max a c, min b d
    if x > y then None else Some (x, y)

let rectIntersect (x1, y1) (x2, y2) (x1', y1') (x2', y2') = 
    let (x1, y1), (x2, y2) = normalizeRect (x1, y1) (x2, y2)
    let (x1', y1'), (x2', y2') = normalizeRect (x1', y1') (x2', y2')
    segInt1d x1 x2 x1' x2' >>= fun (x1, x2) -> 
    segInt1d y1 y2 y1' y2' >>= fun (y1, y2) -> 
    Some(x1, y1, x2, y2)

let geom2line = function 
    | Line (a, b) -> (a, -1.0, b)
    | VerticalLine (x) -> (1.0, 0.0, -x)
    | _ -> failwith "fail"

let (| NoInt | IntInf | IntPoint |) ((A, B, C), (A', B', C')) = 
    let det a b c d = a * d - b * c
    let d = det A B A' B'
    let dx = - (det C B C' B')
    let dy = - (det A C A' C')
    if d ?= 0.0 then 
        if (dx ?= 0.0 && dy ?= 0.0) then IntInf
        else NoInt
    else IntPoint (dx / d, dy / d)

let intersectWithPoint (x, y) = function
    | VerticalLine _ | Line _ as v -> if isOnLine (geom2line v) x y then Point(x, y) else NoPoint
    | LineSegment ( (x', y'), (x'', y'' ) )  -> 
        if isOnLine (line (x', y') (x'', y'') ) x y 
            && insideRect (x', y', x'', y'') x y then Point(x, y) else NoPoint
    | _ -> failwith "_"

let rec resolveIntersections g =  
    let intersect' a' b'  =
        let a = resolveIntersections a'
        let b = resolveIntersections b'
        match a, b with
        | NoPoint, _ | _, NoPoint -> NoPoint
        | Point(x, y) as p, Point(x', y') -> if (x, y) ?== (x', y') then p else NoPoint
        | (Point(x, y), v) | (v, Point(x, y) ) -> 
            intersectWithPoint (x, y) v
        | LineSegment ((x1, y1), (x2, y2)), LineSegment ((x1', y1'), (x2', y2')) -> 
            match line (x1, y1) (x2, y2), line (x1', y1') (x2', y2') with
            | IntInf -> 
                match rectIntersect (x1, y1) (x2, y2) (x1', y1') (x2', y2') with
                | Some (x1, y1, x2, y2) -> LineSegment ((x1, y1), (x2, y2))
                | None -> NoPoint
            | IntPoint (x, y) when insideRect (x1, y1, x2, y2) x y && insideRect (x1', y1', x2', y2') x y -> Point(x, y)
            | _ -> NoPoint
        | LineSegment ((x1, y1), (x2, y2)), v | v, LineSegment ((x1, y1), (x2, y2)) ->
            match geom2line v, line (x1, y1) (x2, y2) with 
            | IntInf -> LineSegment ((x1, y1), (x2, y2))
            | IntPoint (x, y) when insideRect (x1, y1, x2, y2) x y ->  Point(x, y)
            | _ -> NoPoint
        | v1, v2 -> 
            match geom2line v1, geom2line v2 with 
            | IntInf -> v1 
            | IntPoint (x, y) -> Point (x, y) 
            | _ -> NoPoint
    match g with
    | Intersect (a, b) -> intersect' a b
    | x -> x