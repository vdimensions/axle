using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

//using Axle.Application.Aop;
using Axle.Core.Infrastructure.DependencyInjection;
using Axle.Core.Infrastructure.DependencyInjection.Sdk;
using Axle.Core.Infrastructure.DependencyInjection.Descriptors;
using Axle.Core.Infrastructure.DependencyInjection.Impl;
using Axle.Linq;
using Axle.Reflection;


namespace Axle.Core.Infrastructure.DependencyInjection.Impl
{
    [Obsolete]
    internal class Resolvable //: IResolvable
    {       
//        private const BindingFlags Flags = BindingFlags.Public | BindingFlags.NonPublic;

//        private static IEnumerable<IInvokable> GetConstructors(Type type)
//        {
//            #if NETSTANDARD || NET45_OR_NEWER
//            #else
//            #endif
//            var allConstructors = type.GetConstructors(BindingFlags.Instance | Flags | BindingFlags.CreateInstance);
//            var constructors = allConstructors.Select(ci => new ConstructorToken(ci)).Cast<IInvokable>().ToList();
//            var empty = allConstructors.SingleOrDefault(ct => ct.IsPublic && ct.GetParameters().Length == 0);
//            if ((constructors.Count == 0) && (empty != null))
//            {
//                return new[] { new ConstructorToken(empty) };
//            }
//            if (empty != null)
//            {
//                constructors.Add(new ConstructorToken(empty));
//            }
//            // NOTE: constructors must retrieved with the ones having the most parameters first.
//            var c = constructors.Count > 1
//                ? constructors.OrderByDescending(c1 => c1.GetParameters().Length).ToList()
//                : constructors;
//            return c;
//        }
//        private static IEnumerable<IInvokable> GetFactoryMethods(Type type)
//        {
//            #if NETSTANDARD || NET45_OR_NEWER
//            var allConstructors = type.GetTypeInfo().GetMethods(BindingFlags.Static | Flags | BindingFlags.InvokeMethod);
//            #else
//            var allConstructors = type.GetMethods(BindingFlags.Static | Flags | BindingFlags.InvokeMethod);
//            #endif
//            var methods = allConstructors
//                .Where(mi => (mi.GetCustomAttributes(typeof(InjectAttribute), false).Length > 0) && type.IsAssignableFrom(mi.ReturnType))
//                .Select(mi => new MethodToken(mi)).Cast<IInvokable>()
//                .ToList();
//            var empty = allConstructors.SingleOrDefault(ct => ct.IsPublic && (ct.GetParameters().Length == 0));
//            if ((methods.Count == 0) && (empty != null))
//            {
//                return new[] { new MethodToken(empty) };
//            }
//            if (empty != null)
//            {
//                methods.Add(new MethodToken(empty));
//            }
//            // NOTE: constructors must retrieved with the ones having the most parameters first.
//            var c = methods.Count > 1
//                ? methods.OrderByDescending(c1 => c1.GetParameters().Length).ToList()
//                : methods;
//            return c;
//        }
//        private static IEnumerable<PropertyDependency> GetProperties(Type type)
//        {
//            #if NETSTANDARD || NET45_OR_NEWER
//            #else
//            #endif
//            return type.GetProperties(BindingFlags.Instance | Flags)
//                .Select(
//                    p => new
//                    {
//                        Property = new PropertyToken(p),
//                        Name = p.GetCustomAttributes(true).OfType<InjectAttribute>().Select(x => x.Name).SingleOrDefault(),
//                        IsOptional = p.GetCustomAttributes(true).OfType<OptionalAttribute>().Select(x => new bool?(x.Value)).SingleOrDefault().GetValueOrDefault(false)
//                    })
//                .Where(pa => pa.Name != null)
//                .Where(pa => pa.Property.SetAccessor != null)
//                .Select(x => new PropertyDependency(x.Property, x.Name, x.IsOptional))
//                .ToList();
//        }
//        private static IEnumerable<IInvokable> GetInitMethods(Type type)
//        {
//            #if NETSTANDARD || NET45_OR_NEWER
//            #else
//            #endif
//            var initMethods = type.GetMethods(BindingFlags.Instance | Flags | BindingFlags.InvokeMethod);
//            var methods = initMethods
//                .Where(mi => (mi.GetCustomAttributes(typeof(InjectAttribute), false).Length > 0) && (mi.ReturnType == typeof(void)))
//                .Select(mi => new MethodToken(mi)).Cast<IInvokable>()
//                .ToList();
//            // NOTE: constructors must retrieved with the ones having the most parameters first.
//            var c = methods.Count > 1
//                ? methods.OrderByDescending(c1 => c1.GetParameters().Length).ToList()
//                : methods;
//            return c;
//        }
//        private static ParameterDependency[] GetParameters(IInvokable x)
//        {
//            return x.GetParameters()
//                .Select(y => new ParameterDependency(y, y.Attributes.Select(z => z.Attribute).OfType<InjectAttribute>().Select(z => z.Name).SingleOrDefault() ?? string.Empty))
//                .ToArray();
//        }
//
//        private readonly object _syncRoot = new object();
//        private readonly Type _type;
//        private readonly bool _isSingleton;
////        private readonly IProxyManager proxyManager;
//        private IEnumerable<Recepie> _recepies;
//        private object _value;
//        private bool _isResolved;
//        private IDependencyDescriptorProvider _dependenyDescriptorProvider;
////
////        public Resolvable(Type type, IProxyManager proxyManager, bool isSingleton)
////        {
////            this.type = type;
////            this.proxyManager = proxyManager;
////            this.isSingleton = isSingleton;
////        }
////        public Resolvable(Type type, IProxyManager proxyManager)
////        {
////            this.type = type;
////            this.proxyManager = proxyManager;
////            this.isSingleton = false;
////        }
////
//        public void MarkInjectable(object instance)
//        {
//            lock (_syncRoot)
//            {
//                if (IsInjectable)
//                {
//                    throw new InvalidOperationException("Instance already marked as injectable");
//                }
//                //this.value = proxyManager.GenerateProxy(instance);
//                _value = instance;
//            }
//        }
//        public void MarkResolved()
//        {
//            if (_isResolved)
//            {
//                return;
//            }
//            lock (_syncRoot)
//            {
//                _isResolved = true;
//            }
//        }
//
//        public object Value
//        {
//            get
//            {
//                if (!IsInjectable)
//                {
//                    throw new InvalidOperationException("There is no value to be injected yet.");
//                }
//                return _value;
//            }
//        }
//        public Type Type => _type;
//        public bool IsInjectable => _value != null;
//        public bool IsResolved => IsInjectable && _isResolved;
//        public IEnumerable<Recepie> Recepies
//        {
//            get
//            {
//                if (_recepies == null)
//                {
//                    lock (_syncRoot)
//                    {
//                        if (_recepies == null)
//                        {
//                            var recepies = ConstructionRecepie.ForType(_type, _dependenyDescriptorProvider);
//                            _recepies = recepies.Select(
//                                x =>
//                                {
//                                    var propsList = x.Properties
//                                        .Union(x.Fields)
//                                        .Select(p => new PropertyDependency())
//                                        .ToList();
//                                    var parameters = GetParameters(x);
//                                    parameters.AsSequence().ForEach(
//                                        y => propsList.RemoveAll(
//                                            p => p.RequestedType == y.RequestedType
//                                                 && y.DeclaredName.Length > 0
//                                                 && (comparer.Equals(p.DeclaredName, y.DeclaredName) || comparer.Equals(p.InferredName, y.DeclaredName))));
//                                    return new Recepie(x, parameters, propsList.ToArray());
//                                })
//
//                            //var comparer = StringComparer.Ordinal;
//                            //var properties = GetProperties(_type);
//                            //var factoryMethods = GetConstructors(_type).Union(GetFactoryMethods(_type));
//                            //_recepies = factoryMethods
//                            //    .Select(
//                            //        x =>
//                            //        {
//                            //            var propsList = properties.ToList();
//                            //            var parameters = GetParameters(x);
//                            //            parameters.AsSequence().ForEach(
//                            //                y => propsList.RemoveAll(
//                            //                    p => p.RequestedType == y.RequestedType 
//                            //                        && y.DeclaredName.Length > 0
//                            //                        && (comparer.Equals(p.DeclaredName, y.DeclaredName) || comparer.Equals(p.InferredName, y.DeclaredName))));
//                            //            return new Recepie(x, parameters, propsList.ToArray());
//                            //        })
//                            //    .ToArray();
//                        }
//                    }
//                }
//                return _recepies;
//            }
//        }
//        public bool IsSingleton => _isSingleton;
    }
}