using System;
using System.Security.Authentication;

namespace Axle.Security.AccessControl.Authentication
{
    public interface IAuthenticator
    {
        [Obsolete("Move to another class that supports authentication interception.")]
        void DemandCredentials();

        /// <summary>
        /// Authenticates a user based on the provided authentication token.
        /// </summary>
        /// <param name="authToken">
        /// An authentication token object that represents the authenticating user's credentials.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="authToken"/> is <see langword="null"/>.
        /// </exception>
        /// <exception cref="AuthenticationException">
        /// Invalid user credentials have been passed.
        /// </exception>
        void SignIn(IAuthenticationToken authToken);

        void SignOut();

        bool IsGuest { get; }
        IAccount CurrentAccount { get; }
        IAccount AuthenticatedAccount { get; }
    }
}