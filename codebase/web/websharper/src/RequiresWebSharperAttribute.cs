﻿using System;
using System.Diagnostics.CodeAnalysis;

using Axle.Modularity;


namespace Axle.Web.WebSharper
{
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface, Inherited = true, AllowMultiple = false)]
    public sealed class RequiresWebSharperAttribute : RequiresAttribute
    {
        public RequiresWebSharperAttribute() : base(typeof(WebSharperModule)) { }
    }
}