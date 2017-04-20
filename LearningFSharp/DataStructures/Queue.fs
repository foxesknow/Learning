namespace DataStructures

module Queue =

    type 'a T =
        | Empty
        | Queue of Stack.T<'a> * Stack.T<'a>
    
    let private rebalance queue =
        match queue with
        | Empty -> Empty
        | Queue(front, back) when front = Stack.Empty && back = Stack.Empty -> Empty
        | Queue(front, back) when front = Stack.Empty -> Queue(Stack.reverse back, Stack.Empty)
        | _ -> queue

    let empty =
        Empty

    let isEmpty queue =
        match queue with
        | Empty -> true
        | _ -> false

    let enqueue item queue =
        match queue with
        | Empty -> rebalance(Queue(Stack.Empty, (Stack.Empty |> Stack.push item)))
        | Queue(front, back) -> rebalance(Queue(front, Stack.push item back))


    let front queue =
        match queue with
        | Empty -> failwith "queue is empty"
        | Queue(front, _) -> Stack.top front

    let tryFront queue =
        match queue with
        | Empty -> None
        | Queue(front, _) -> Stack.tryTop front

    let dequeue queue =
        match queue with
        | Empty -> failwith "queue is empty"
        | Queue(front, back) -> rebalance(Queue(Stack.pop front, back))

    let tryDequeue queue =
        match queue with
        | Empty -> None
        | _ -> Some(dequeue queue)

    let tryFrontAndDequeue queue =
        match queue with
        | Empty -> None
        | Queue(front, back) -> 
            let top = Stack.top front
            let q = rebalance(Queue(Stack.pop front, back))
            Some(top, q)

    let length queue =
        match queue with
        | Empty -> 0
        | Queue(front, back) -> (Stack.length front) + (Stack.length back)

