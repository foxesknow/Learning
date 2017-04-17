namespace DataStructures
// Learn more about F# at http://fsharp.org
// See the 'F# Tutorial' project for more help.

module Program = 
    [<EntryPoint>]
    let main argv = 

        let s = Stack.empty
                |> Stack.push 10

        printfn "There are %A items" (Stack.length s)
        
        0 // return an integer exit code
