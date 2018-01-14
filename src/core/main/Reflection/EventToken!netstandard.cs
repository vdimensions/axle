﻿using System;
using System.Linq;
using System.Reflection;


namespace Axle.Reflection
{
    [Serializable]
    partial class EventToken
    {
        public EventToken(EventInfo info) : base(info.DeclaringType, info.Name)
        {
            eventInfo = info;
            var am = info.GetAddMethod(true);
            var rm = info.GetRemoveMethod(true);
            combineAccessor = am == null ? null : new EventAddAccessor(this, new MethodToken(am));
            removeAccessor = rm == null ? null : new EventRemoveAccessor(this, new MethodToken(rm));

            declaration = ReflectionExtensions.GetDeclarationTypeUnchecked(am, rm);
            var m = new[] {am, rm};
            var isPublic = m.All(x => (x == null) || x.IsPublic);
            var isAssembly = !isPublic && m.All(x => (x == null) || x.IsAssembly);
            var isFamily = !isPublic && m.All(x => (x == null) || x.IsFamily);
            var isPrivate = !(isPublic || isFamily || isAssembly) && m.All(x => (x == null) || x.IsPrivate);
            accessModifier = GetAccessModifier(isPublic, isAssembly, isFamily, isPrivate);
        }
    }
}