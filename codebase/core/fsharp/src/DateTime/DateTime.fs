namespace Axle
open System
open Axle.Verification
open Axle.Extensions.DateTime

/// <summary>
/// Module containing useful <see cref="System.DateTime" /> functions. 
/// Most of them are the functional equivalents of the extensions methods 
/// provided by the <see cref="Axle.Extensions.DateTime.DateTimeExtensions"/> 
/// and <see cref="Axle.Extensions.DateTime.DateTimeTimeZoneExtensions"/> classes.
/// </summary>
module DateTime =
    [<CompiledName "ChangeKind">]
    let changeKind (kind : DateTimeKind) (dateTime : DateTime) = dateTime.ChangeKind kind
    [<CompiledName "ChangeKindToLocal">]
    let changeKindToLocal = changeKind DateTimeKind.Local
    [<CompiledName "ChangeKindToUtc">]
    let changeKindToUtc = changeKind DateTimeKind.Utc

    [<CompiledName "ToLocalTime">]
    let toLocalTime (Default DateTimeKind.Local assumedKind) (dateTime : DateTime) =
        dateTime.ToLocalTime assumedKind

