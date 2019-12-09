using Axle.Configuration;
using Axle.Modularity;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Diagnostics.CodeAnalysis;

namespace Axle.Web.AspNetCore.StaticFiles
{
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface, Inherited = true, AllowMultiple = false)]
    public sealed class UtilizesStaticFilesAttribute : UtilizesAttribute
    {
        public UtilizesStaticFilesAttribute() : base(typeof(StaticFilesModule)) { }
    }

    [Module]
    public sealed class StaticFilesModule : IApplicationConfigurer
    {
        public StaticFilesModule(IConfiguration configuration)
        {

        }

        void IApplicationConfigurer.Configure(
            Microsoft.AspNetCore.Builder.IApplicationBuilder app, 
            IHostingEnvironment env)
        {
            app.UseStaticFiles();
        }
    }
}