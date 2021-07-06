using System;
using System.Security.Authentication;

namespace Axle.Security.AccessControl.Authentication
{
    /// <summary>
    /// An interface used as an abstraction to the authentication process.
    /// </summary>
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

        /// <summary>
        /// Signs out the currently authenticated principal.
        /// </summary>
        void SignOut();

        /// <summary>
        /// A boolean value determining whether the current principal is a guest to the target system, or an
        /// authenticated account.
        /// </summary>
        bool IsGuest { get; }
        
        /// <summary>
        /// Gets a reference to the current account. This would represent a guest account in case the current user dud
        /// not authenticate yet.  
        /// </summary>
        IAccount CurrentAccount { get; }
        
        /// <summary>
        /// Gets a reference to the currently authenticated account. Unlike the <see cref="CurrentAccount"/> property,
        /// this one will throw an <see cref="SecurityException"/> in case the current account is a guest
        /// account. 
        /// </summary>
        /// <remarks>
        /// For security-critical operations, it is a good safety measure to prefer this property over
        /// <see cref="CurrentAccount"/>. The effect would be that the calling site will fail to proceed with the
        /// intended operation unless the thrown <see cref="SecurityException">security exception</see> is purposely
        /// caught and suppressed (which would be a bad idea in general).
        /// </remarks>
        /// <exception cref="SecurityException">
        /// Thrown if there is no authenticated user in the system.
        /// </exception>
        IAccount AuthenticatedAccount { get; }
    }
}