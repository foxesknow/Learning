namespace DataStructures

module Queue =

    type 'a T = {Front : Stack.T<'a>; Back : Stack.T<'a>}

    let private rebalance queue =
        match queue with
        | {Front = Stack.Empty; Back = back} -> {Front = Stack.reverse back; Back = Stack.empty}
        | _ -> queue

    let empty =
        {Front = Stack.empty; Back = Stack.empty}

    let enqueue item queue =
        rebalance {queue with Back = Stack.push item queue.Back}

    let front queue =
        match queue.Front with
        | Stack.Empty -> failwith "nothing in queue"
        | s -> Stack.top s

    let tryFront queue =
        Stack.tryTop queue.Front

    let dequeue queue =
        match queue.Front with
        | Stack.Empty -> failwith "nothing in queue"
        | s -> rebalance {queue with Front = Stack.pop s}

    let tryDequeue queue =
        match queue.Front with
        | Stack.Empty -> None
        | s -> Some(rebalance {queue with Front = Stack.pop s})

    let tryFrontAndDequeue queue =
        match queue.Front with
        | Stack.Empty -> None
        | s -> Some(Stack.top s, rebalance {queue with Front = Stack.pop s})

    let length queue =
        (Stack.length queue.Front) + (Stack.length queue.Back)

