﻿namespace FunctionalVending

module GreedyAlgorithm =

    let calculate changeRequired (purse : Purse.T) =
        let rec loop changeRequired (purse : Purse.T) (soFar : Purse.T) =
            if changeRequired = 0 then
                Some soFar
            else
                match purse.Money with
                | [] when changeRequired > 0 -> None

                | [] when changeRequired = 0 -> Some soFar

                | x :: xs when x.Denomination <= changeRequired ->
                    let coinsToTake = min x.Quantity (changeRequired / x.Denomination)
                    let changeHere = Change.create x.Denomination coinsToTake
                    let amountToRemove = coinsToTake * x.Denomination                    
                    loop (changeRequired - amountToRemove) {purse with Money = xs} (Purse.add changeHere soFar)

                | x :: xs -> loop changeRequired {purse with Money = xs} soFar 

                | _ -> None

        loop changeRequired purse Purse.create
