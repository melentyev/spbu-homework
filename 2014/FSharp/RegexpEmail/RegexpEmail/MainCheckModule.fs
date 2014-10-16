module MainCheckModule

//namespace 

open System.Text.RegularExpressions
open ARSoft.Tools.Net.Dns

let checkEmail s = 
    let preffix = @"[a-zA-Z_][\w\-]*(\.[\-\w]+)*";
    let suffix = @"([\w\-]+[\.])+((\w{2,3})|info|museum|travel|xxx|name|mobi|jobs|coop|asia|aero)";
    let pattern = "^" + preffix + "@" + "(?<Domain>(" + suffix + "))$" 
    Regex.Match(s, pattern).Success
//    let res = Regex.Match(s, pattern)
//    let dom = res.Groups.["Domain"];
//    let ans = DnsClient.Default.Resolve("gmail.com", RecordType.Mx).AnswerRecords
//    ans.Count > 0 && res.Success

[<EntryPoint>]
let main argv = 
    let b = checkEmail "yo@domain.somedomain"
    printfn "Hello, World";
    0 
