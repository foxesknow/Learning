namespace DataStructures

module BinaryTree =
    type T<'a> =
        | Empty
        | Node of item:'a * left:T<'a> * right:T<'a>

    let empty =
        Empty

    let isEmpty tree =
        match tree with
        | Empty -> true
        | _ -> false

    let head tree =
        match tree with
        | Empty -> failwith "tree is empty"
        | Node(i, _, _) -> i

    let tryHead tree =
        match tree with
        | Empty -> None
        | Node(i, _, _) -> Some(i)

    let left tree =
        match tree with
        | Empty -> failwith "tree is empty"
        | Node(_, left, _) -> left

    let tryLeft tree =
        match tree with
        | Empty -> None
        | Node(_, left, _) -> Some(left)

    let right tree =
        match tree with
        | Empty -> failwith "tree is empty"
        | Node(_, _, right) -> right

    let tryRight tree =
        match tree with
        | Empty -> None
        | Node(_, _, right) -> Some(right)

    let rec length tree =
        match tree with
        | Empty -> 0
        | Node(_, left, right) ->
            1 + (length left) + (length right)

    let rec add item tree =
        match tree with
        | Empty -> Node(item, Empty, Empty)
        | Node(i, left, right) as node ->
            if i = item then 
                node
            else if item < i then 
                Node(i, add item left, right)
            else 
                Node(i, left, add item right)


    let addCP item tree =        
        // My head hurts...!
        let rec cont item tree k =
            match tree with
            | Empty -> k(Node(item, Empty, Empty))
            | Node(i, left, right) as node ->
                if i = item then 
                    k(node)
                else if item < i then 
                    cont item left (fun n -> k(Node(i, n, right)))
                else 
                    cont item right (fun n -> k(Node(i, left, n)))

        cont item tree id

    let rec contains item tree =
        match tree with
        | Empty -> false
        | Node(i, left, right) as node ->
            if i = item then 
                true
            else if item < i then 
                contains item left
            else 
                contains item right

    let rec asInfixSequence tree =
        seq{
            match tree with
            | Empty -> yield! Seq.empty
            | Node(i, left, right) ->
                yield! asInfixSequence left
                yield i
                yield! asInfixSequence right
        }

    let rec asPrefixSequence tree =
        seq{
            match tree with
            | Empty -> yield! Seq.empty
            | Node(i, left, right) ->
                yield i
                yield! asPrefixSequence left                
                yield! asPrefixSequence right
        }

    let rec asPostfixSequence tree =
        seq{
            match tree with
            | Empty -> yield! Seq.empty
            | Node(i, left, right) ->
                yield! asPostfixSequence left                
                yield! asPostfixSequence right
                yield i
        }

    let breadthFirst tree =        
        let rec loop q =
            seq{
                match Queue.tryFrontAndDequeue q with
                | None -> yield! Seq.empty
                | Some(node, q') ->
                    match node with
                    | Empty -> yield! Seq.empty
                    | Node(i, left, right) ->
                        yield i
                        yield! (q' |> Queue.enqueue left |> Queue.enqueue right |> loop)
            }

        let queue = Queue.empty |> Queue.enqueue tree
        loop queue
                
            



