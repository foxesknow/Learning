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

    let subtract (change : Change.T) (purse : T) =
        let rec loop (changes : Change.T list) = 
            match changes with
            | [] -> []
            | x :: xs when change.Denomination = x.Denomination -> 
                if x.Quantity = change.Quantity then
                    xs
                else if x.Quantity > change.Quantity then
                    (Change.create change.Denomination (x.Quantity - change.Quantity)) :: xs
                else
                    failwith "not enough denomination to subtract"
            | x :: xs when change.Denomination > x.Denomination -> x :: xs
            | x :: xs -> x :: (loop xs)

        let newMoney = loop purse.Money
        {Money = newMoney}
        

    // Adds 2 purses together
    let combine purse1 purse2 = 
        List.fold (fun purse change -> add change purse) purse2 purse1.Money

    let totalValue purse =
        Change.totalValueOf purse.Money

    let individualChange purse =
        List.collect(fun cs -> Change.individualChange cs) purse.Money
