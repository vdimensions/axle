#if NETSTANDARD1_5_OR_NEWER || NET35_OR_NEWER
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using Axle.Reflection.Extensions.Type;
using Axle.Verification;

namespace Axle.Reflection.Binding
{
    /// <summary>
    /// An interface representing a binder; that is an object which will modify 
    /// </summary>
    public interface IBinder
    {
        void Bind(object target, IBindingResolver resolver);
    }

    public class Binder : IBinder
    {
        internal void Bind(object target, IBindingResolver resolver, IDictionary<Type, IIntrospector> introspectors)
        {
            var targetType = target.GetType();
            if (!introspectors.TryGetValue(targetType, out var introspector))
            {
                introspectors.Add(targetType, introspector = CreateIntrospector(targetType));
            }
            const ScanOptions scanOpts = ScanOptions.Instance | ScanOptions.Public;
            var props = Enumerable.ToDictionary(
                Enumerable.OfType<IReadWriteMember>(introspector.GetProperties(scanOpts)), 
                x => x.Name, 
                StringComparer.OrdinalIgnoreCase);
            foreach (var field in introspector.GetFields(scanOpts))
            {
                if (!props.ContainsKey(field.Name))
                {
                    props.Add(field.Name, field);
                }
            }

            foreach (var propName in props.Keys)
            {
                var prop = props[propName];
                switch (Type.GetTypeCode(prop.MemberType))
                {
                    case TypeCode.Boolean:
                    case TypeCode.Byte:
                    case TypeCode.Char:
                    case TypeCode.DateTime:
                    case TypeCode.Decimal:
                    case TypeCode.Double:
                    case TypeCode.Int16:
                    case TypeCode.Int32:
                    case TypeCode.Int64:
                    case TypeCode.SByte:
                    case TypeCode.Single:
                    case TypeCode.String:
                    case TypeCode.UInt16:
                    case TypeCode.UInt32:
                    case TypeCode.UInt64:
                        if (resolver.TryResolve(prop.MemberType, prop.Name, out var resolved))
                        {
                            prop.SetAccessor.SetValue(target, resolved);
                        }
                        break;
                    case TypeCode.Object:
                        if (TryCreateInstance(prop.MemberType, out var instance))
                        {
                            Bind(instance, resolver.CreateNestedResolver(prop.Name), introspectors);
                            prop.SetAccessor.SetValue(target, instance);
                        }
                        break;
                    // TODO: handle collections, dictionaries and etc
                    default:
                        break;
                }
            }            
        }

        protected virtual IIntrospector CreateIntrospector(Type type) => new DefaultIntrospector(type.VerifyArgument(nameof(type)).IsNotNull());

        protected virtual bool TryCreateInstance(Type propMemberType, out object instance)
        {
            var introspector = CreateIntrospector(propMemberType);
            var ctor = introspector.GetConstructor(ScanOptions.Public | ScanOptions.Instance);
            if (ctor != null)
            {
                try
                {
                    instance = ctor.Invoke();
                    return instance != null;
                }
                catch
                {
                    instance = null;
                    return false;
                }
            }

            instance = null;
            return false;
        }

        public void Bind(object target, IBindingResolver resolver) => Bind(target, resolver, new Dictionary<Type, IIntrospector>());
    }
}
#endif