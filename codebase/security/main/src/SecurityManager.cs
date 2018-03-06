using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Security.Authentication;

using Axle.References;
using Axle.Security.Authentication;
using Axle.Security.Authorization;


namespace Axle.Security
{
    public sealed class SecurityManager
    {
        /// <summary>
        /// The sole instance of the <see cref="SecurityManager" /> class.
        /// </summary>
        public static SecurityManager Instance => Singleton<SecurityManager>.Instance;

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly IAuthenticator _authenticator;

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly IAuthorizer _authorizer;

        private SecurityManager()
        {
            _authenticator = new Authenticator();
            _authorizer = new Authorizer();
        }

        /// <summary>
        /// Checks whether the current principal is an authenticated user or not. Same as <see cref="IAuthenticator.IsGuest" />
        /// </summary>
        /// <returns>
        /// <c>true</c> if the current user is not an authenticated user; <c>false</c> otherwise.
        /// </returns>
        /// <seealso cref="IAuthenticator.IsGuest"/>
        /// <seealso cref="IAuthenticator"/>
        public bool IsAnonymous => _authenticator.IsGuest;

        /// <summary>
        /// Gets a reference to the current principal.
        /// </summary>
        /// <returns>
        /// Depending on the <see cref="IsAnonymous"/> value, returns either a reference to the guest account, or to the currently
        /// authenticated principal.
        /// </returns>
        /// <seealso cref="IPrincipal" />
        public IPrincipal CurrentPrincipal => _authenticator.CurrentUser;

        /// <summary>
        /// Gets a reference to the currently authenticated principal. 
        /// </summary>
        /// <returns>
        /// A reference to the currently authenticated user.
        /// </returns>
        /// <seealso cref="IPrincipal" />
        public IPrincipal AuthenticatedPrincipal => _authenticator.AuthenticatedUser;

        public bool IsOwnerOf(IAccessLevelEntry accessLevelEntry) => IsOwnerOf(accessLevelEntry.Owner);
        public bool IsOwnerOf(string principal) => !IsAnonymous && CurrentPrincipal.Name.Equals(principal, StringComparison.Ordinal);

        public bool IsRelatedToPrincipal(string principal) => _authorizer.ArePrincipalsRelated(CurrentPrincipal.Name, principal);
        public bool IsRelatedToPrincipal(IPrincipal principal) => _authorizer.ArePrincipalsRelated(CurrentPrincipal, principal);

        public void DemandAccess(IAccessLevelEntry accessLevelEntry, IEnumerable<IAccessRight> accessRights)
        {
            _authorizer.DemandAccess(CurrentPrincipal, accessLevelEntry, accessRights);
        }
        public void DemandAccess(IAccessLevelEntry accessLevelEntry, IEnumerable<string> accessRights)
        {
            _authorizer.DemandAccess(CurrentPrincipal, accessLevelEntry, accessRights);
        }
        public void DemandAccess(string accessLevel, IEnumerable<IAccessRight> accessRights)
        {
            _authorizer.DemandAccess(CurrentPrincipal, accessLevel, accessRights);
        }
        public void DemandAccess(string accessLevel, IEnumerable<string> accessRights)
        {
            _authorizer.DemandAccess(CurrentPrincipal, accessLevel, accessRights);
        }
        public void DemandAccess(IAccessLevelEntry accessLevelEntry, params IAccessRight[] accessRights)
        {
            _authorizer.DemandAccess(CurrentPrincipal, accessLevelEntry, accessRights);
        }
        public void DemandAccess(IAccessLevelEntry accessLevelEntry, params string[] accessRights)
        {
            _authorizer.DemandAccess(CurrentPrincipal, accessLevelEntry, accessRights);
        }
        public void DemandAccess(string accessLevel, params IAccessRight[] accessRights)
        {
            _authorizer.DemandAccess(CurrentPrincipal, accessLevel, accessRights);
        }
        public void DemandAccess(string accessLevel, params string[] accessRights)
        {
            _authorizer.DemandAccess(CurrentPrincipal, accessLevel, accessRights);
        }

        public bool HasAccess(IAccessLevelEntry accessLevelEntry, IEnumerable<IAccessRight> accessRights)
        {
            return _authorizer.HasAccess(CurrentPrincipal, accessLevelEntry, accessRights);
        }
        public bool HasAccess(IAccessLevelEntry accessLevelEntry, IEnumerable<string> accessRights)
        {
            return _authorizer.HasAccess(CurrentPrincipal, accessLevelEntry, accessRights);
        }
        public bool HasAccess(string accessLevel, IEnumerable<IAccessRight> accessRights)
        {
            return _authorizer.HasAccess(CurrentPrincipal, accessLevel, accessRights);
        }
        public bool HasAccess(string accessLevel, IEnumerable<string> accessRights)
        {
            return _authorizer.HasAccess(CurrentPrincipal, accessLevel, accessRights);
        }
        public bool HasAccess(IAccessLevelEntry accessLevelEntry, params IAccessRight[] accessRights)
        {
            return _authorizer.HasAccess(CurrentPrincipal, accessLevelEntry, accessRights);
        }
        public bool HasAccess(IAccessLevelEntry accessLevelEntry, params string[] accessRights)
        {
            return _authorizer.HasAccess(CurrentPrincipal, accessLevelEntry, accessRights);
        }
        public bool HasAccess(string accessLevel, params IAccessRight[] accessRights)
        {
            return _authorizer.HasAccess(CurrentPrincipal, accessLevel, accessRights);
        }
        public bool HasAccess(string accessLevel, params string[] accessRights)
        {
            return _authorizer.HasAccess(CurrentPrincipal, accessLevel, accessRights);
        }

        /// <summary>
        /// Authenticates a user based on the provided authentication token.
        /// </summary>
        /// <param name="authToken">
        /// An authentication token object that represents the authenticating user's credentials.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="authToken"/> is public <see langword="null"/>.
        /// </exception>
        /// <exception cref="AuthenticationException">
        /// Invalid user credentials have been passed.
        /// </exception>
        public void SignIn(IAuthenticationToken authToken) => _authenticator.SignIn(authToken);

        public void SignOut() => _authenticator.SignOut();
    }
}
