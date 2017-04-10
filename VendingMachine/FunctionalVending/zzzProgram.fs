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

        let purse2 =    purse |> Purse.subtract(Change.create 10 9)
        let t = Purse.totalValue purse;

        //Change.create 10 2 |> Change.individualChange |> List.iter (fun c -> printfn "%s" (Change.toString c))
        //Purse.individualChange purse |> List.iter (fun c -> printfn "%s" (Change.toString c))

        //purse2.Money |> List.iter (fun c -> printfn "%s" (Change.toString c))

        match BacktrackingAlgorithm.calculate 8 purse with
        | Some purse -> purse.Money |> List.iter (fun c -> printfn "%s" (Change.toString c))
        | None -> printfn "no change available"


        0 // return an integer exit code
