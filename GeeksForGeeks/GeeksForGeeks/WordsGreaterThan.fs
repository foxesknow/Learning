namespace GeeksForGeeks

open System

// https://www.geeksforgeeks.org/find-words-greater-given-length-k/
module WordsGreaterThan =
    let calculate size (sentence : string) =
        sentence.Split(' ') |> Seq.filter (fun word -> word.Length >= size)

    let test() =
        let words = calculate 5 "the quick brown fox jumped over the lazy hen"
        printfn "%A" words

