namespace Axle

open Axle.References

[<AutoOpen>]
[<CompiledName("Option")>]
module Option =
    let null2opt value = if (obj.ReferenceEquals(null, value)) then None else Some value
    let nullable2opt (value: System.Nullable<'a>) = if value.HasValue then Some value.Value else None
    let ns2opt (value: Nullsafe<'a>) = if value.HasValue then Some value.Value else None
    let opt2ns (value: 'a option) = match value with Some v -> v |> Nullsafe<'a>.Some | None -> Nullsafe<'a>.None

    let null2vopt value = if (obj.ReferenceEquals(null, value)) then ValueNone else ValueSome value
    let nullable2vopt (value: System.Nullable<'a>) = if value.HasValue then ValueSome value.Value else ValueNone
    let ns2vopt (value: Nullsafe<'a>) = if value.HasValue then ValueSome value.Value else ValueNone
    let vopt2ns (value: 'a voption) = match value with ValueSome v -> v |> Nullsafe<'a>.Some | ValueNone -> Nullsafe<'a>.None
   