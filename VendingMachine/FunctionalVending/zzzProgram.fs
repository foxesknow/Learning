namespace FunctionalVending

module Program =
    [<EntryPoint>]
    let main argv = 
        let purse = Purse.create
                    |> Purse.add (Change.create 50 2)
                    |> Purse.add (Change.create 10 1)
                    |> Purse.add (Change.create 20 3)
                    |> Purse.add (Change.create 10 8)
                    |> Purse.add (Change.create 5 2)
                    |> Purse.add (Change.create 2 3)

        purse.Money |> List.iter (fun c -> printfn "%s" (Change.toString c))

        printfn ""
        printfn "Calculating change"

        let change = GreedyAlgorithm.calculate 77 purse

        match change with
        | Some purse -> purse.Money |> List.iter (fun c -> printfn "%s" (Change.toString c))
        | None -> printfn "no change available"

        0 // return an integer exit code
