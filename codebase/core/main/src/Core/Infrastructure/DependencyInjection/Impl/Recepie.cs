//using System.Diagnostics;
//
//using Axle.Reflection;
//
//
//namespace Axle.Core.Infrastructure.DependencyInjection.Impl
//{
//    internal struct Recepie
//    {
//        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
//        private readonly IInvokable _factoryMethod;
//        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
//        private readonly ParameterDependency[] _factoryParameters;
//        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
//        private readonly PropertyDependency[] _properties;
//
//        public Recepie(IInvokable factoryMethod, ParameterDependency[] factoryParameters, PropertyDependency[] properties) : this()
//        {
//            _factoryMethod = factoryMethod;
//            _factoryParameters = factoryParameters;
//            _properties = properties;
//        }
//
//        public object Cook(params object[] args)
//        {
//            if (_factoryMethod is IConstructor constructor)
//            {
//                return constructor.Invoke(args);
//            }
//            return _factoryMethod.Invoke(null, args);
//        }
//
//        public ParameterDependency[] FactoryParameters => _factoryParameters;
//
//        public PropertyDependency[] Properties => _properties;
//    }
//}