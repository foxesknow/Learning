namespace GeeksForGeeks

// https://www.geeksforgeeks.org/n-bonacci-numbers/
module Nbonacci =

    let calculate n m =
        let zeros = [for i in 1 .. n - 1 -> 0]
        let initialSequence = 1 :: zeros

        let rec loop window total m acc =
            if total = m then
                acc
            else
                let sum = Seq.take window acc |> Seq.sum
                loop window (total + 1) m (sum :: acc)

        loop n n m initialSequence |> List.rev

