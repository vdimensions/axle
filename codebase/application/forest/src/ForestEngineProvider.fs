namespace Axle.Forest

open Forest


type [<Interface>] IForestEngineProvider =
    abstract member Engine : Engine with get

type [<Sealed;NoEquality;NoComparison>] DefaultForestEngineProvider(engine : Engine) =
    let mutable _engine : Engine = engine
    interface IForestEngineProvider with member __.Engine with get() = _engine
