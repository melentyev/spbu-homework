// Learn more about F# at http://fsharp.net. See the 'F# Tutorial' project
// for more guidance on F# programming.

#load "Library1.fs"
open Tests

// Define your library scripting code here

//
//
//module TestHelpers = 
//    let trueRealization (daylight: IDaylight, luminary : ILuminary, wind: IWind, magic: IMagic) = 
//        match luminary.IsShining with
//        | true  -> 
//            match daylight.Current with
//            | DaylightType.Morning -> magic.CallStork(),  if wind.Speed < 6 then CreatureType.Puppy else CreatureType.Piglet
//            | DaylightType.Noon    -> magic.CallDaemon(), if wind.Speed < 6 then CreatureType.Kitten else CreatureType.Bat
//            | DaylightType.Evening -> magic.CallStork(),  if wind.Speed < 6 then CreatureType.Hedgehog else CreatureType.Balloon
//            | DaylightType.Night   -> magic.CallDaemon(), if wind.Speed < 6 then CreatureType.Bearcub else CreatureType.Puppy
//        | false ->
//            match daylight.Current with
//            | DaylightType.Morning -> magic.CallStork(),  if wind.Speed < 4 then CreatureType.Kitten else CreatureType.Bat
//            | DaylightType.Noon    -> magic.CallDaemon(), if wind.Speed < 4 then CreatureType.Hedgehog else CreatureType.Balloon
//            | DaylightType.Evening -> magic.CallStork(),  if wind.Speed < 4 then CreatureType.Bearcub else CreatureType.Puppy   
//              | DaylightType.Night   -> magic.CallDaemon(), if wind.Speed < 4 then CreatureType.Piglet else CreatureType.Kitten


//    type IntGenerator0_5 = static member Wnd() = Arb.fromGen <| Gen.choose(0, 5)
//    type IntGenerator6_10 = static member Wnd() = Arb.fromGen <| Gen.choose(6, 10)
//    type IntGenerator03 = static member Wnd() = Arb.fromGen <| Gen.choose(0, 3)
//    type IntGenerator4_10 = static member Wnd() = Arb.fromGen <| Gen.choose(4, 10)
    
//        Arb.register<IntGenerator6_10>() |> ignore