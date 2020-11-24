﻿using System.Diagnostics.CodeAnalysis;
using Axle.Modularity;

namespace Axle.Data.MySql
{
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    [Module]
    [DbServiceProvider(Name = MySqlServiceProvider.Name)]
    internal sealed class MySqlModule : DatabaseServiceProviderModule
    {
        public MySqlModule() : base(MySqlServiceProvider.Instance) { }
    }
}