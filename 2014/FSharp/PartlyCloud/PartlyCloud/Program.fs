open System
open Abstract
open Cloud

let x = 1
let a = Some(1, (x = 1))

[<EntryPoint>]
let main argv = 
    printfn "%A" argv
    0
