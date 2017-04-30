namespace DataStructures

open System
open Microsoft.VisualStudio.TestTools.UnitTesting

module Expect =
    let some value =
        match value with
        | None -> raise(AssertFailedException("expected Some, got None"))
        | _ -> ()

    let none value =
        match value with
        | None -> ()
        | _ -> raise(AssertFailedException("expected None, got Some"))

    let equalTo expected value =
        match expected = value with
        | false -> raise(AssertFailedException(sprintf "expected %A got %A" expected value))
        | true -> ()

