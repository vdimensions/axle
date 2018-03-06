//using Axle.ComponentModel;
//using Axle.Security.Authentication.ComponentModel;
//
//
//namespace Axle.Security.Authentication
//{
//    public sealed class Authenticator : ComponentProxy<IAuthenticator>, IAuthenticator
//    {
//        internal Authenticator() : base(string.Empty, new NoOpAuthenticatorDriver()) { }
//
//        public bool IsGuest { get { return Target.IsGuest; } }
//        public IAccount CurrentUser { get { return Target.CurrentUser; } }
//        public IAccount AuthenticatedUser { get { return Target.AuthenticatedUser; } }
//
//        public void DemandCredentials() { Target.DemandCredentials(); }
//        public void SignIn(IAuthenticationToken authToken) { Target.SignIn(authToken); }
//        public void SignOut() { Target.SignOut(); }
//    }
//}