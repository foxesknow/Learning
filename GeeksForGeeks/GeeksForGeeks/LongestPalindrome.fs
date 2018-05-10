namespace GeeksForGeeks

// https://www.geeksforgeeks.org/print-longest-palindrome-word-sentence/
module LongestPalindrome =

    let isPalindrome (word : string) =
        let normalized = word.ToLower()
        let length = normalized.Length
        let midPoint = (normalized.Length / 2) + (normalized.Length % 2)
        
        let rec loop index =
            if index = midPoint then
                true
            else
                if normalized.[index] <> normalized.[length - 1 - index] then
                    false
                else
                    loop (index + 1)

        loop 0

    let calculate (sentence : string) =
        let palidromes = sentence.Split(' ') 
                        |> Seq.toList
                        |> List.filter(fun word -> isPalindrome word)
                        |> List.mapi(fun index word -> word.Length, word)

        match palidromes with
        | [] -> None
        | items -> Some (List.maxBy fst items |> snd)

    let test() =
        let result = calculate "Tell me madam do you like Abba"
        printfn "%A" result

