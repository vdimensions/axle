namespace Axle.Forest

open Forest

type [<Interface>] IForestFacadeProvider =
    abstract member ForestFacade : IForestFacade with get

