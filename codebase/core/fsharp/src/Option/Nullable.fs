namespace Axle

[<CompiledName("Nullable")>]
module Nullable =
    [<CompiledName("Map")>]
    let map (mapping:('T -> 'U)) (value:System.Nullable<'T>) =
        if value.HasValue 
        then value.Value |> mapping |> System.Nullable<'U>
        else System.Nullable<'U>()
        
