open Intersect
open NUnit.Framework
open FsUnit

[<TestFixture>]
module Tests = 
    let test a b = (resolveIntersections a == b) |> should equal true
    [<Test>]
    let ``equal sets``() =
        let expected = Point(1.0, 1.0)
        let data = Intersect(Point(1.0, 1.0), Intersect(VerticalLine(1.0), Line(0.0, 1.0) ) )
        test data expected
    [<Test>]
    let ``different sets``() =
        let expected = NoPoint
        let data = Intersect(Point(1.0, 1.0), Point(2.0, 2.0)) 
        test data expected
    [<Test>]
    let ``complicated1``() =
        let expected = Point(-2.0, 1.0)
        let data = Intersect(LineSegment((-3.0, 1.0), (-1.0, 1.0)), 
                             Intersect(Line(2.0, 5.0), Line(3.0, 7.0) ) ) 
        test data expected
    [<Test>]
    let ``complicated2``() =
        let expected = NoPoint
        let data = Intersect(LineSegment((-3.0, 1.0), (-2.5, 1.0)), 
                             Intersect(Line(2.0, 5.0), Line(3.0, 7.0) ) ) 
        test data expected
    [<Test>]
    let ``2 segments``() =
        let expected = LineSegment((-1.0, -1.0), (0.0, 0.0))
        let data = Intersect(LineSegment((-3.0, -3.0), (0.0, 0.0)), 
                             LineSegment((2.0, 2.0), (-1.0, -1.0) ) ) 
        test data expected
    [<Test>]
    let ``2 segments reversed`` () = 
        let expected = LineSegment((0.0, 0.0), (-1.0, -1.0))
        let data = Intersect(LineSegment((-3.0, -3.0), (0.0, 0.0)), 
                             LineSegment((2.0, 2.0), (-1.0, -1.0) ) ) 
        test data expected

let a = Tests.``2 segments reversed``()