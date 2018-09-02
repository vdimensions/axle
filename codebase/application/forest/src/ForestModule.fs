namespace Axle.Application.Forest

open System

open Forest
open Forest.Reflection

open Axle.DependencyInjection
open Axle.Modularity


[<Module>]
[<Requires(typeof<ForestModule>)>]
type [<Interface>] IForestViewProvider =
    abstract member RegisterViews: registry:IViewRegistry -> unit

 and private AxleViewFactory(container:IContainer) =
    interface IViewFactory with
        member __.Resolve(vm:IViewDescriptor) : IView = 
            let x = ref Unchecked.defaultof<obj>
            if container.TryResolve(vm.ViewType, x, vm.Name) || container.TryResolve(vm.ViewType, x)
            then downcast !x:IView
            else raise <| new ViewInstantiationException(vm.ViewType)

 and private ContainerViewRegistry(container:IContainer, originalRegistry:IViewRegistry) =
    interface IViewRegistry with
        member __.GetDescriptor(viewType:Type):IViewDescriptor = 
            originalRegistry.GetDescriptor viewType
        member __.GetDescriptor(name:string):IViewDescriptor = 
            originalRegistry.GetDescriptor name
        member __.Register<'T when 'T:>IView>():IViewRegistry = 
            container.RegisterType<'T>() |> ignore
            originalRegistry.Register<'T>()
        member __.Register(t:Type):IViewRegistry = 
            container.RegisterType t |> ignore
            originalRegistry.Register t
        member __.Resolve(viewType:Type):IView = 
            originalRegistry.Resolve viewType
        member __.Resolve(name:string): IView = 
            originalRegistry.Resolve name

 and [<Module;NoEquality;NoComparison>] internal ForestModule(container:IContainer) =
    [<ModuleInit>]
    member __.Init(exporter:ModuleExporter) =
        let viewFactory =
            match container.TryResolve<IViewFactory>() with
            | (true, vf) -> vf
            | (false, _) -> upcast AxleViewFactory(container)
        let reflectionProvider =
            match container.TryResolve<IReflectionProvider>() with
            | (true, rp) -> rp
            | (false, _) -> upcast DefaultReflectionProvider()
        let securityManager =
            match container.TryResolve<ISecurityManager>() with
            | (true, rp) -> rp
            | (false, _) -> upcast NoopSecurityManager()
        let context = upcast DefaultForestContext(viewFactory, reflectionProvider, securityManager):IForestContext
        context |> exporter.Export |> ignore

    [<ModuleDependencyInitialized>]
    member __.DependencyInitialized(vp:IForestViewProvider) =
        let ctx = container.Parent.Resolve<IForestContext>()
        ContainerViewRegistry(container, ctx.ViewRegistry) |> vp.RegisterViews

    [<ModuleDependencyTerminated>]
    member __.DependencyTerminated(vp:IForestViewProvider) =
        ()

