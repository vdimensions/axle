//using System;
//
//using Axle.Application.IoC;
//using Axle.ComponentModel;
//using Axle.Security.Authentication.Sdk;
//using Axle.Security.Authorization;
//
//
//namespace Axle.Security.Authentication.ComponentModel
//{
//    internal sealed class NoOpAuthenticatorDriver : GenericComponentDriver<IAuthenticator>
//    {
//        private sealed class NoOpAuthenticator : AuthenticatorBase
//        {
//            [Serializable]
//            private sealed class Account : IAccount
//            {
//                string IPrincipal.Name { get { return System.Threading.Thread.CurrentPrincipal.Identity.Name; } }
//                IRole IAccount.Role { get { return null; } }
//            }
//
//            public NoOpAuthenticator()
//            {
//                CurrentUser = new Account();
//            }
//
//            protected override IAccount GetAuthenticatedUser() { return null; }
//
//            public override void DemandCredentials() { }
//
//            public override void SignIn(IAuthenticationToken authToken) { }
//
//            public override void SignOut() { }
//
//            public override bool IsGuest { get { return true; } }
//
//            public override IAccount CurrentUser { get; protected set; }
//        }
//
//        protected override IAuthenticator DoResolve(Container container, string name) { return new NoOpAuthenticator(); }
//    }
//}