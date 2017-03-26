namespace FunctionalVending

open Change

module Program =
    [<EntryPoint>]
    let main argv = 
        let c = [create 1 10; create 5 1]
        printfn "%A" (totalValueOf c)
        0 // return an integer exit code
