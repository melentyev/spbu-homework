module Intersect

open System

type Geom = 
    | NoPoint
    | Point of float * float
    | Line  of float * float   // уравнение прямой y = a*x+b
    | VerticalLine of float    // вертикальная прямая проходящая через x
    | LineSegment of (float * float) * (float * float) // отрезок
    | Intersect of Geom * Geom // пересечение двух множеств 

type GenLine = float * float * float // Ax + By + C = 0

let eps = 1e-9

let (?=) (x:float) y = Math.Abs(x - y) < eps

let line x y x' y' =  (y - y', x' - x, x * y' - x' * y)

let isOnLine (A, B, C) x y = A * x + B * y + C ?= 0.0

let rec resolveIntersections g = 
    
    
    let boundedBy (x1, y1, x2, y2) x y =
        let x', y' = min x1 x2, min y1 y2
        let x'', y'' = max x1 x2, max y1 y2 
        x >= x' && x <= x'' && y >= y' && y <= y''
    let geom2line = function 
        | Line (a, b) -> (a, -1.0, b)
        | VerticalLine (x) -> (1.0, 0.0, -x)
    let linesIntersection a' b' = 
        let (A, B, C) = geom2line a'
        let (A', B', C') = geom2line b'
        NoPoint
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
                if isOnLine (line x' y' x'' y'' ) x y 
                    && boundedBy (x', y', x'', y'') x y then Point(x, y) else NoPoint
            | _ -> failwith "_"
        | l1, l2 -> linesIntersection l1 l2
    match g with
    | Intersect (a, b) -> intersect' a b
    | x -> x