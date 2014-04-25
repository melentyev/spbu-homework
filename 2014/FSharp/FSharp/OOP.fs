module OOP

type Device(approx_quality) =
    let mutable state = approx_quality
    let rnd = new System.Random()
    member x.Use() = 
        state <- state - rnd.Next(15, 20)
        if state <= 0 then failwith "Device broken"

type InputDevice<'T> (approx_quality) as dev =
    inherit Device(approx_quality)
    let ev = new Event<'T * System.DateTime>() 
    abstract member InputReceived : IEvent<'T * System.DateTime>
    default x.InputReceived = 
        ev.Publish 
        |> Event.map(fun x -> dev.Use(); x)
    member x.SimulateInput a = ev.Trigger(a, System.DateTime.Now)

type OutputDevice(approx_quality) = 
    inherit Device(approx_quality)
    let lockobj = new System.Object()
    let mutable busy = false
    member x.IsBusy = busy
    member x.Capture() = lock lockobj <| fun () -> 
        x.Use()
        if busy then false 
        else busy <- true; true
    member x.Release() = lock lockobj <| fun () -> 
        if not busy then false 
        else busy <- false; true

type Printer(approx_quality) = 
    inherit OutputDevice(approx_quality)
    let rnd = new System.Random()
    member x.Print(data) = 
        async {
            try
                x.Capture() |> ignore
                do! Async.Sleep <| rnd.Next(2000, 3000)
                x.Use() 
            finally
                x.Release() |> ignore
        }

type KeyboardEventType = KB_UP | KB_DOWN | KB_PRESS
type MouseEventType = 
    | MB_RIGHT_UP  |  MB_RIGHT_DOWN
    | MB_MIDDLE_UP |  MB_MIDDLE_DOWN 
    | MB_LEFT_UP   |  MB_LEFT_DOWN
    | MOUSE_MOVE

type Keyboard(approx_quality, mapping: Map<int, char> ) =  
    inherit InputDevice<KeyboardEventType * int>(approx_quality)
    member x.SimulateKeyPress key = 
        x.SimulateInput(KB_DOWN, key)
        x.SimulateInput(KB_UP, key)
    member x.KbInputReceived = 
        base.InputReceived 
        |> Event.map (fun ((t, c), _) -> 
            if Map.containsKey c mapping then 
                Choice1Of2 (mapping.[c])
            else 
                Choice2Of2 c)

type Mouse(approx_quality) =
    inherit InputDevice<MouseEventType * (int * int)>(approx_quality)
    member this.DoubleClickReceived = 
        base.InputReceived
        |> Event.pairwise
        |> Event.choose (fun ( ( ( t1, _), tm1 ),  ( (t2, pos), tm2 ) ) -> 
            if t1 = MB_LEFT_UP && t2 = MB_LEFT_DOWN 
                && tm2 - tm1 < System.TimeSpan.FromMilliseconds(50.0) 
            then Some(pos) 
            else None)
    member this.SimulateClick(x, y) = 
        this.SimulateInput(MB_LEFT_DOWN, (x, y) )
        this.SimulateInput(MB_LEFT_UP, (x, y) )


let tests() =
    let kmap = 
        ['a' .. 'z'] @ ['A' .. 'Z']
        |> List.fold(fun m c -> Map.add (int c) c m) Map.empty
    let m = new Mouse(200)
    let kb = new Keyboard(200, kmap)
    let p = new Printer(210)
    m.InputReceived.Add(printfn "Mouse input received: %A")
    m.DoubleClickReceived.Add(printfn "DoubleClickReceived received: %A")
    kb.KbInputReceived.Add(printfn "KbInputReceived: %A")
    try
        kb.SimulateKeyPress(96)
        kb.SimulateKeyPress(97)
        kb.SimulateKeyPress(98)
        m.SimulateClick(100, 200)
        System.Threading.Thread.Sleep(200);
        m.SimulateClick(100, 201)
        m.SimulateClick(100, 202)
        
        Async.StartImmediate (async { 
            let! res = Async.Catch(p.Print("hello, world"))
            match res with
            | Choice1Of2() -> printfn "printing done"
            | Choice2Of2(e) -> printfn "printing exception (%s)" e.Message
        })
    with 
        e -> printfn "Exception: %s" e.Message
    ()

tests()
printfn "Finished"
System.Console.ReadKey() |> ignore