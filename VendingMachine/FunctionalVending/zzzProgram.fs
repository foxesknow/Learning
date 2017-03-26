namespace FunctionalVending

module Program =
    [<EntryPoint>]
    let main argv = 
        let m1 =    VendingMachine.create
                    |> VendingMachine.add (Change.create 50 2)
                    |> VendingMachine.add (Change.create 10 1)
                    |> VendingMachine.add (Change.create 20 3)
                    |> VendingMachine.add (Change.create 10 8)

        m1.MoneyInMachine |> List.iter (fun c -> printfn "%s" (Change.toString c))

        0 // return an integer exit code
