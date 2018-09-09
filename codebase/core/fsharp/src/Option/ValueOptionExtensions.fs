namespace Axle.Extesions.Option

open System.Runtime.CompilerServices

open Axle.Option

/// Credit goes to Luis Diego Fallas @ http://langexplr.blogspot.com/2008/06/using-f-option-types-in-c.html
[<Extension>]
type ValueOptionExtensions =
   [<Extension>]
   static member HasValue<'a> (opt:'a voption) =
        match opt with
        | ValueSome _ -> true
        | ValueNone -> false

   [<Extension>]
   static member GetValueOrDefault<'a>(opt:'a voption, defaultValue:'a) =
        match opt with
        | ValueSome v -> v
        | ValueNone -> defaultValue

   [<Extension>]
   static member AsNullsafe<'a when 'a : not struct>(opt:'a voption) = null2vopt opt

