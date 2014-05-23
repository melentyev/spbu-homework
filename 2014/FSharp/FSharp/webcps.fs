open System
open System.IO
open System.Net
open System.Collections.Generic
open System.Threading

open HtmlAgilityPack

let getUrl url f = 
    Async.StartImmediate <| async {
        use! response = WebRequest.Create(new Uri(url) ).AsyncGetResponse()
        use content = response.GetResponseStream()
        use reader = new StreamReader(content)
        let! data = Async.AwaitTask <| reader.ReadToEndAsync()
        data |> f
    }

    
let rec foldCps' f (acc: 'state)  (l: 'listelem seq) (k: 'state -> 'smth)  = 
    if Seq.isEmpty l then k acc
    else 
        let hd = Seq.head l in
        let tl = Seq.skip 1 l in
        foldCps' f acc tl <| fun foldedtl -> f foldedtl hd (fun res ->  res |> k) 
    
let imgTags urls = 
    foldCps' (fun acc x k -> 
        getUrl x <| fun s -> 
            let doc = new HtmlDocument(OptionFixNestedTags  = true, OptionAutoCloseOnEnd = true)
            doc.LoadHtml(s)
            let imgs = 
                doc.DocumentNode.SelectNodes("//img")
                |> Seq.map(fun x -> x.Attributes.["src"].Value )
                |> Seq.distinct
                |> Seq.toList
            k <| acc @ (if imgs.Length >=5 then imgs else [])
        ()
    ) [] urls (fun res -> 
        res 
        |> Seq.distinct 
        |> Seq.toList 
        |> printfn "%A" ) 
    printfn "imgTags finished";

[<EntryPoint>]
let main argv = 
    [ 
        "http://google.ru/"
        "http://google.ru/"
        "http://yandex.ru/" 
    ]
    |> imgTags
    Console.ReadKey() |> ignore
    0
