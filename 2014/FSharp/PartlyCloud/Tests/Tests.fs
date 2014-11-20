namespace Tests
open NUnit.Framework
open FsUnit
open Abstract
open Cloud
open NSubstitute
open FsCheck
          

[<TestFixture>]
module Tests = 
    let moqCloud (dl: DaylightType) (ish: bool) (spd: int) (magic: IMagic) = 
        let daylight = Substitute.For<IDaylight>()
        let luminary = Substitute.For<ILuminary>() 
        let wind = Substitute.For<IWind>()
        daylight.Current.Returns dl                   |> ignore
        luminary.IsShining.Returns ish                |> ignore
        wind.Speed.Returns spd                        |> ignore
        new Cloud(daylight, luminary, wind, magic)

    let simpleTest (dl: DaylightType) (ish: bool) (spd: int) (ct: CreatureType) (isStork: bool)  = 
        let magic = Substitute.For<IMagic>()
        let cour = Substitute.For<ICourier>()
        magic.CallStork().Returns cour |> ignore
        magic.CallDaemon().Returns cour |> ignore
        let c = (moqCloud dl ish spd magic).Create()
        (c.Type) |> should equal ct
        (if isStork then magic.Received().CallStork() else magic.Received().CallDaemon()) |> ignore
        cour.Received().GiveBaby(c)
    let fscTest (dl: DaylightType) (ish: bool) (s1: int) (s2:int) (ct: CreatureType) (isStork: bool) = 
        let test (wnd: int) = 
            simpleTest dl ish wnd ct isStork
        Gen.sample 0 10 (Gen.choose(s1, s2)) |> List.iter test
    [<Test>] 
    let ``simple test 1`` () =  simpleTest (DaylightType.Morning) true 3 (CreatureType.Puppy) (true)
    [<Test>] 
    let ``simple test 2`` () =  simpleTest (DaylightType.Noon) true 2 (CreatureType.Kitten) (false)
    [<Test>] 
    let ``simple test 3`` () =  simpleTest (DaylightType.Evening) true 4 (CreatureType.Hedgehog) (true)
    [<Test>] 
    let ``simple test 4`` () =  simpleTest (DaylightType.Night) true 1 (CreatureType.Bearcub) (false)
    [<Test>] 
    let ``simple test 5`` () =  simpleTest (DaylightType.Morning) true 6 (CreatureType.Piglet) (true)
    [<Test>] 
    let ``simple test 6`` () =  simpleTest (DaylightType.Noon) true 7 (CreatureType.Bat) (false)
    [<Test>] 
    let ``simple test 7`` () =  simpleTest (DaylightType.Evening) true 8 (CreatureType.Balloon) (true)
    [<Test>] 
    let ``simple test 8`` () =  simpleTest (DaylightType.Night) true 9 (CreatureType.Puppy) (false)

    [<Test>] 
    let ``simple test 9`` () =  simpleTest (DaylightType.Morning) false 0 (CreatureType.Kitten) (true)
    [<Test>] 
    let ``simple test 10`` () =  simpleTest (DaylightType.Noon) false 1 (CreatureType.Hedgehog) (false)
    [<Test>] 
    let ``simple test 11`` () =  simpleTest (DaylightType.Evening) false 2 (CreatureType.Bearcub) (true)
    [<Test>] 
    let ``simple test 12`` () =  simpleTest (DaylightType.Night) false 3 (CreatureType.Piglet) (false)
    [<Test>] 
    let ``simple test 13`` () =  simpleTest (DaylightType.Morning) false 8 (CreatureType.Bat) (true)
    [<Test>] 
    let ``simple test 14`` () =  simpleTest (DaylightType.Noon) false 9 (CreatureType.Balloon) (false)
    [<Test>] 
    let ``simple test 15`` () =  simpleTest (DaylightType.Evening) false 7 (CreatureType.Puppy) (true)
    [<Test>] 
    let ``simple test 16`` () =  simpleTest (DaylightType.Night) false 10 (CreatureType.Kitten) (false)
    
    [<Test>] 
    let ``FsCheck 1`` () =  fscTest (DaylightType.Morning) true 0 5 (CreatureType.Puppy) (true)
    [<Test>] 
    let ``FsCheck 2`` () =  fscTest (DaylightType.Noon) true  0 5  (CreatureType.Kitten) (false)
    [<Test>] 
    let ``FsCheck 3`` () =  fscTest (DaylightType.Evening) true  0 5 (CreatureType.Hedgehog) (true)
    [<Test>] 
    let ``FsCheck 4`` () =  fscTest (DaylightType.Night) true  0 5  (CreatureType.Bearcub) (false)
    [<Test>] 
    let ``FsCheck 5`` () =  fscTest (DaylightType.Morning) true 6 10 (CreatureType.Piglet) (true)
    [<Test>] 
    let ``FsCheck 6`` () =  fscTest (DaylightType.Noon) true 6 10  (CreatureType.Bat) (false)
    [<Test>] 
    let ``FsCheck 7`` () =  fscTest (DaylightType.Evening) true 6 10 (CreatureType.Balloon) (true)
    [<Test>] 
    let ``FsCheck 8`` () =  fscTest (DaylightType.Night) true 6 10 (CreatureType.Puppy) (false)

    [<Test>] 
    let ``FsCheck 9`` () =  fscTest (DaylightType.Morning) false 0 3 (CreatureType.Kitten) (true)
    [<Test>] 
    let ``FsCheck 10`` () =  fscTest (DaylightType.Noon) false 0 3 (CreatureType.Hedgehog) (false)
    [<Test>] 
    let ``FsCheck 11`` () =  fscTest (DaylightType.Evening) false 0 3 (CreatureType.Bearcub) (true)
    [<Test>] 
    let ``FsCheck 12`` () =  fscTest (DaylightType.Night) false 0 3 (CreatureType.Piglet) (false)
    [<Test>] 
    let ``FsCheck 13`` () =  fscTest (DaylightType.Morning) false 6 10 (CreatureType.Bat) (true)
    [<Test>] 
    let ``FsCheck 14`` () =  fscTest (DaylightType.Noon) false 6 10 (CreatureType.Balloon) (false)
    [<Test>] 
    let ``FsCheck 15`` () =  fscTest (DaylightType.Evening) false 6 10 (CreatureType.Puppy) (true)
    [<Test>] 
    let ``FsCheck 16`` () =  fscTest (DaylightType.Night) false 6 10 (CreatureType.Kitten) (false)
