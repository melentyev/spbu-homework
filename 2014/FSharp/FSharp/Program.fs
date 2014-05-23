module HW1 =
    let rec sum = function 
        | [] -> 0
        | h :: t -> h + sum t
    let rec append a b = 
        match a with 
        | [] -> b
        | h :: t -> h :: append t b
    let rec addToEnd x = function 
        | [] -> [x]
        | h :: tl -> h :: addToEnd x tl

    let rec filter f = function 
        | [] -> []
        | h :: tl -> (if f h then [h] else []) @ filter f tl
 
    let genSqr n = 
        Seq.initInfinite <| fun x -> (x + 1) * (x + 1)
        |> Seq.takeWhile ( (>=) n)
        |> Seq.toList
    let tests() =
        printfn "HW1.tests:" 
        sum [ 1 .. 10] 
        |> printfn "sum [ 1 .. 10]: %A"
        append [1 .. 5] [6..10] 
        |> printfn "append [1 .. 5] [6..10]: %A"
        addToEnd 6 [1 .. 5]
        |> printfn "addToEnd 6 [1 .. 5]: %A"
        filter ( (<=) 0 ) [ -5 .. 5 ]
        |> printfn "filter ( (<) 0 ) [ -5 .. 5 ]: %A" 
        genSqr 24
        |> printfn "genSqr 24: %A"
        genSqr 25
        |> printfn "genSqr 25: %A"
        printfn ""

module HW2 = 
    open System
    let biggestDelimiter n = 
        let isPrime n = 
            seq { 2L .. (int64 << sqrt << float ) n + 1L }
            |> Seq.fold (fun flag x -> flag && n % x <> 0L) true
        let delims = 
            seq { 1L .. (int64 << sqrt << float ) n + 1L }
            |> Seq.filter (fun x -> n % x = 0L)
            |> Seq.toList
        delims @ List.map (fun d -> n / d) delims
        |> List.filter isPrime
        |> List.sort
        |> List.rev
        |> List.head
    
    let sumfibs n = 
        Seq.unfold (fun (x, y) -> Some(x + y, (y, x + y) ) ) (0, 1)
        |> Seq.takeWhile ((>=)n)
        |> Seq.filter (fun x -> x % 2 = 0)
        |> Seq.sum
    
    let factorialDigitsSum n = 
        let chars (s:string) = s.ToCharArray() 
        let sq = seq { 1I .. n } 
        sq
        |> Seq.fold (*) 1I
        |> string
        |> chars
        |> Array.map (int << string)
        |> Array.sum 
    
    type Expr = 
        | Const of int
        | Var of string
        | Add of Expr * Expr
        | Sub of Expr * Expr
        | Mul of Expr * Expr
        | Div of Expr * Expr
    
   (* let rec arithm expression =
        let cmp e1 e2 = 
            match e2 with 
            | e1 -> true 
            | _ -> false
        let binaryOp x y op expr = 
            let lhs, rhs = arithm x, arithm y
            match lhs, rhs with
            | Const a, Const b -> Const(op a b)
            | Var x, Const 0 | Const 0, Var x when cmp expr Add -> Var x 
            | Var x, Const 0 when cmp expr Sub -> Var x
            | Var x, Const 1 | Const 1, Var x when cmp expr Mul -> Var x
            | Var x, Const 1 when cmp expr Div -> Var x
            | _ -> expr(lhs, rhs)
        let reduce = function
        | Add (Const a, Const b) -> Const (a + b)
        | Sub (Const a, Const b) -> Const (a - b)
        | Sub (Const a, Const b) -> Const (a * b)
        | Sub (Const a, Const b) -> Const (a / b)
        match expression with
        | Add (x,y) -> reduce <| Add (arithm x, arithm y)
        //| Sub (x,y) -> 
        | Add(x, y) -> binaryOp x y (+) Add 
        | Sub(x, y) -> binaryOp x y (-) Sub
        | Mul(x, y) -> binaryOp x y (*) Mul 
        | Div(x, y) -> binaryOp x y (/) Div
        | x -> x

        *)
    type Geom = 
        | NoPoint
        | Point of float * float
        | Line  of float * float   // уравнение прямой y = a*x+b
        | VerticalLine of float    // вертикальная прямая проходящая через x
        | LineSegment of (float * float) * (float * float) // отрезок
        | Intersect of Geom * Geom // пересечение двух множеств
    let rec resolveIntersections g = 
        let line x y x' y' = (y - y', x' - x, x * y' - x' * y)
        let onLine (A, B, C) x y = A * x + B * y + C = 0.0
        let boundedBy (x1, y1, x2, y2) x y =
            let x', y' = min x1 x2, min y1 y2
            let x'', y'' = max x1 x2, max y1 y2 
            x >= x' && x <= x'' && y >= y' && y <= y''
        let geom2line = function 
            | Line (a, b) -> (a, -1.0, b)
            | VerticalLine (x) -> (1.0, 0.0, -x)
        let linesIntersection a' b' = 
            let (A, B, C) = geom2line a'
            let (A', B', C') = geom2line b'
            NoPoint
        let intersect' a' b'  =
            let a = resolveIntersections a'
            let b = resolveIntersections b'
            match a, b with
            | NoPoint, _ | _, NoPoint -> NoPoint
            | Point(x, y) as p, Point(x', y') -> if x = x' && y = y' then p else NoPoint
            | (Point(x, y), v) | (v, Point(x, y) ) -> 
                match v with 
                | VerticalLine x' -> if x = x' then Point(x, y) else NoPoint
                | Line (a, b) -> if y = a * x + b then Point(x, y) else NoPoint
                | LineSegment ( (x', y'), (x'', y'' ) )  -> 
                    if onLine (line x' y' x'' y'' ) x y 
                        && boundedBy (x', y', x'', y'') x y then Point(x, y) else NoPoint
                | _ -> failwith "_"
            | l1, l2 -> linesIntersection l1 l2
        match g with
        | Intersect (a, b) -> intersect' a b
        | x -> x

    let rec ways n m = 
        if n = 0L || m = 0L then 1L else 
        ways (n - 1L) m + ways n (m - 1L) 

    let tests() = 
        printfn "HW2.tests:" 
        biggestDelimiter 600851475143L
        |> printfn "biggestDelimiter 600851475143L: %A"
        sumfibs 4000000
        |> printfn "sumfibs 4000000: %A" 
        factorialDigitsSum 100I
        |> printfn "factorialDigitsSum 100: %A" 
        //arithm <| Add (Add(Const 1, Const 2), Add (Var "a", Const 0))
        //|> printfn "arithm <| Add (Add(Const 1, Const 2), Add (Var \"a\", Const 0)): %A" 
        //ways 20 20
        printfn ""

module HW3 = 
    let rec map' f l k = 
        match l with
        | [] -> k []
        | hd :: tl -> map' f tl (fun mtl -> f hd (fun x -> x :: mtl |> k) ) 
        
    let tests() = 
        let sqr' x k = k (x * x)
        map' (sqr' ) [1; 2; 3; 4; 5; 6] (printfn "map' ( (+) 1) [1; 2; 3; 4; 5; 6]: %A")    
        printfn ""
module HW4 = 
    open HtmlAgilityPack
    open System
    open System.IO
    open System.Net
    open System.Windows.Forms
    open System.Collections.Generic
    open System.Threading

    let getUrl url f = 
        Async.StartImmediate <| async {
            use! response = WebRequest.Create(new Uri(url) ).AsyncGetResponse()
            use content = response.GetResponseStream()
            use reader = new StreamReader(content)
            reader.ReadToEnd() |> f
        }

    
    let rec foldCps' f (acc: 'state)  (l: 'listelem seq) (k: 'state -> 'smth)  = 
        if Seq.isEmpty l then k acc
        else 
            let hd = Seq.head l in
            let tl = Seq.skip 1 l in
            foldCps' f acc tl <| fun foldedtl -> f foldedtl hd (fun res ->  res |> k) 
    
    let imgTags urls = 
        //let ht = new HashSet<string>()
        foldCps' (fun acc x k -> 
            getUrl x <| fun s -> 
                let doc = new HtmlAgilityPack.HtmlDocument(OptionFixNestedTags  = true, OptionAutoCloseOnEnd = true)
                doc.LoadHtml(s)
                let imgs = 
                    doc.DocumentNode.SelectNodes("//img")
                    |> Seq.map(fun x -> x.Attributes.["src"].Value )
                    |> Seq.distinct
                    |> Seq.toList
                k <| if imgs.Length >=5 then acc @ imgs else acc
            ()
        ) [] urls (fun res -> res |> Seq.distinct |> Seq.toList |> printfn "%A" ) 
        printfn "imgTags finished";

    
    let chatting() = 
        let jqUrl = "http://code.jquery.com/jquery-1.6.4.min.js"
        //let urlStart = "http://www.personalityforge.com/directchat.php?BotID=77095&MID=77094"   
        let urlStart = "http://www.personalityforge.com/directchat.php?BotID=102231&MID=102225"
        //let jq = getUrlSync jqUrl
        let wb = new WebBrowser(Visible = false, Height =  410, Width = 700)
        let tb = new TextBox(Top = 420)
        let btnSend = new Button(Top = 420, Left = 200)
        let form = new Form(Visible = false, Height =  560, Width = 740)
        let firstT ime = ref true
        let injectBase src js = 
            new Action(fun () ->
                let scriptEl = wb.Document.CreateElement("script")
                scriptEl.SetAttribute("src", src)
                let element = scriptEl.DomElement :?> mshtml.IHTMLScriptElement
                element.text <- js
                wb.Document.GetElementsByTagName("head").[0].AppendChild(scriptEl) |> ignore)
            |> wb.Invoke |> ignore
                

        let injectJs s = 
            injectBase "" s

        let injectJq f = 
            //injectBase "http://code.jquery.com/jquery-latest.min.js" ""
            //wb.DocumentCompleted 
            //|> Event.filter (fun e -> e.Url.AbsoluteUri = jqUrl)
            //|> Event.add f
            injectBase jqUrl ""
            f()
        let invoke s = 
            new Func<obj>(fun () ->
                wb.Document.InvokeScript(s) )
            |> wb.Invoke
        btnSend.Click.Add <| fun _ -> 
            let fname = "injFunc" + (string << abs <| System.DateTime.Now.ToString().GetHashCode() )
            let txt = tb.Text
            let js = "function " + fname + """ () {  
                var form = $(".rounded > form"); 
                            
                form.find('input[name="Message"]').val('""" + txt + """');
                form.find('input[type="submit"]').click();
                //return txt;  
            } """
            injectJs js
            let k = invoke fname
            ()
        form.Controls.AddRange [| wb; btnSend; tb |]
        (*form.Activated.Add <| fun _ -> 
            if not !firstTime then () else 
                firstTime := false      
                
                //wb.DocumentCompleted |> Event.add (fun e -> printfn "%s" e.Url.AbsoluteUri)
                wb.DocumentCompleted 
                |> Event.filter (fun e -> e.Url.AbsoluteUri = urlStart)
                |> Event.add (fun _ -> 
                    injectJq (fun _ -> 
                        injectJs """ function getTxt() {  
                            var form = $(".rounded > form"); 
                            var txt = form.find(".bigarea .rounded span.bigfont").html();
                            //$('input[name="Message"]').click();
                            //$('input[type="submit"]').click();
                            return txt;  
                        } """
                        let k = invoke "getTxt"
                        printfn "%A" (string k)
                        //MessageBox.Show(string k)|> ignore
                    ))
                printfn "here"
                wb.Navigate(urlStart) 
        Application.Run(form)
        ()*)

    let tests() = 
        [ "http://google.ru/"
          "http://google.ru/"
          "http://yandex.ru/" 
          ]
          |> imgTags
        printfn ""

[<EntryPoint>]
[<System.STAThread>]
let main argv = 
    //HW1.tests()
    //HW2.tests()
    //HW4.tests()
    HW4.chatting()
    printfn "Any key to continue"
    //System.Console.ReadKey() |> ignore
    0 // return an integer exit code

(*
#if INTERACTIVE
#r "System.Windows.Forms"
#r "System.Drawing"
#endif


open System.Windows.Forms
open System.Drawing
let l1 =  new Label()
let l2 =  new Label()
let w1 = new Form(Text = "lll1", Visible = true)
w1.Controls.AddRange [| l1 :> Control|]
w1.MouseMove.Add <| fun e -> l1.Text <- string e.X

let w2 = new Form(Text = "lll3", Visible = true)
w2.Controls.AddRange [| l2 :> Control|]
w2.MouseMove.Add <| fun e -> l2.Text <- string e.X

w1.AcceptButton = *)