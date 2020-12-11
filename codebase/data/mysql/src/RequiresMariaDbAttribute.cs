﻿using System;

using Axle.Modularity;

namespace Axle.Data.MySql
{
    [AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
    public sealed class RequiresMariaDbAttribute : RequiresAttribute
    {
        public RequiresMariaDbAttribute() : base(typeof(MariaDbModule)) { }
    }
}