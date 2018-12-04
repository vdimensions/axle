using System;
using System.Collections.Concurrent;
using Microsoft.AspNetCore.Http;
using Axle.Logging;
using Axle.Forest;
using Axle.Verification;
using Axle.Modularity;
using Axle.Web.AspNetCore.Session;
using Forest;
using Forest.UI;
using Forest.Web.WebSharper;
using Microsoft.FSharp.Core;
using WebSharper.UI;


namespace Axle.Web.WebSharper.Forest
{
    [Module]
    [RequiresForest]
    [RequiresWebSharper]
    [RequiresAspNetSession]
    internal sealed class WebSharperForestModule : ISessionEventListener, IForestFacadeProvider, IDocumentRenderer, IWebSharperTemplateRegistry
    {
        private readonly IForestContext _forestContext;
        private readonly ILogger _logger;
        private readonly PerSessionWebSharperForestFacade _perSessionForesFacade;

        private readonly ConcurrentDictionary<string, FSharpFunc<Tuple<ICommandDispatcher, DomNode>, WebSharperPhysicalView>> _registeredPhysicalViewFacories =
            new ConcurrentDictionary<string, FSharpFunc<Tuple<ICommandDispatcher, DomNode>, WebSharperPhysicalView>>(StringComparer.Ordinal);

        public WebSharperForestModule(
            IForestContext forestContext,
            IHttpContextAccessor httpContextAccessor,
            Axle.Logging.ILogger logger)
        {
            _forestContext = forestContext;
            _perSessionForesFacade = new PerSessionWebSharperForestFacade(httpContextAccessor);
            _logger = logger;
        }

        [ModuleTerminate]
        internal void Terminated()
        {
            (_perSessionForesFacade as IDisposable).Dispose();
            _registeredPhysicalViewFacories.Clear();
        }

        [ModuleDependencyInitialized]
        internal void DependencyInitialized(IWebSharperTemplateConfigurer c)
        {
            c.Configure(this);
        }

        void ISessionEventListener.OnSessionStart(ISession session)
        {
            var sessionId = session.Id;
            _perSessionForesFacade.AddOrReplace(
                sessionId,
                new WebSharperForestFacade(_forestContext, new WebSharperPhysicalViewRenderer(this)),
                (_, __) => new WebSharperForestFacade(_forestContext, new WebSharperPhysicalViewRenderer(this)));
            _logger.Trace("WebSharper forest facade for session {0} created", sessionId);
        }


        void ISessionEventListener.OnSessionEnd(string sessionId)
        {
            if (_perSessionForesFacade.TryRemove(sessionId, out _))
            {
                _logger.Trace("WebSharper forest facade for session {0} deleted", sessionId);
            }
        }

        IForestFacade IForestFacadeProvider.ForestFacade => _perSessionForesFacade.Current;

        Doc IDocumentRenderer.Doc() => ((IDocumentRenderer) _perSessionForesFacade.Current.Renderer).Doc();

        IWebSharperTemplateRegistry IWebSharperTemplateRegistry.Register(string name, FSharpFunc<Tuple<ICommandDispatcher, DomNode>, WebSharperPhysicalView> factory)
        {
            _registeredPhysicalViewFacories.AddOrUpdate(
                name.VerifyArgument(nameof(name)).IsNotNullOrEmpty(),
                factory.VerifyArgument(nameof(factory)).IsNotNull(),
                (n, _) => throw new InvalidOperationException($"A physical view with the provided name '{n}' is already registered"));
            return this;
        }


        WebSharperPhysicalView IWebSharperTemplateRegistry.Get(ICommandDispatcher commandDispatcher, DomNode domNode)
        {
            commandDispatcher.VerifyArgument(nameof(commandDispatcher)).IsNotNull();
            var name = domNode.VerifyArgument(nameof(domNode)).IsNotNull().Value.Name;
            if (_registeredPhysicalViewFacories.TryGetValue(name, out var value))
            {
                return value.Invoke(new Tuple<ICommandDispatcher, DomNode>(commandDispatcher, domNode));
            }
            throw new InvalidOperationException(string.Format("A physical view with the provided name '{0}' could not be found", name));
        }
    }
}