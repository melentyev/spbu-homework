module MainCheckModule

//namespace 

open System.Text.RegularExpressions

let checkEmail s = 
    let pattern = @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$" in Regex.Match(s, pattern).Success

[<EntryPoint>]
let main argv = 
    printfn "Hello, World";
    0 
