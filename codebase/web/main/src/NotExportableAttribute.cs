using System;
using Axle.DependencyInjection;
using Axle.Reflection;
using Axle.Verification;

namespace Axle.Web.AspNetCore
{
    /// <summary>
    /// <para>
    /// By default, the axle framework will register all exported instances via the
    /// <see cref="IDependencyExporter.Export"/> method as singleton services in the aspnetcore app.
    /// </para>
    /// <para>
    /// This attribute marks a type as 'not-a-service' thus notifying the framework not to register
    /// the annotated object as a service in the aspnetcore application.
    ///</para>
    /// <para>
    /// Valid examples for using this attribute are Axle's own wrappers around common aspnetcore types,
    /// such as the <see cref="Microsoft.AspNetCore.Http.IHttpContextAccessor"/>
    /// </para>
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public sealed class NotExportableAttribute : Attribute
    {
        /// <summary>
        /// Determines if the <see cref="NotExportableAttribute"/> has been defined for the target
        /// <paramref name="type"/>.
        /// </summary>
        /// <param name="type">
        /// The type to check if has a <see cref="NotExportableAttribute"/> defined on.
        /// </param>
        /// <returns>
        /// <c>true</c> if the <see cref="NotExportableAttribute"/> has been defined for the target
        /// <paramref name="type"/>; <c>false</c> otherwise.
        /// </returns>
        public static bool IsDefinedFor(Type type)
        {
            type.VerifyArgument(nameof(type)).IsNotNull();
            return new TypeIntrospector(type).GetAttributes(typeof(NotExportableAttribute)).Length == 1; 
        }
    }
}