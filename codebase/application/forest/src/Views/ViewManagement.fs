namespace Axle.Application.Forest

open System

open Forest

open Axle.DependencyInjection


type [<Sealed;NoEquality;NoComparison>] private AxleViewFactory(container:IContainer) =
    interface IViewFactory with
        member __.Resolve(vm:IViewDescriptor) : IView = 
            let x = ref Unchecked.defaultof<obj>
            if container.TryResolve(vm.ViewType, x, vm.Name) || container.TryResolve(vm.ViewType, x)
            then downcast !x:IView
            else raise <| new InvalidOperationException(String.Format("Type {0} was not registered in a dependency container.", vm.ViewType.FullName))

type [<Sealed;NoEquality;NoComparison>] private ContainerViewRegistry(container:IContainer, originalRegistry:IViewRegistry) =
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
            // this will call AxleViewFactory
            originalRegistry.Resolve viewType
        member __.Resolve(name:string): IView = 
            // this will call AxleViewFactory
            originalRegistry.Resolve name
