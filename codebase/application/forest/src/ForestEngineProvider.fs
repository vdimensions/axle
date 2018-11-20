namespace Axle.Forest

open Forest


type [<Interface>] IForestEngineProvider =
    abstract member Engine : ForestEngine with get

type [<Sealed;NoEquality;NoComparison>] DefaultForestEngineProvider(engine : ForestEngine) =
    let mutable _engine : ForestEngine = engine
    interface IForestEngineProvider with member __.Engine with get() = _engine
