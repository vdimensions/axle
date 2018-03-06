using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Security.Authentication;

using Axle.References;
using Axle.Security.Authentication;
using Axle.Security.Authorization;


namespace Axle.Security
{
    //[Maturity(CodeMaturity.Stable)]
    public sealed class SecurityManager
    {
        /// <summary>
        /// The sole instance of the <see cref="SecurityManager" /> class.
        /// </summary>
        public static SecurityManager Instance { get { return Singleton<SecurityManager>.Instance; } }

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly IAuthenticator authenticator;

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly IAuthorizer authorizer;

        private SecurityManager()
        {
            authenticator = new Authenticator();
            authorizer = new Authorizer();
        }

        /// <summary>
        /// Checks whether the current principal is an authenticated user or not. Same as <see cref="IAuthenticator.IsGuest" />
        /// </summary>
        /// <returns>
        /// <c>true</c> if the current user is not an authenticated user; <c>false</c> otherwise.
        /// </returns>
        /// <seealso cref="IAuthenticator.IsGuest"/>
        /// <seealso cref="IAuthenticator"/>
        public bool IsAnonymous { get { return authenticator.IsGuest; } }
        /// <summary>
        /// Gets a reference to the current principal.
        /// </summary>
        /// <returns>
        /// Depending on the <see cref="IsAnonymous"/> value, returns either a reference to the guest account, or to the currently
        /// authenticated principal.
        /// </returns>
        /// <seealso cref="IPrincipal" />
        public IPrincipal CurrentPrincipal { get { return authenticator.CurrentUser; } }
        /// <summary>
        /// Gets a reference to the currently authenticated principal. 
        /// </summary>
        /// <returns>
        /// A reference to the currently authenticated user.
        /// </returns>
        /// <seealso cref="IPrincipal" />
        public IPrincipal AuthenticatedPrincipal { get { return authenticator.AuthenticatedUser; } }

        public bool IsOwnerOf(IAccessLevelEntry accessLevelEntry) { return IsOwnerOf(accessLevelEntry.Owner); }
        public bool IsOwnerOf(string principal) { return !IsAnonymous && CurrentPrincipal.Name.Equals(principal, StringComparison.Ordinal); }

        public bool IsRelatedToPrincipal(string principal) { return authorizer.ArePrincipalsRelated(CurrentPrincipal.Name, principal); }
        public bool IsRelatedToPrincipal(IPrincipal principal) { return authorizer.ArePrincipalsRelated(CurrentPrincipal, principal); }

        public void DemandAccess(IAccessLevelEntry accessLevelEntry, IEnumerable<IAccessRight> accessRights)
        {
            authorizer.DemandAccess(CurrentPrincipal, accessLevelEntry, accessRights);
        }
        public void DemandAccess(IAccessLevelEntry accessLevelEntry, IEnumerable<string> accessRights)
        {
            authorizer.DemandAccess(CurrentPrincipal, accessLevelEntry, accessRights);
        }
        public void DemandAccess(string accessLevel, IEnumerable<IAccessRight> accessRights)
        {
            authorizer.DemandAccess(CurrentPrincipal, accessLevel, accessRights);
        }
        public void DemandAccess(string accessLevel, IEnumerable<string> accessRights)
        {
            authorizer.DemandAccess(CurrentPrincipal, accessLevel, accessRights);
        }
        public void DemandAccess(IAccessLevelEntry accessLevelEntry, params IAccessRight[] accessRights)
        {
            authorizer.DemandAccess(CurrentPrincipal, accessLevelEntry, accessRights);
        }
        public void DemandAccess(IAccessLevelEntry accessLevelEntry, params string[] accessRights)
        {
            authorizer.DemandAccess(CurrentPrincipal, accessLevelEntry, accessRights);
        }
        public void DemandAccess(string accessLevel, params IAccessRight[] accessRights)
        {
            authorizer.DemandAccess(CurrentPrincipal, accessLevel, accessRights);
        }
        public void DemandAccess(string accessLevel, params string[] accessRights)
        {
            authorizer.DemandAccess(CurrentPrincipal, accessLevel, accessRights);
        }

        public bool HasAccess(IAccessLevelEntry accessLevelEntry, IEnumerable<IAccessRight> accessRights)
        {
            return authorizer.HasAccess(CurrentPrincipal, accessLevelEntry, accessRights);
        }
        public bool HasAccess(IAccessLevelEntry accessLevelEntry, IEnumerable<string> accessRights)
        {
            return authorizer.HasAccess(CurrentPrincipal, accessLevelEntry, accessRights);
        }
        public bool HasAccess(string accessLevel, IEnumerable<IAccessRight> accessRights)
        {
            return authorizer.HasAccess(CurrentPrincipal, accessLevel, accessRights);
        }
        public bool HasAccess(string accessLevel, IEnumerable<string> accessRights)
        {
            return authorizer.HasAccess(CurrentPrincipal, accessLevel, accessRights);
        }
        public bool HasAccess(IAccessLevelEntry accessLevelEntry, params IAccessRight[] accessRights)
        {
            return authorizer.HasAccess(CurrentPrincipal, accessLevelEntry, accessRights);
        }
        public bool HasAccess(IAccessLevelEntry accessLevelEntry, params string[] accessRights)
        {
            return authorizer.HasAccess(CurrentPrincipal, accessLevelEntry, accessRights);
        }
        public bool HasAccess(string accessLevel, params IAccessRight[] accessRights)
        {
            return authorizer.HasAccess(CurrentPrincipal, accessLevel, accessRights);
        }
        public bool HasAccess(string accessLevel, params string[] accessRights)
        {
            return authorizer.HasAccess(CurrentPrincipal, accessLevel, accessRights);
        }

        /// <summary>
        /// Authenticates a user based on the provided authentication token.
        /// </summary>
        /// <param name="authToken">
        /// An authentication token object that represents the authenticating user's credentials.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="authToken"/> is <c>null</c>.
        /// </exception>
        /// <exception cref="AuthenticationException">
        /// Invalid user credentials have been passed.
        /// </exception>
        public void SignIn(IAuthenticationToken authToken) { authenticator.SignIn(authToken); }

        public void SignOut() { authenticator.SignOut(); }
    }
}
