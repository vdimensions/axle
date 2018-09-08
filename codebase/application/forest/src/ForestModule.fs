namespace Axle.Application.Forest

open System

open Forest
open Forest.Reflection

open Axle.Application.Forest.Resources
open Axle.DependencyInjection
open Axle.Modularity


[<AttributeUsage(AttributeTargets.Class|||AttributeTargets.Interface, Inherited = true, AllowMultiple = false)>]
type [<Sealed>] RequiresForestAttribute() = inherit RequiresAttribute(typeof<ForestModule>)

 and [<Interface;Module;RequiresForest>] IForestViewProvider =
    abstract member RegisterViews: registry:IViewRegistry -> unit

 and [<Sealed;NoEquality;NoComparison;Module;Requires(typeof<ForestResourceModule>)>] internal ForestModule(container:IContainer) =
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

    [<ModuleDependencyInitialized>]
    member __.DependencyInitialized(vp:IForestViewProvider) =
        let ctx = container.Parent.Resolve<IForestContext>()
        ctx.ViewRegistry |> vp.RegisterViews

