namespace FunctionalVending

module BacktrackingAlgorithm =
    let calculate changeRequired (purse : Purse.T) =
        let rec main changeRequired (changes : Change.T list) (soFar : Purse.T) =
            match changes with
            | [] -> 
                // We've build a new change list, so try to calculate change
                GreedyAlgorithm.calculate changeRequired soFar
            | x :: xs ->
                // We're part way through building up a change structure.
                match main changeRequired xs soFar with
                | Some p -> 
                    // We've matched without adding the change at this level
                    Some p
                | None -> 
                    // We've not mananged a match with the change at this level.
                    // Start adding in the change, one at a time
                    let rec loopIndividual coins changeRequired (changes : Change.T list) (soFar : Purse.T) =
                        match coins with
                        | [] -> None
                        | c :: cs ->
                            let newPurse = Purse.add c soFar
                            match main changeRequired xs newPurse with
                            | Some p -> Some p
                            | _ -> loopIndividual cs changeRequired xs newPurse

                    loopIndividual (Change.individualChange x) changeRequired xs soFar

        main changeRequired purse.Money Purse.create

