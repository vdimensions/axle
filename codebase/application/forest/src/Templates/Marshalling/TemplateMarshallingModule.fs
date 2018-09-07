namespace Axle.Application.Forest.Resources.Marshalling

open System.Collections.Generic

open Axle.Modularity
open Axle.Resources


[<Module;RequiresResources;Requires(typeof<ForestTemplateMarshallingModule>)>]
type [<Interface>] IForestTemplateResourceMarshallerConfigurer =
    abstract member RegisterMarshallers: registry:IForestTemplateMarshallerRegistry -> unit

 and [<Module;Sealed;NoEquality;NoComparison>] internal ForestTemplateMarshallingModule =
    val marshallers:ICollection<IForestTemplateMarshaller>
    new() = { marshallers = LinkedList<_>() }
    [<ModuleInit>]
    member this.Init() =
        this |> this.ModuleDependencyInitialized

    [<ModuleTerminate>]
    member this.Terminate() =
        this |> this.ModuleDependencyTerminated

    [<ModuleDependencyInitialized>]
    member this.ModuleDependencyInitialized(cfg:IForestTemplateResourceMarshallerConfigurer) =
        this |> cfg.RegisterMarshallers

    [<ModuleDependencyTerminated>]
    member this.ModuleDependencyTerminated(cfg:IForestTemplateResourceMarshallerConfigurer) =
        ()

    member this.Marshallers
        with get() = upcast this.marshallers : IForestTemplateMarshaller seq

    interface IForestTemplateMarshallerRegistry with
        member this.Register m =
            m |> this.marshallers.Add
            upcast this:IForestTemplateMarshallerRegistry
        member this.Marshallers = this.Marshallers

    interface IForestTemplateResourceMarshallerConfigurer with
        member __.RegisterMarshallers registry =
            XmlTemplateMarshaller() |> registry.Register |> ignore
