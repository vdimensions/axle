namespace Axle.Application.Forest

open Axle.Application.Forest.Resources
open Axle.Application.Forest.Resources.Marshalling
open Axle.Modularity
open Axle.Resources
open Axle.Resources.Bundling
open Axle.Resources.Extraction


[<Module;Requires(typeof<ForestTemplateMarshallingModule>)>]
type [<Sealed;NoEquality;NoComparison>] internal ForestResourceModule(resourceManager:ResourceManager,frmm:ForestTemplateMarshallingModule) =
    [<ModuleInit>]
    member __.Init(exporter:ModuleExporter) =
        resourceManager |> ResourceTemplateProvider |> exporter.Export |> ignore

    interface IResourceBundleConfigurer with
        member __.Configure(registry:IResourceBundleRegistry): unit = 
            let parseUri = Axle.Conversion.Parsing.UriParser().Parse
            let bundle = ResourceManagerIntegration.BundleName
            registry.Configure(bundle)
                    .Register(("./"+bundle) |> parseUri)
                    .Extractors.Register(ResourceManagerIntegration.createExtractors(frmm.Marshallers))
            |> ignore
        

