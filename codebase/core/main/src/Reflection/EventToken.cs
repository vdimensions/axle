using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;


namespace Axle.Reflection
{
    #if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
    [Serializable]
    #endif
    public class EventToken : MemberTokenBase<EventInfo>, IEquatable<EventToken>, IEvent
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly AccessModifier _accessModifier;
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly DeclarationType _declaration;
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly ICombineAccessor _combineAccessor;
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly IRemoveAccessor _removeAccessor;
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly EventInfo _eventInfo;

        public EventToken(EventInfo info) : base(info, info.DeclaringType, info.Name)
        {
            _eventInfo = info;
            #if NETSTANDARD
            var am = info.AddMethod;
            var rm = info.RemoveMethod;
            #else
            var am = info.GetAddMethod(true);
            var rm = info.GetRemoveMethod(true);
            #endif
            _combineAccessor = am == null ? null : new EventAddAccessor(this, new MethodToken(am));
            _removeAccessor = rm == null ? null : new EventRemoveAccessor(this, new MethodToken(rm));

            _declaration = ReflectionExtensions.GetDeclarationTypeUnchecked(am, rm);
            var m = new[] { am, rm };
            var isPublic = Enumerable.All(m, x => (x == null) || x.IsPublic);
            var isAssembly = !isPublic && Enumerable.All(m, x => (x == null) || x.IsAssembly);
            var isFamily = !isPublic && Enumerable.All(m, x => (x == null) || x.IsFamily);
            var isPrivate = !(isPublic || isFamily || isAssembly) && m.All(x => (x == null) || x.IsPrivate);
            _accessModifier = AccessModifierExtensions.GetAccessModifier(isPublic, isAssembly, isFamily, isPrivate);
        }

        public override bool Equals(object obj) => obj is EventToken && base.Equals(obj);
        bool IEquatable<EventToken>.Equals(EventToken other) => base.Equals(other);

        IAccessor IAccessible.FindAccessor(AccessorType accessorType)
        {
            switch (accessorType)
            {
                case AccessorType.Add: return CombineAccessor;
                case AccessorType.Remove: return RemoveAccessor;
                default: return null;
            }
        }

        /// <inheritdoc />
        public override int GetHashCode() => base.GetHashCode();

        public override AccessModifier AccessModifier => _accessModifier;
        public override DeclarationType Declaration => _declaration;
        public override Type MemberType => ReflectedMember.EventHandlerType;
        public ICombineAccessor CombineAccessor => _combineAccessor;
        public IRemoveAccessor RemoveAccessor => _removeAccessor;
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        IEnumerable<IAccessor> IAccessible.Accessors => new IAccessor[] { _combineAccessor, _removeAccessor };
        public override EventInfo ReflectedMember => _eventInfo;
    }
}