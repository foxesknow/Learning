namespace GeeksForGeeks
// Learn more about F# at http://fsharp.org
// See the 'F# Tutorial' project for more help.

module Program =
    [<EntryPoint>]
    let main argv = 
        let numbers = Nbonacci.calculate 3 8
        printfn "%A" numbers
        0 // return an integer exit code
