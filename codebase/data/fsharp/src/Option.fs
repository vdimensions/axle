namespace Axle.Data

open System

[<AutoOpen>]
[<CompiledName("Option")>]
module Option =
    let opt2dbnull (value : 'a option) : obj = 
        match value with
        | Some v -> box v
        | None -> box DBNull.Value 

