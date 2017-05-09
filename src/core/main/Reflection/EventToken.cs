using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;


namespace Axle.Reflection
{
    //[Maturity(CodeMaturity.Stable)]
    public partial class EventToken : MemberTokenBase<EventInfo>, IEquatable<EventToken>, IEvent
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly AccessModifier accessModifier;
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly DeclarationType declaration;
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly ICombineAccessor combineAccessor;
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly IRemoveAccessor removeAccessor;
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly EventInfo eventInfo;

        public override bool Equals(object obj) { return obj is EventToken && base.Equals(obj); }
        bool IEquatable<EventToken>.Equals(EventToken other) { return base.Equals(other); }

        IAccessor IAccessible.FindAccessor(AccessorType accessorType)
        {
            switch (accessorType)
            {
                case AccessorType.Add: return CombineAccessor;
                case AccessorType.Remove: return RemoveAccessor;
                default: return null;
            }
        }

        public override int GetHashCode() { return unchecked(base.GetHashCode()); }

        public override AccessModifier AccessModifier { get { return accessModifier; } }
        public override DeclarationType Declaration { get { return declaration; } }
        public override Type MemberType { get { return ReflectedMember.EventHandlerType; } }
        public ICombineAccessor CombineAccessor { get { return combineAccessor; } }
        public IRemoveAccessor RemoveAccessor { get { return removeAccessor; } }
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        IEnumerable<IAccessor> IAccessible.Accessors { get { return new IAccessor[] { combineAccessor, removeAccessor }; } }
        public override EventInfo ReflectedMember { get { return eventInfo; } }
    }
}