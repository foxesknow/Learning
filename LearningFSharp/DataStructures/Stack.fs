namespace DataStructures

module Stack =

    type 'a T =
        internal
        | Empty
        | StackItem of 'a * 'a T


    let empty =
        Empty

    let isEmpty stack =
        match stack with
        | Empty -> true
        | _ -> false
    
    let push value stack =
        StackItem(value, stack)

    let top stack =
        match stack with
        | StackItem(value, rest) -> value
        | Empty -> failwith "stack is empty"

    let tryTop stack =
        match stack with
        | StackItem(value, rest) -> Some value
        | _ -> None

    let pop stack =
        match stack with
        | StackItem(value, rest) -> rest
        | Empty -> failwith "stack is empty"

    let tryPop stack =
        match stack with
        | StackItem(value, rest) -> Some rest
        | Empty -> None

    let topAndPop stack =
        match stack with
        | StackItem(value, rest) -> (value, rest)
        | _ -> failwith "stack is empty"

    let tryTopAndPop stack =
        match stack with
        | StackItem(value, rest) -> Some (value, rest)
        | _ -> None

    let rec fold f state stack =
        match stack with
        | StackItem(item, rest) -> fold f (f state item) rest
        | Empty -> state

    let length stack =
        fold (fun state i -> state + 1) 0 stack

    let reverse stack =
        fold (fun state i -> push i state) Empty stack

