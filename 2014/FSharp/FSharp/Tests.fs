open Intersect
open NUnit.Framework
open FsUnit

[<TestFixture>]
module Tests = 
    [<Test>]
    let ``test equal sets``() =
        let expected = Point(1.0, 1.0)
        let data = Intersect(Point(1.0, 1.0), Intersect(VerticalLine(1.0), Line(0.0, 1.0) ) )
        (resolveIntersections data == expected) |> should equal true
    let ``test different sets``() =
        let expected = NoPoint
        let data = Intersect(Point(1.0, 1.0), Point(2.0, 2.0)) 
        (resolveIntersections data == expected) |> should equal true
    let ``test complicated1``() =
        let expected = NoPoint
        let data = Intersect(LineSegment((-3.0, 1.0), (-1.0, 1.0)), Intersect(Line(2.0, 5.0), Line(3.0, 7.0) ) ) 
        (resolveIntersections data == expected) |> should equal true
    let ``test complicated2``() =
        let expected = Point(-2.0, 1.0)
        let data = Intersect(LineSegment((-3.0, 1.0), (-2.5, 1.0)), Intersect(Line(2.0, 5.0), Line(3.0, 7.0) ) ) 
        (resolveIntersections data == expected) |> should equal false

let a = Tests.``test equal sets``()