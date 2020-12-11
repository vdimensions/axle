﻿using System.Diagnostics.CodeAnalysis;
using Axle.Verification;

namespace Axle.Application.Services
{
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseDefaultServiceGroup(this IApplicationBuilder app)
        {
            app.VerifyArgument(nameof(app)).IsNotNull();
            return app.ConfigureModules(m => m.Load<ServiceGroup>());
        }
        // public static IApplicationBuilder UseServiceGroup<T>(this IApplicationBuilder app)
        // {
        //     app.VerifyArgument(nameof(app)).IsNotNull();
        //     return app.ConfigureModules(m => m.Load<ServiceGroup<T>>());
        // }
    }
}