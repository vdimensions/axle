using Axle.Web.AspNetCore.Sdk;
using Microsoft.AspNetCore.Authorization;

namespace Axle.Web.AspNetCore.Authorization
{
    [RequiresAspNetCoreAuthorization]
    public interface IAuthorizationConfigurer : IAspNetCoreConfigurer<AuthorizationOptions>
    {
    }
}
