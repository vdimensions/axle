namespace Axle.Application.Forest

open System
open System.Diagnostics
open System.Reflection

open Forest
open Forest.Reflection
open Forest.Security
open Forest.Templates.Raw
open Forest.Templates
open Forest.UI

open Axle
open Axle.Application.Forest.Resources
open Axle.DependencyInjection
open Axle.Logging
open Axle.Modularity


/// An interface allowing communication between the physical application front-end and the Forest UI layer
type [<Interface>] IForestFacade = 
    inherit ICommandDispatcher
    inherit IMessageDispatcher
    abstract member LoadTemplate: template:string -> unit

type [<Interface>] IForestRendererConfigurer =
    abstract member SetRenderer: renderer:IDomProcessor -> unit

[<AttributeUsage(AttributeTargets.Class|||AttributeTargets.Interface, Inherited = true, AllowMultiple = false)>]
type [<Sealed>] RequiresForestAttribute() = inherit RequiresAttribute(typeof<ForestModule>)

and [<Interface;Module;RequiresForest>] IForestViewProvider =
    abstract member RegisterViews: registry:IViewRegistry -> unit

and [<Sealed;NoEquality;NoComparison;Module;Requires(typeof<ForestResourceModule>)>] 
    internal ForestModule(container : IContainer, templateProvider : ITemplateProvider, app : Application, rtp : ResourceTemplateProvider, logger : ILogger) =
    [<DefaultValue>]
    val mutable private _context:IForestContext
    [<DefaultValue>]
    val mutable private _engine:Engine
    [<DefaultValue>]
    val mutable private _result:ForestResult
    [<DefaultValue>]
    val mutable private _renderer:IDomProcessor

    [<ModuleInit>]
    member this.Init(e : ModuleExporter) =
        let reflectionProvider =
            match container.TryResolve<IReflectionProvider>() with
            | (true, rp) -> rp
            | (false, _) -> upcast DefaultReflectionProvider()
        let securityManager =
            match container.TryResolve<ISecurityManager>() with
            | (true, sm) -> sm
            | (false, _) -> upcast NoopSecurityManager()
        let viewFactory =
            match null2vopt container.Parent with
            | ValueSome c -> (c, app)
            | ValueNone -> (container, app)
            |> AxleViewFactory
        let context:IForestContext = upcast DefaultForestContext(viewFactory, reflectionProvider, securityManager, templateProvider)
        this._context <- context
        this._engine <- new Engine(context)
        this._result <- this._engine.InitialResult

        this |> e.Export<IForestFacade> |> ignore

    interface IForestRendererConfigurer with
        member this.SetRenderer renderer =
            this._renderer <- renderer

    [<ModuleDependencyInitialized>]
    member this.DependencyInitialized(viewProvider : IForestViewProvider) =
        (this:>IViewRegistry) |> viewProvider.RegisterViews

    interface ICommandDispatcher with
        member this.ExecuteCommand target name arg =
            let opWatch = Stopwatch.StartNew()
            this._result <- this._engine.Update(fun e -> e.ExecuteCommand target name arg)
            opWatch.Stop()
            logger.Debug("Forest ExecuteCommand operation took {0}ms", opWatch.ElapsedMilliseconds)

            let renderWatch = Stopwatch.StartNew()
            this._result.Render this._renderer 
            renderWatch.Stop()
            logger.Debug("Forest Render operation took {0}ms", renderWatch.ElapsedMilliseconds)

    interface IMessageDispatcher with
        member this.SendMessage(message : 'M): unit = 
            let sw = Stopwatch.StartNew()
            this._result <- this._engine.Update(fun e -> e.SendMessage message)
            sw.Stop()
            logger.Debug("Forest SendMessage operation took {0}ms", sw.ElapsedMilliseconds)

            let renderWatch = Stopwatch.StartNew()
            this._result.Render this._renderer 
            renderWatch.Stop()
            logger.Debug("Forest Render operation took {0}ms", renderWatch.ElapsedMilliseconds)

    interface IForestFacade with
        member this.LoadTemplate name =
            let sw = Stopwatch.StartNew()
            this._result <- this._engine.LoadTemplate name
            sw.Stop()
            logger.Debug("Forest LoadTemplate operation took {0}ms", sw.ElapsedMilliseconds)

            let renderWatch = Stopwatch.StartNew()
            this._result.Render this._renderer 
            renderWatch.Stop()
            logger.Debug("Forest Render operation took {0}ms", renderWatch.ElapsedMilliseconds)

    interface IViewRegistry with
        member this.GetDescriptor(viewType : Type): IViewDescriptor = this._context.ViewRegistry.GetDescriptor viewType
        member this.GetDescriptor(name : vname): IViewDescriptor = this._context.ViewRegistry.GetDescriptor name
        member this.Register<'t when 't:>IView>():IViewRegistry = 
            typeof<'t>.GetTypeInfo().Assembly |> rtp.RegisterAssemblySource 
            this._context.ViewRegistry.Register<'t>()
        member this.Register(t : Type): IViewRegistry = 
            t.GetTypeInfo().Assembly |> rtp.RegisterAssemblySource 
            this._context.ViewRegistry.Register t
        member this.Resolve(viewType : Type): IView = this._context.ViewRegistry.Resolve viewType
        member this.Resolve(viewType : Type, model : obj): IView = this._context.ViewRegistry.Resolve(viewType, model)
        member this.Resolve(name : vname): IView = this._context.ViewRegistry.Resolve name
        member this.Resolve(name : vname, model : obj): IView = this._context.ViewRegistry.Resolve(name, model)

