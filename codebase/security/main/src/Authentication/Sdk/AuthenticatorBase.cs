using System;


namespace Axle.Security.Authentication.Sdk
{
    public abstract class AuthenticatorBase : IAuthenticator
    {
        private readonly object syncRoot = new object();

        public abstract bool IsGuest { get; }

        public abstract IAccount CurrentUser { get; protected set; }

        public IAccount AuthenticatedUser
        {
            get
            {
                lock (syncRoot)
                {
                    if (IsGuest)
                    {
                        DemandCredentials();
                        if (IsGuest)
                        {
                            throw new InvalidOperationException("The account is currently unavailable. Authentication might currently be in progress.");
                        }
                        throw new NotSupportedException("The current session belongs to a guest user and there is no available mechanism for forcing authentication.");
                    }
                }
                return GetAuthenticatedUser();
            }
        }

        protected AuthenticatorBase() { }

        protected abstract IAccount GetAuthenticatedUser();

        public abstract void DemandCredentials();

        public abstract void SignIn(IAuthenticationToken authToken);

        public abstract void SignOut();
    }
}
