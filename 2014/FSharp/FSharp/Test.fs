let o = new obj()


let s = Seq.unfold (fun(a, b) -> Some(b, (b, a + b))) (0, 1)
let v = Seq.cache s

printfn "%A" (Seq.take 10 v |> Seq.toList)

System.Console.ReadKey() |> ignore