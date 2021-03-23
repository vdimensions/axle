using Axle.Web.AspNetCore.Sdk;
using Microsoft.AspNetCore.Authentication;

namespace Axle.Web.AspNetCore.Authentication
{
    [RequiresAspNetCoreAuthentication]
    public interface IAuthenticationConfigurer : IAspNetCoreConfigurer<AuthenticationOptions>
    {
    }
}
