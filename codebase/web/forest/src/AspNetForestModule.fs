namespace Axle.Web.AspNetCore.Forest

open System
open System.Collections.Concurrent
open Microsoft.AspNetCore.Http
open Axle
open Axle.Logging
open Axle.Modularity
open Axle.Web.AspNetCore.Session


[<AttributeUsage(AttributeTargets.Class|||AttributeTargets.Interface, Inherited = true, AllowMultiple = false)>]
type [<Sealed>] RequiresAspNetForest() = inherit RequiresAttribute(typeof<AspNetForestModule>)

and [<Sealed;NoComparison;Module;Forest.RequiresForest;RequiresAspNetSession>] internal AspNetForestModule(ctx : Forest.IForestContext, accessor : IHttpContextAccessor, logger : Axle.Logging.ILogger) = 
    let forestEnginesPerSession = new ConcurrentDictionary<string, Forest.ForestEngine>(StringComparer.Ordinal)

    let engineIdentity _ e = e

    interface ISessionEventListener with
        member __.OnSessionStart(session) =
            forestEnginesPerSession.AddOrUpdate(
                session.Id,
                Forest.ForestEngine(ctx),
                System.Func<string, Forest.ForestEngine, Forest.ForestEngine>(engineIdentity)) 
            |> ignore
            logger.Trace("Added http-session-bound forest context for session id {0}", session.Id)

        member __.OnSessionEnd(sessionId) =
            let (removed, _) = 
                sessionId 
                |> forestEnginesPerSession.TryRemove 
            if removed then logger.Trace("Removed http-session-bound forest context for session id {0}", sessionId)
            

    interface Forest.IForestEngineProvider with
        member __.Engine 
            with get() =
                let session = accessor.HttpContext.Session
                match null2vopt forestEnginesPerSession.[session.Id] with
                | ValueSome result -> result
                | ValueNone -> String.Format("A forest context bound to the current session ({0}) does not exist", session.Id) |> invalidOp

