namespace Axle.Application.Forest

open System.Globalization

open Forest.Templates.Raw

open Axle.Resources


type [<Sealed>] ResourceTemplateProvider(rm:ResourceManager) =
    interface ITemplateProvider with
        member __.Load name =
            let bundle = ResourceManagerIntegration.BundleName
            let culture = CultureInfo.InvariantCulture
            let resource = rm.Load(bundle, name, culture)
            match resource with
            | :? ResourceManagerIntegration.TemplateResourceInfo as tri -> Template.Root(tri.Value)
            | _ -> raise <| ResourceNotFoundException(name, bundle, culture)


