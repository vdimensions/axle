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
            use tmpContainer = createSubContainer(container)
            tmpContainer
                .RegisterType(vm.ViewType, vm.Name)
                .RegisterType(vm.ViewModelType)
                |> ignore
            // let exception pop, Forest will wrap it up accordingly
            downcast (tmpContainer.Resolve(vm.ViewType, vm.Name)):IView

