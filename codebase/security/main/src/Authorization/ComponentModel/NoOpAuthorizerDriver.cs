//using System.Collections.Generic;
//using System.Diagnostics;
//
//using Axle.Application.IoC;
//using Axle.ComponentModel;
//using Axle.Security.Authorization.Sdk;
//
//
//namespace Axle.Security.Authorization.ComponentModel
//{
//    internal sealed class NoOpAuthorizerDriver : GenericComponentDriver<IAuthorizer>
//    {
//        private sealed class NoOpAuthorier : AbstractAuthorizerizerizerizerizerizer
//        {
//            private sealed class PseudoPrincipal : IPrincipal
//            {
//                [DebuggerBrowsable(DebuggerBrowsableState.Never)]
//                private readonly string name;
//
//                public PseudoPrincipal(string name)
//                {
//                    this.name = name;
//                }
//
//                public string Name { get { return name; } }
//            }
//            
//            private sealed class PseudoAccessRight : IAccessRight
//            {
//                [DebuggerBrowsable(DebuggerBrowsableState.Never)]
//                private readonly string name;
//
//                public PseudoAccessRight(string name)
//                {
//                    this.name = name;
//                }
//
//                public string Name { get { return name; } }
//            }
//
//            private sealed class PseudoAccessPolicy : IAccessPolicy
//            {
//                [DebuggerBrowsable(DebuggerBrowsableState.Never)]
//                private readonly string name;
//
//                public PseudoAccessPolicy(string name)
//                {
//                    this.name = name;
//                }
//
//                public string AccessLevel { get { return name; } }
//
//                public bool BypassElevation { get { return true; } }
//            }
//
//            #region Overrides of AuthorizerBase
//            protected override IEnumerable<IAccessPolicy> GetAccessPolicies(string accessLevel, IEnumerable<IAccessRight> accessRights)
//            {
//                return new IAccessPolicy[1]{new PseudoAccessPolicy(string.Empty)};
//            }
//
//            protected override IEnumerable<IAccessPolicy> GetParentPolicies(IAccessPolicy accessPolicy)
//            {
//                return new IAccessPolicy[0];
//            }
//
//            protected override IEnumerable<IGroup> GetGroups(IGroupMember groupMember)
//            {
//                return new IGroup[0];
//            }
//
//            protected override IPrincipal GetPrincipal(string principal)
//            {
//                return new PseudoPrincipal(principal);
//            }
//
//            protected override IAccessRight GetAccessRight(string value)
//            {
//                return new PseudoAccessRight(value);
//            }
//
//            protected override bool ValidateRules(IEnumerable<IPrincipal> principals, IEnumerable<IAccessRule> rules)
//            {
//                return true;
//            }
//            #endregion
//        }
//
//        protected override IAuthorizer DoResolve(Container container, string name) { return new NoOpAuthorier(); }
//    }
//}