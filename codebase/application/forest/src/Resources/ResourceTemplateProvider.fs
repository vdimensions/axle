namespace Axle.Application.Forest.Resources

open System.Globalization

open Forest.Templates.Raw

open Axle.Resources


type [<Sealed>] ResourceTemplateProvider(rm:ResourceManager) =
    [<DefaultValue>]
    val mutable private bundles:string list voption

    static member BundleName = "ForestTemplates"

    member internal this.AddBundle(bundle:string) = 
        this.bundles <- 
            match this.bundles with
            | ValueSome b -> bundle::b
            | ValueNone -> [bundle]
            |> ValueSome
        
    interface ITemplateProvider with
        member this.Load name =
            let rec load bundles =
                match bundles with
                | [] -> raise <| ResourceNotFoundException(name, ResourceTemplateProvider.BundleName, CultureInfo.InvariantCulture)
                | bundle::rest ->
                    let template = rm.Load<Template>(bundle, name, CultureInfo.InvariantCulture)
                    if not <| obj.ReferenceEquals(template, null) 
                    then template
                    else load rest
            match this.bundles with
            | ValueSome b -> b
            | ValueNone -> []
            |> load
            