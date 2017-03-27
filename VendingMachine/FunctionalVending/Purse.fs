namespace FunctionalVending

module Purse =

    type T = {Money : Change.T list}

    let create =
        {Money = []}

    let add (change : Change.T) (purse : T) =
        // Keep the change ordered, with the highest denominations at the front of the list
        let rec loop (changes : Change.T list) = 
            match changes with
            | [] -> [change]
            | x :: xs when change.Denomination = x.Denomination -> (Change.addChange change x)::xs
            | x :: xs when change.Denomination > x.Denomination -> change :: x :: xs
            | x :: xs -> x :: (loop xs)

        let newMoney = loop purse.Money
        {Money = newMoney}

