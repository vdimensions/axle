namespace InCogLab.Calendar.Web

open System
open System.Collections.Concurrent
open Microsoft.AspNetCore.Http
open Axle.Logging
open Axle.Forest
open Axle.Verification
open Axle.Modularity
open Axle.Web.AspNetCore.Session
open Forest
open Forest.WebSharper


type private WebSharperForestFacade(forestContext : IForestContext, renderer : WebSharperPhysicalViewRenderer) =
    inherit DefaultForestFacade<WebSharperPhysicalView>(forestContext, renderer)
    member __.Renderer with get() = renderer

type private PerSessionWebSharperForestFacade(httpContextAccessor : IHttpContextAccessor) =
    inherit SessionScoped<WebSharperForestFacade>(httpContextAccessor)

[<AttributeUsage(AttributeTargets.Class|||AttributeTargets.Interface, Inherited = true, AllowMultiple = false)>]
type [<Sealed;NoComparison>] RequiresWebSharperForestAttribute() = inherit RequiresAttribute(typeof<WebSharperForestModule>)

and [<Interface;RequiresWebSharperForest>] IWebSharperTemplateConfigurer =
    abstract member Configure: registry : IWebSharperTemplateRegistry -> unit

and [<Sealed;NoComparison;NoEquality;Module;RequiresForest;RequiresWebSharper;RequiresAspNetSession>] internal WebSharperForestModule(
        forestContext : IForestContext, 
        httpContextAccessor : IHttpContextAccessor,
        logger : Axle.Logging.ILogger) = 

    let perSessionForesFacade = new PerSessionWebSharperForestFacade(httpContextAccessor)
    let registeredPhysicalViewFacories = ConcurrentDictionary<vname, WebSharperPhysicalViewFactory>(StringComparer.Ordinal)

    [<ModuleTerminate>]
    member __.Terminated() =
        (upcast perSessionForesFacade : IDisposable).Dispose()
        registeredPhysicalViewFacories.Clear()

    [<ModuleDependencyInitialized>]
    member registry.DependencyInitialized (c : IWebSharperTemplateConfigurer) = c.Configure registry

    interface ISessionEventListener with
        member this.OnSessionStart session =
            let sessionId = session.Id
            perSessionForesFacade.AddOrReplace(
                sessionId, 
                WebSharperForestFacade(forestContext, WebSharperPhysicalViewRenderer(this)),
                new Func<_,_,_>(fun _ _ -> WebSharperForestFacade(forestContext, WebSharperPhysicalViewRenderer(this))))
            logger.Trace("WebSharper forest facade for session {0} created", sessionId)

        member __.OnSessionEnd sessionId =
            match perSessionForesFacade.TryRemove sessionId with
            | (true, _) -> logger.Trace("WebSharper forest facade for session {0} deleted", sessionId)
            | _ -> ignore()

    interface IForestFacadeProvider with
        member __.ForestFacade with get() = upcast perSessionForesFacade.Current

    interface IDocumentRenderer with
        member __.Doc() = (perSessionForesFacade.Current.Renderer :> IDocumentRenderer).Doc()

    interface IWebSharperTemplateRegistry with
        member this.Register (NotNull "name" name) (NotNull "factory" factory) =
            registeredPhysicalViewFacories.AddOrUpdate(
                name, 
                factory, 
                System.Func<vname, _, _>(fun n _ -> invalidOp <| String.Format("A physical view with the provided name '{0}' is already registered", n)))
            |> ignore
            (this :> IWebSharperTemplateRegistry)

        member __.Get (NotNull "commandDispatcher" commandDispatcher) (NotNull "domNode" domNode) =
            let name = domNode.Name
            match registeredPhysicalViewFacories.TryGetValue(name) with
            | (true, value) -> value(commandDispatcher, domNode)
            | (false, _) -> invalidOp <| String.Format("A physical view with the provided name '{0}' could not be found", name)
