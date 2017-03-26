namespace FunctionalVending

module Change =

    type T = {Denomination : int; Quantity : int}

    let create denomination quantity =
        {Denomination = denomination; Quantity = quantity}

    let totalValue change =
        change.Denomination * change.Quantity

    let (++) change quantity =
        let newQuantity = change.Quantity + quantity

        if(newQuantity < 1) then
            invalidArg "quantity" "new quantity would be 0 or less"
        else
            {change with Quantity = newQuantity}

    let (+++) change changeToAdd =
        if change.Denomination <> changeToAdd.Denomination then
            invalidArg "changeToAdd" "denominations do not match"
        else
            {change with Quantity = change.Quantity + changeToAdd.Quantity}

    //let totalValueOf changes =
        //List.sumBy (fun c -> totalValue c) changes

    let totalValueOf changes =
        let rec loop acc = function
            | [] -> acc
            | x::xs -> loop (acc + totalValue x) xs

        loop 0 changes



        

 

