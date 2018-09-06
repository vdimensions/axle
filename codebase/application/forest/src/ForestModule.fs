namespace Axle.Application.Forest

open Forest
open Forest.Reflection

open Axle.DependencyInjection
open Axle.Modularity
open Axle.Resources
open Axle.Resources.Bundling
open Axle.Resources.Extraction


[<Module>]
[<Requires(typeof<ForestModule>)>]
type [<Interface>] IForestViewProvider =
    abstract member RegisterViews: registry:IViewRegistry -> unit

 and [<Module;Sealed;NoEquality;NoComparison>] internal ForestModule(container:IContainer, resourceManager:ResourceManager) =
    [<ModuleInit>]
    member __.Init(exporter:ModuleExporter) =
        let reflectionProvider =
            match container.TryResolve<IReflectionProvider>() with
            | (true, rp) -> rp
            | (false, _) -> upcast DefaultReflectionProvider()
        let viewFactory = upcast AxleViewFactory(container) : IViewFactory
        let viewRegistry = upcast ContainerViewRegistry(container, DefaultViewRegistry(viewFactory, reflectionProvider)) : IViewRegistry
        let securityManager =
            match container.TryResolve<ISecurityManager>() with
            | (true, sm) -> sm
            | (false, _) -> upcast NoopSecurityManager()
        let context = upcast DefaultForestContext(viewRegistry, securityManager) : IForestContext

        context |> exporter.Export |> ignore
        resourceManager |> ResourceTemplateProvider |> exporter.Export |> ignore

    [<ModuleDependencyInitialized>]
    member __.DependencyInitialized(vp:IForestViewProvider) =
        let ctx = container.Parent.Resolve<IForestContext>()
        ctx.ViewRegistry |> vp.RegisterViews

    //[<ModuleDependencyTerminated>]
    //member __.DependencyTerminated(vp:IForestViewProvider) = ()

    interface IResourceBundleConfigurer with
        member __.Configure(registry:IResourceBundleRegistry): unit = 
            let parseUri = Axle.Conversion.Parsing.UriParser().Parse
            let bundle = ResourceManagerIntegration.BundleName
            registry.Configure(bundle)
                    .Register(("./"+bundle) |> parseUri)
                    .Extractors.Register(ResourceManagerIntegration.createExtractors())
            |> ignore
        

