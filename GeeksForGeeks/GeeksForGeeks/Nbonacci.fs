namespace GeeksForGeeks

// https://www.geeksforgeeks.org/n-bonacci-numbers/

// For an n-bonacci sequence we start with n numbers where the first n-1 are 0 and the next is 1 (eg 0, 0, 1) for n = 3
// We then generate the sequence by adding the previous n numbers together to get the next number in the sequence until
// we have m numbers.
//
// A fibonacci seqence would have n = 2
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


    let test() =
        let numbers = calculate 3 8
        printfn "%A" numbers

