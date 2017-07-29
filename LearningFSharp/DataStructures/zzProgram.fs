namespace DataStructures
// Learn more about F# at http://fsharp.org
// See the 'F# Tutorial' project for more help.

module Program = 
    [<EntryPoint>]
    let main argv = 
        let q = Queue.empty
                |> Queue.enqueue 1
                |> Queue.enqueue 2
                |> Queue.enqueue 3

        let printer item queue =
            printfn "%A" item
            Queue.tryFrontAndDequeue queue

        Queue.tryFrontAndDequeue q
        |> Option.bind(fun (top, q) -> printer top q)
        |> Option.bind(fun (top, q) -> printer top q)
        |> Option.bind(fun (top, q) -> printer top q)
        |> Option.bind(fun (top, q) -> printer top q)
        |> ignore

        printfn "There are %A items" (Queue.length q)
        
        0 // return an integer exit code
