namespace TreeMap

module TreeMap = 

    open System
    open System.Collections.Generic
    open System.Collections

    type TreeNode<'K, 'V> = 
        | Fork of TreeNode<'K, 'V> * TreeNode<'K, 'V> * 'K * 'V * int
        | Empty

    let private isEmpty = function Empty -> true | _ -> false

    let private left =  function 
        | Empty -> failwith "empty" 
        | Fork(x, _, _, _, _) -> x

    let private right =  function 
        | Empty -> failwith "empty" 
        | Fork(_, x, _, _, _) -> x

    let private key =  function 
        | Empty -> failwith "empty" 
        | Fork(_, _, x, _, _) -> x

    let private value = function 
        | Empty -> failwith "empty" 
        | Fork(_, _, _, x, _) -> x

    let private height =  function 
        | Empty -> 0
        | Fork(_, _, _, _, x) -> x

    let private upd l r = max (height l) (height r) + 1

    let private fork l r k v = Fork(l, r, k, v, upd l r)

    let private left_rotate = function
        | Fork(l, Fork(l2, r2, k2, v2, h2), k, v, h) as t ->
            fork (fork l l2 k v) r2 k2 v2
        | _ -> failwith "unexpected"
         
    let private right_rotate = function
        | Fork(Fork(l2, r2, k2, v2, h2), r, k, v, h) as t ->
            fork l2 (fork r2 r k v) k2 v2
        | _ -> failwith "unexpected" 

    let private balance = function
        | Fork(l, r, k, v, h) as t -> 
            if height r - height l > 1 then 
                if not (height (left r) <= height (right r) )  
                then fork l (right_rotate r) k v
                else t
                |> left_rotate
            elif height l - height r > 1 then
                if not (height (right l) <= height(left l) ) 
                then fork (left_rotate l) r k v
                else t
                |> right_rotate
            else t
        | Empty -> Empty
        
    let rec private add' k v = function
        | Empty -> Fork(Empty, Empty, k, v, 1) 
        | Fork(l, r, k1, v1, _) -> 
            if k < k1 then 
                fork (add' k v l) r k1 v1
            else 
                fork l (add' k v r) k1 v1
            |> balance
    let rec private count' = function 
        | Empty -> 0
        | Fork(l, r, _, _, _) -> count' l + 1 + count' r
        
    let rec private mostLeft = function
        | Fork(x, _, _, _, _) as t -> if isEmpty x then t else mostLeft x
        | Empty -> failwith "struct error"
    
    let rec private mostRight = function
        | Fork(_, x, _, _, _) as t -> if isEmpty x then t else mostRight x
        | Empty -> failwith "struct error"

    let rec private find' k t = 
            match t with
            | Empty -> Empty
            | Fork(l, r, k1, _, _) as f when k = k1 -> f
            | Fork(l, r, k1, _, _) when k < k1 -> find' k l
            | Fork(l, r, k1, _, _) -> find' k r

    let rec private remove' k = function
            | Empty -> Empty
            | Fork(l, r, k1, v1, _) when k <> k1 -> 
                if k < k1 
                then fork (remove' k l) r k1 v1
                else fork l (remove' k r) k1 v1
                |> balance
            | Fork(l, r, k, v, _) as t-> 
                if isEmpty l && isEmpty r then Empty
                else 
                    if height l > height r then
                        let v' = mostRight l
                        fork (remove' (key v') l) r (key v') (value v')
                    else 
                        let v' = mostLeft r
                        fork l (remove' (key v') r) (key v') (value v')
                    |> balance

    type TreeMap<'K, 'V  when 'K : comparison > (t: TreeNode<'K, 'V>) = 
        let getEnumerator() = 
            let rec traverse = function 
                | Empty -> [] 
                | Fork(l, r, _, _, _) as f -> traverse l @ [f] @ traverse r
            let fullpath = Empty :: traverse t
            let path = ref fullpath
            let reset() = path := fullpath
            let current() = 
                match List.head !path with 
                        | Fork(_, _, k, v, _) -> (k,v)
                        | _ -> failwith "e.Current"
            { new IEnumerator<_> with
                member e.Current = current()
            interface IEnumerator with
                member e.Current = current() |> box
                member e.MoveNext() = 
                    path := List.tail !path
                    not <| List.isEmpty !path
                    
                member e.Reset() = path := fullpath 
            interface System.IDisposable with 
                member e.Dispose() = () 
                }

        new (?data) = 
            match data with 
            | None -> new TreeMap<'K,'V> (TreeNode<'K,'V>.Empty)
            | Some(sq) ->
                if Seq.isEmpty sq then TreeMap<'K,'V> (TreeNode<'K,'V>.Empty)
                else new TreeMap<'K,'V> (Seq.distinctBy fst sq 
                                        |> Seq.fold (fun m (k, v) -> add' k v m) Empty )

        member x.Add(k, v) = 
            let v = 
                match (find' k t) with 
                | Empty -> t
                | _ -> (remove' k t)
                |> add' k v
            new TreeMap<_,_>(v)

        member x.Remove k = new TreeMap<_,_>(remove' k t)
        member x.TryFind k = match find' k t with Empty -> None | t -> Some(value t)
        member x.ContainsKey k = 
            match x.TryFind k with
            | Some(_) -> true
            | _ -> false
        member x.Count = count' t
        member x.IsEmpty = match t with Empty -> true | _ -> false
        member x.Item k = 
            match x.TryFind(k) with
            | Some(v) -> v
            | _ -> raise(new KeyNotFoundException())
        override x.ToString() = "TreeMap: " + Seq.fold (fun acc y -> acc + y.ToString()) "" x
        interface IEnumerable<'K*'V> with
            member x.GetEnumerator() = getEnumerator()
        interface IEnumerable with
             member x.GetEnumerator() = getEnumerator() :> IEnumerator
    

    let rnd = new System.Random()
    let a = new TreeMap<_, _> (Seq.init 100 (fun _ -> (rnd.Next(1, 200), "aba") ))
    printfn "%A" <| Seq.toList a
    printfn "%A" <| Seq.toList (new TreeMap<_, _> ( [ (1, 2); (1, 3) ] ))

    System.Console.ReadKey() |> ignore