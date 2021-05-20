using System;

namespace Axle.Security.AccessControl.Authentication
{
    public abstract class AbstractAuthenticator : IAuthenticator
    {
        private readonly object _syncRoot = new object();

        protected AbstractAuthenticator() { }

        protected abstract IAccount GetAuthenticatedUser();

        public abstract void DemandCredentials();

        public abstract void SignIn(IAuthenticationToken authToken);

        public abstract void SignOut();

        public abstract bool IsGuest { get; }

        public abstract IAccount CurrentAccount { get; protected set; }

        public IAccount AuthenticatedAccount
        {
            get
            {
                lock (_syncRoot)
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
    }

    public abstract class AbstractAuthenticator<TToken> 
        : AbstractAuthenticator,
          IAuthenticationTokenProcessor<TToken>
        where TToken: IAuthenticationToken
    {
        public virtual bool CanProcessToken(TToken token) => true;

        public bool CanProcessToken(IAuthenticationToken token)
        {
            if (token is TToken supportedToken)
            {
                CanProcessToken(supportedToken);
            }
            throw new NotSupportedException("The provided auth token is not supported.");
        }

        public abstract void SignIn(TToken authToken);

        public sealed override void SignIn(IAuthenticationToken authToken)
        {
            if (authToken is TToken supportedToken && CanProcessToken(supportedToken))
            {
                SignIn(supportedToken);
            }
            throw new NotSupportedException("The provided auth token is not supported.");
        }
    }
}
