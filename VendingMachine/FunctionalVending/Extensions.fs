namespace FunctionalVending

module Option =
    let firstSome (predicate : 'T -> Option<'U>) (items : 'T list) =
        let rec loop i =
            match i with
            | [] -> None
            | x :: xs ->
                match predicate x with
                | Some x -> Some x
                | None -> loop xs

        loop items

