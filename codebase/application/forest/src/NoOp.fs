namespace Axle.Forest
open Forest
open Forest.UI

module private NoOp =

    [<Literal>]
    let private DefaultErrorMessage = "Forest is not initialized"

    type [<Sealed;NoComparison;NoEquality>] private Renderer(message : string) =
        inherit AbstractPhysicalViewRenderer<IPhysicalView>()
        override __.CreateNestedPhysicalView _ _ _ = invalidOp message
        override __.CreatePhysicalView _ _ = invalidOp message

    type [<Sealed;NoComparison;NoEquality>] Facade(ctx : IForestContext) =
        inherit DefaultForestFacade<IPhysicalView>(ctx, Renderer(DefaultErrorMessage))
        override __.LoadTemplate _ = invalidOp DefaultErrorMessage
        override __.SendMessage _ = invalidOp DefaultErrorMessage
        override __.ExecuteCommand _ _ _ = invalidOp DefaultErrorMessage


