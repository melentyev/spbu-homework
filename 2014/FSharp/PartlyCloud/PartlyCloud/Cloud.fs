module Cloud
open Abstract

(*let table = @"IsShining	0 - 5	Morning	Puppy	Stork
IsShining	0 - 5	Noon	Kitten	Daemon
IsShining	0 - 5	Evening	Hedgehog	Stork
IsShining	0 - 5	Night	Bearcub	Daemon
IsShining	6 - 10	Morning	Piglet	Stork
IsShining	6 - 10	Noon	Bat	Daemon
IsShining	6 - 10	Evening	Balloon	Stork
IsShining	6 - 10	Night	Puppy	Daemon
!IsShining	0 - 3	Morning	Kitten	Stork
!IsShining	0 - 3	Noon	Hedgehog	Daemon
!IsShining	0 - 3	Evening	Bearcub	Stork
!IsShining	0 - 3	Night	Piglet	Daemon
!IsShining	4 - 10	Morning	Bat	Stork
!IsShining	4 - 10	Noon	Balloon	Daemon
!IsShining	4 - 10	Evening	Puppy	Stork
!IsShining	4 - 10	Night	Kitten	Daemon";*)

let private rnd = new System.Random()

type Daylight () = 
    interface IDaylight with
        member x.Current = 
            match rnd.Next(4) with 
            | 0 -> DaylightType.Morning
            | 1 -> DaylightType.Noon
            | 2 -> DaylightType.Evening
            | 3 -> DaylightType.Night
            | _ -> failwith "error"

type Luminary () = 
    interface ILuminary with
        member x.IsShining = match rnd.Next(2) with 0 -> false | _ -> true

type Wind () = 
    interface IWind with
        member x.Speed = rnd.Next(11)

type Courier (t : CourierType) = 
    interface ICourier with
        member x.GiveBaby _ = ()
        //member x.Type = t

type Magic () = 
    interface IMagic with  
        member x.CallStork () = new Courier(CourierType.Stork) :> ICourier
        member x.CallDaemon () = new Courier(CourierType.Daemon) :> ICourier

type Cloud (daylight: IDaylight, luminary : ILuminary, wind: IWind, magic: IMagic) =
    member private x.InternalCreate() =
        //failwith "asdsadsa"
        match luminary.IsShining with
        | true  -> 
            match daylight.Current with
            | DaylightType.Morning -> magic.CallStork(),  if wind.Speed < 6 then CreatureType.Puppy else CreatureType.Piglet
            | DaylightType.Noon    -> magic.CallDaemon(), if wind.Speed < 6 then CreatureType.Kitten else CreatureType.Bat
            | DaylightType.Evening -> magic.CallStork(),  if wind.Speed < 6 then CreatureType.Hedgehog else CreatureType.Balloon
            | DaylightType.Night   -> magic.CallDaemon(), if wind.Speed < 6 then CreatureType.Bearcub else CreatureType.Puppy
        | false ->
            match daylight.Current with
            | DaylightType.Morning -> magic.CallStork(),  if wind.Speed < 4 then CreatureType.Kitten else CreatureType.Bat
            | DaylightType.Noon    -> magic.CallDaemon(), if wind.Speed < 4 then CreatureType.Hedgehog else CreatureType.Balloon
            | DaylightType.Evening -> magic.CallStork(),  if wind.Speed < 4 then CreatureType.Bearcub else CreatureType.Puppy   
            | DaylightType.Night   -> magic.CallDaemon(), if wind.Speed < 4 then CreatureType.Piglet else CreatureType.Kitten
 
    member x.Create() =
        let courier, ct = x.InternalCreate()
        let creature = new Creature(ct)
        courier.GiveBaby(creature)
        creature


