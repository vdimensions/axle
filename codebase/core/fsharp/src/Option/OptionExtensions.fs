namespace Axle.Extesions.Option

open System.Runtime.CompilerServices

open Axle.Option

/// Credit goes to Luis Diego Fallas @ http://langexplr.blogspot.com/2008/06/using-f-option-types-in-c.html
[<Extension>]
type OptionExtensions =
   [<Extension>]
   static member HasValue<'a>(opt:'a option) =
        match opt with
        | Some _ -> true
        | None -> false

   [<Extension>]
   static member GetValueOrDefault<'a>(opt:'a option, defaultValue:'a) =
        match opt with
        | Some v -> v
        | None -> defaultValue

   [<Extension>]
   static member AsNullsafe<'a when 'a : not struct>(opt:'a option) = opt2ns opt

