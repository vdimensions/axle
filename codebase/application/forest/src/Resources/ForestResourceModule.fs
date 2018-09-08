namespace Axle.Application.Forest.Resources

open System

open Axle.Application.Forest.Resources
open Axle.Modularity
open Axle.Resources
open Axle.Resources.Extraction


[<Module;RequiresResources;Requires(typeof<ForestResourceModule>)>]
type [<Interface>] IForestTemplateMarshallerConfigurer =
    abstract member RegisterMarshallers: registry:IForestTemplateMarshallerRegistry -> unit

 and [<Sealed;NoEquality;NoComparison;Module;RequiresResources>] internal ForestResourceModule =
    val private resourceManager:ResourceManager
    val private templateProvider:ResourceTemplateProvider
    new(rm:ResourceManager) = { resourceManager = rm; templateProvider = ResourceTemplateProvider(rm) }

    [<ModuleInit>]
    member this.Init(e:ModuleExporter) =
        this |> this.ModuleDependencyInitialized
        e.Export(this.templateProvider) |> ignore

    [<ModuleTerminate>]
    member this.Terminate() =
        this |> this.ModuleDependencyTerminated

    [<ModuleDependencyInitialized>]
    member this.ModuleDependencyInitialized(cfg:IForestTemplateMarshallerConfigurer) =
        this |> cfg.RegisterMarshallers

    [<ModuleDependencyTerminated>]
    member __.ModuleDependencyTerminated(_:IForestTemplateMarshallerConfigurer) =
        ()

    interface IForestTemplateMarshallerConfigurer with
        member __.RegisterMarshallers registry =
            // 
            // Enable support for XML templates out of the box
            //
            XmlTemplateMarshaller() |> registry.Register |> ignore

    interface IForestTemplateMarshallerRegistry with
        member this.Register m =
            let parseUri = (new Axle.Conversion.Parsing.UriParser()).Parse
            let marshallingExtractor = MarshallingTemplateExtractor(m)
            let formatSpecificBundleName = String.Format("{0}/{1}", ResourceTemplateProvider.BundleName, marshallingExtractor.Extension)
            this.resourceManager.Bundles
                .Configure(formatSpecificBundleName)
                .Register(String.Format("./{0}", ResourceTemplateProvider.BundleName) |> parseUri)
                .Register(String.Format("./{0}", formatSpecificBundleName) |> parseUri)
                .Extractors.Register(marshallingExtractor.ToExtractorList())
                |> ignore
            formatSpecificBundleName |> this.templateProvider.AddBundle
            upcast this:IForestTemplateMarshallerRegistry
