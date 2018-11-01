namespace Axle.Web.AspNetCore.Extensions.Forest

open System.Runtime.CompilerServices


[<Extension>]
[<AutoOpen>]
type AspNetForestExtensions =
    [<Extension>]
    static member LoadAspNetForest(builder : Axle.IApplicationBuilder) =
        builder.Load(typeof<Axle.Web.AspNetCore.Forest.AspNetForestModule>)