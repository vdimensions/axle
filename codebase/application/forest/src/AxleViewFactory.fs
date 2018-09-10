namespace Axle.Application.Forest

open System

open Forest

open Axle
open Axle.DependencyInjection


type [<Sealed;NoEquality;NoComparison>] private AxleViewFactory(container:IContainer,app:Application) =
    let createSubContainer(c:IContainer) =
        app.DependencyContainerProvider.Create c
    interface IViewFactory with
        member __.Resolve(vm:IViewDescriptor) : IView =
            let x = ref Unchecked.defaultof<obj>
            use tmpContainer = createSubContainer(container)
            tmpContainer
                .RegisterType(vm.ViewType, vm.Name)
                .RegisterType(vm.ViewModelType)
                |> ignore
            if tmpContainer.TryResolve(vm.ViewType, x, vm.Name)
            then downcast !x:IView
            else raise <| new InvalidOperationException(String.Format("Type {0} was not registered in a dependency container.", vm.ViewType.FullName))

