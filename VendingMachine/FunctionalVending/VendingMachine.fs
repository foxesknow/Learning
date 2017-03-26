namespace FunctionalVending

module VendingMachine =

    type T = {MoneyInMachine : Change.T list}

    let create =
        {MoneyInMachine = []}

    let add (change : Change.T) (machine : T) =
        let rec loop (changes : Change.T list) = 
            match changes with
            | [] -> [change]
            | x :: xs when change.Denomination = x.Denomination -> (Change.addChange change x)::xs
            | x :: xs when change.Denomination > x.Denomination -> change :: x :: xs
            | x :: xs -> x :: (loop xs)

        let newMoney = loop machine.MoneyInMachine
        {MoneyInMachine = newMoney}

