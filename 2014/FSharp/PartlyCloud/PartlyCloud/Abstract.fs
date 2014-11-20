module Abstract

type DaylightType =
    | Morning
    | Noon
    | Evening
    | Night

type CreatureType = 
    | Puppy
    | Kitten
    | Hedgehog
    | Bearcub
    | Piglet
    | Bat
    | Balloon
type CourierType = Stork | Daemon

type Creature (ct: CreatureType) = 
    member x.Type = ct

type IDaylight = 
    abstract member Current : DaylightType

type ILuminary = 
    abstract member IsShining : bool

type IWind = 
    abstract member Speed : int

type ICourier = 
    abstract member GiveBaby : Creature -> unit
    //abstract member Type : CourierType

type IMagic = 
    abstract member CallStork : unit -> ICourier
    abstract member CallDaemon : unit -> ICourier