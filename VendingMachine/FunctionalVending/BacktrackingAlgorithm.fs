namespace FunctionalVending

module BacktrackingAlgorithm =
    let calculate changeRequired (purse : Purse.T) =
        let rec mainLoop changeRequired (changes : Change.T list) (soFar : Purse.T) =
            match changes with
            | [] -> 
                // We've build a new change list, so try to calculate change
                //List.iter (fun c -> printf "(%s) " (Change.toString c)) soFar.Money; printfn ""
                GreedyAlgorithm.calculate changeRequired soFar
            | coin :: coins ->
                // We've not mananged a match with the change after this level so start adding in the change, one at a time
                let rec levelLoop individualCoins changeRequired (changes : Change.T list) (soFar : Purse.T) =
                    match individualCoins with
                    | [] -> 
                        // We've added all the coins and not got a match
                        None
                    | levelCoin :: levelCoins ->
                        // Add the next coin to the purse and try a match
                        let newPurse = Purse.add levelCoin soFar
                        match mainLoop changeRequired coins newPurse with
                        | Some p -> 
                            // Bingo!
                            Some p
                        | None -> 
                            // No luck, so loop again with the remaining coins
                            levelLoop levelCoins changeRequired coins newPurse

                match levelLoop (Change.individualChange coin) changeRequired coins soFar with
                | Some p -> Some p
                | None -> mainLoop changeRequired coins soFar

        mainLoop changeRequired purse.Money Purse.create

