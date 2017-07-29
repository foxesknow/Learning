namespace DataStructures

module RedBlackTree =
    type Color = Red | Black

    type T<'a> =
        | Empty
        | Node of color:Color * item:'a * left:T<'a> * right:T<'a>

    let empty =
        Empty

    let isEmpty tree =
        match tree with
        | Empty -> true
        | _ -> false

    let head tree =
        match tree with
        | Empty -> failwith "tree is empty"
        | Node(_, i, _, _) -> i

    let tryHead tree =
        match tree with
        | Empty -> None
        | Node(_, i, _, _) -> Some(i)

    let left tree =
        match tree with
        | Empty -> failwith "tree is empty"
        | Node(_, _, left, _) ->
            match left with
            | Empty -> Empty
            | Node(_, i, left, right) -> Node(Black, i, left, right) // The root always has to be black

    let tryLeft tree =
        match tree with
        | Empty -> None
        | Node(_, _, left, _) -> 
            match left with
            | Empty -> None
            | Node(_, i, left, right) -> Some(Node(Black, i, left, right)) // The root always has to be black

    let right tree =
        match tree with
        | Empty -> failwith "tree is empty"
        | Node(_, _, _, right) -> 
            match right with
            | Empty -> Empty
            | Node(_, i, left, right) -> Node(Black, i, left, right) // The root always has to be black

    let tryRight tree =
        match tree with
        | Empty -> None
        | Node(_, _, _, right) ->
            match right with
            | Empty -> None
            | Node(_, i, left, right) -> Some(Node(Black, i, left, right)) // The root always has to be black

    let rec length tree =
        match tree with
        | Empty -> 0
        | Node(_, _, left, right) ->
            1 + (length left) + (length right)

    let rec maxDepth tree =
        match tree with
        | Empty -> 0
        | Node(_, _, left, right) ->
            let leftDepth = maxDepth left
            let rightDepth = maxDepth right
            (max leftDepth rightDepth) + 1


    let private balance = function
        | Black, z, Node(Red, x, a, Node(Red, y, b, c)), d                  // left, right
        | Black, z, Node(Red, y, Node(Red, x, a, b), c), d                  // left, left
        | Black, x, a, Node(Red, y, b, Node(Red, z, c, d))                  // right, right
        | Black, x, a, Node(Red, z, Node(Red, y, b, c), d)                  // right, left
            -> Node(Red, y, Node(Black, x, a, b), Node(Black, z, c, d))
        | color, item, left, right
            -> Node(color, item, left, right)

    let add item tree =
        let rec insert tree =
            match tree with
            | Empty -> Node(Red, item, Empty, Empty)
            | Node(color, i, left, right) as node ->
                if item = i then
                    node
                else if item < i then
                    balance(color, i, insert left, right)
                else
                    balance(color, i, left, insert right)

        let newTree = insert tree
        match newTree with
        | Empty -> failwith "tree shouldn't be empty"
        | Node(_, i, left, right) -> Node(Black, i, left, right)

    let rec contains item tree =
        match tree with
        | Empty -> false
        | Node(_, i, left, right)  ->
            if item = i then 
                true
            else if item < i then 
                contains item left
            else 
                contains item right

    let rec asInfixSequence tree =
        seq{
            match tree with
            | Empty -> yield! Seq.empty
            | Node(_, i, left, right) ->
                yield! asInfixSequence left
                yield i
                yield! asInfixSequence right
        }
