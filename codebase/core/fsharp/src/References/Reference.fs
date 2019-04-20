namespace Axle.References


[<CompiledName("Reference")>]
[<AutoOpen>]
module Reference =
    let ref2opt (r : IReference<'a>) =
        match r.TryGetValue() with
        | true, value -> Some value
        | false, _ -> None

    let ref2vopt (r : IReference<'a>) =
        match r.TryGetValue() with
        | true, value -> ValueSome value
        | false, _ -> ValueNone