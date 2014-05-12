
#load "../packages/FSharp.Charting.0.90.6/FSharp.Charting.fsx"

open System
open System.Threading
open System.IO
open System.Diagnostics
open FSharp.Charting

let pc = new PerformanceCounter("Процессор", "% загруженности процессора", "_Total")

let data = seq { for i in 0 .. 99 -> (i, 0) } |> ref


let out = new Event<_>()
let timer = new System.Windows.Forms.Timer(Interval=200, Enabled=true)
timer.Tick.Add (fun args -> 
    data := Seq.concat [ Seq.skip 1 (!data); Seq.singleton (100, pc.NextValue() |> int) ] |> Seq.map (fun (x, y) -> (x - 1, y) );
    out.Trigger (!data) )
timer.Start()
LiveChart.Line (out.Publish)
(*
let out2 = new Event<_>()
let timer2 = new System.Windows.Forms.Timer(Interval=200, Enabled=true)
timer.Tick.Add (fun _ -> ()

LiveChart.LineIncremental()
*)
