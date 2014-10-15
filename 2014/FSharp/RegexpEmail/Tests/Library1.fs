namespace Tests
open NUnit.Framework
open FsUnit

open MainCheckModule

[<TestFixture>]
module RegexpEmailTests =  
    [<Test>]  
    let ``aATb.cc``                             () = checkEmail "a@b.cc" |> should equal true
    [<Test>]  
    let ``victor.polozovATmail.ru``             () = checkEmail "victor.polozov@mail.ru" |> should equal true
    [<Test>]  
    let ``myATdomain.info``                     () = checkEmail "my@domain.info" |> should equal true
    [<Test>]  
    let ``_.1ATmail.com``                       () = checkEmail "_.1@mail.com" |> should equal true
    [<Test>]  
    let ``paints_departmentAThermitage.museum`` () = checkEmail "paints_department@hermitage.museum" |> should equal true
    
    [<Test>]  
    let ``aATb.c``                              () = checkEmail "a@b.c" |> should equal false
    [<Test>]  
    let ``a..bATmail.ru``                       () = checkEmail "a..b@mail.ru" |> should equal false
    [<Test>]  
    let ``.aATmail.ru``                         () = checkEmail ".a@mail.ru" |> should equal false
    [<Test>]  
    let ``yoATdomain.somedomain``               () = checkEmail "yo@domain.somedomain" |> should equal false
    [<Test>]  
    let ``1ATmail.ru``                          () = checkEmail "1@mail.ru" |> should equal false


