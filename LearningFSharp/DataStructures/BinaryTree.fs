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



