namespace Axle.References

[<CompiledName("Nullsafe")>]
module Nullsafe =
    [<CompiledName("Map")>]
    let map (mapping:('T -> 'U)) (value:Axle.References.Nullsafe<'T>) =
        if value.HasValue 
        then value.Value |> mapping |> Nullsafe.Some
        else Nullsafe.None
        
    //let inline (|Nullsafe|) arg = Nullsafe.Some arg