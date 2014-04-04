module Huffman

open NUnit.Framework
open FsUnit

type CodeTree = 
    | Fork of CodeTree * CodeTree * char list * int
    | Leaf of char * int

let getChars = function Leaf(c, _) -> [c] | Fork(_, _, cs, _) -> cs

let createCodeTree (chars: char list) : CodeTree = 
    let rec makeForks = function
    | [a] -> a
    | l ->
        let getCnt = function Leaf(_, cnt) -> cnt | Fork(_, _, _, cnt) -> cnt        
        let a = List.minBy getCnt l
        let l' = List.filter ((<>) a) l
        let b = List.minBy getCnt l'
        let l'' = List.filter ((<>) b) l'
        Fork(a, b, getChars a @ getChars b, getCnt a + getCnt b) :: l'' |> makeForks
    
    List.fold (fun m c -> 
        let cnt = if Map.containsKey c m then Map.find c m + 1 else 1
        Map.add c cnt m ) Map.empty chars
    |> Map.fold (fun l c cnt -> Leaf(c, cnt) :: l) []
    |> makeForks

type Bit = int

let decode (tree: CodeTree)  (bits: Bit list) : char list = 
    let rec decoder (node, ls) bit = 
        match node with 
        | Fork(Leaf(c, cnt), _, _, _) when bit = 0 -> (tree, c :: ls)
        | Fork(_, Leaf(c, cnt), _, _) when bit = 1 -> (tree, c :: ls)
        | Fork(l, _, _, _) when bit = 0 -> (l, ls)
        | Fork(_, r, _, _) when bit = 1 -> (r, ls)
        | _ -> failwith "decode failed"
    let rest, l = List.fold decoder (tree, []) bits 
    if rest <> tree then failwith "decode failed (rest)" else List.rev l

let encode (tree: CodeTree)  (text: char list) : Bit list = 
    let rec encodeChar (c:char) ls = function
        | Leaf (_, _) -> ls
        | Fork (l, r, _, _) -> 
            if List.exists ((=) c) (getChars l)
            then encodeChar c (0 :: ls) l
            else encodeChar c (1 :: ls) r
    text
    |> List.fold (fun l c -> encodeChar c [] tree @ l) [] 
    |> List.rev

[<TestFixture>]
module Test =
    [<Test>]
    let ``abac``() = 
        let src = ['a'; 'b'; 'a'; 'c']
        let t = createCodeTree src
        let enc = encode t src
        decode t enc |> should equal src
    [<Test>]
    let ``asdksjdksajdsnvwjurfeuhasddfjnvcjjhasjsdhsjhjh``() = 
        let src = 
            "asdksjdksajdsnvwjurfeuhasddfjnvcjjhasjsdhsjhjh".ToCharArray()
            |> Array.toList
        let t = createCodeTree src
        let enc = encode t src
        decode t enc |> should equal src
    [<Test>]
    let ``big test``() = 
        let src = 
            (String.replicate 1000
                "abcdehhhhhadhhhhhsdhhhhhhuuuhhhhhhhh").ToCharArray()
            |> Array.toList
        let t = createCodeTree src
        let enc = encode t src
        decode t enc |> should equal src
    [<Test>]
    let ``with exception``() =
        let src = ['a'; 'b'; 'a'; 'c']
        let t = createCodeTree src
        let enc = encode t src
        (fun () -> decode t (enc @ [0]) |> ignore) 
            |> should throw typeof<System.Exception>

    