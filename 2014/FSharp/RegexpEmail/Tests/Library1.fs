namespace Tests
open NUnit.Framework
open FsUnit

open MainCheckModule

[<TestFixture>]
module RegexpEmailTests =  
    [<Test>]  
    let ``aATb.cc``                             () = checkEmail "a@b.cc" |> should be True
    [<Test>]  
    let ``victor.polozovATmail.ru``             () = checkEmail "victor.polozov@mail.ru" |> should be True
    [<Test>]  
    let ``myATdomain.info``                     () = checkEmail "my@domain.info" |> should be True
    [<Test>]  
    let ``_.1ATmail.com``                       () = checkEmail "_.1@mail.com" |> should be True
    [<Test>]  
    let ``paints_tmentAThermitage.museum`` () = checkEmail "paints_tment@hermitage.museum" |> should be True
    
    [<Test>]  
    let ``aATb.c``                              () = checkEmail "a@b.c" |> should be False
    [<Test>]  
    let ``a..bATmail.ru``                       () = checkEmail "a..b@mail.ru" |> should be False
    [<Test>]  
    let ``.aATmail.ru``                         () = checkEmail ".a@mail.ru" |> should be False
    [<Test>]  
    let ``yoATdomain.somedomain``               () = checkEmail "yo@domain.somedomain" |> should be False
    [<Test>]  
    let ``1ATmail.ru``                          () = checkEmail "1@mail.ru" |> should be False


