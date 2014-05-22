open System
open System.Windows.Forms
open System.Drawing
open AIMLbot

[<EntryPoint>]
let main argv = 
    let bot = new Bot()
    let user = new AIMLbot.User("User", bot)
    let request s = (bot.Chat (s, user.UserID) ).Output

    bot.loadSettings()
    bot.loadAIMLFromFiles ()

    let preparedForm = 
        
        let form = new Form(Text = "Chat", FormBorderStyle = FormBorderStyle.FixedDialog, 
                            Padding = new Padding(10), Width = 700, Height = 500 )

        let history = new RichTextBox (Width = form.ClientSize.Width, ReadOnly = true, 
                                        Height = form.ClientSize.Height - 30, BackColor = Color.White)

        let sendBtn = new Button(Text = "Send", Left = form.ClientSize.Width - 75, 
                                 Top = form.ClientSize.Height - 30)

        let input = new TextBox(Width = form.ClientSize.Width - 78, Top = form.ClientSize.Height - 30)

        let esc, enter =  
            input.KeyUp
            |> Event.filter (fun e -> e.KeyCode = Keys.Escape || e.KeyCode = Keys.Enter)
            |> Event.partition (fun e -> e.KeyCode = Keys.Escape)
        
        enter
        |> Event.map(fun e -> e :> EventArgs)
        |> Event.merge (sendBtn.Click)
        |> Event.add (fun _ -> 
            input.Text <- ""
            history.AppendText ("[You]: " + input.Text + "\n")
            history.SelectionStart <- history.TextLength
            history.SelectionColor <- Color.Red
            history.AppendText ("[Bot]: " + request input.Text + "\n")
            history.SelectionColor <- history.ForeColor
        )

        (*
        FP approach
        |> Event.scan (fun text _ -> request input.Text + "\n" :: input.Text + "\n" :: text) []
        |> Event.add (fun t -> 
            input.Text <- ""
            history.Text <- ""
            t 
            |> List.rev 
            |> List.mapi (fun i s -> (i % 2 = 1, s) )
            |> List.iter (fun (isBotPhrase, s) ->
                if isBotPhrase then 
                    history.SelectionStart <- history.TextLength
                    history.SelectionColor <- Color.Red
                history.AppendText ( (if isBotPhrase then "[Bot]: " else  "[You]: ") + s)
                if isBotPhrase then history.SelectionColor <- history.ForeColor
            ) )
        *)
        
        esc 
        |> Event.add(fun _ -> form.Close())

        form.Activated |> Event.add (fun _ -> input.Focus() |> ignore)

        form.Controls.AddRange [| history; sendBtn; input |]
        
        form
    
    Application.Run preparedForm
    0
