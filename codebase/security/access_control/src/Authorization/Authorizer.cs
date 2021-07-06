//using System.Collections.Generic;
//
//using Axle.ComponentModel;
//using Axle.Security.Authorization.ComponentModel;
//
//
//namespace Axle.Security.Authorization
//{
//    internal sealed class Authorizer : ComponentProxy<IAuthorizer>, IAuthorizer
//    {
//        internal Authorizer() : base(string.Empty, new NoOpAuthorizerDriver()) { }
//
//        public bool ArePrincipalsRelated(IPrincipal principal1, IPrincipal principal2)
//        {
//            return Target.ArePrincipalsRelated(principal1, principal2);
//        }
//        public bool ArePrincipalsRelated(string principal1, string principal2)
//        {
//            return Target.ArePrincipalsRelated(principal1, principal2);
//        }
//
//        public void DemandAccess(IPrincipal principal, IAccessLevelEntry accessLevelEntry, IEnumerable<IAccessRight> accessRights)
//        {
//            Target.DemandAccess(principal, accessLevelEntry, accessRights);
//        }
//        public void DemandAccess(IPrincipal principal, IAccessLevelEntry accessLevelEntry, params IAccessRight[] accessRights)
//        {
//            Target.DemandAccess(principal, accessLevelEntry, accessRights);
//        }
//
//        public void DemandAccess(IPrincipal principal, string accessLevel, IEnumerable<IAccessRight> accessRights)
//        {
//            Target.DemandAccess(principal, accessLevel, accessRights);
//        }
//        public void DemandAccess(IPrincipal principal, string accessLevel, params IAccessRight[] accessRights)
//        {
//            Target.DemandAccess(principal, accessLevel, accessRights);
//        }
//
//        public void DemandAccess(IPrincipal principal, IAccessLevelEntry accessLevelEntry, IEnumerable<string> accessRights)
//        {
//            Target.DemandAccess(principal, accessLevelEntry, accessRights);
//        }
//        public void DemandAccess(IPrincipal principal, IAccessLevelEntry accessLevelEntry, params string[] accessRights)
//        {
//            Target.DemandAccess(principal, accessLevelEntry, accessRights);
//        }
//
//        public void DemandAccess(IPrincipal principal, string accessLevel, IEnumerable<string> accessRights)
//        {
//            Target.DemandAccess(principal, accessLevel, accessRights);
//        }
//        public void DemandAccess(IPrincipal principal, string accessLevel, params string[] accessRights)
//        {
//            Target.DemandAccess(principal, accessLevel, accessRights);
//        }
//
//        public bool HasAccess(IPrincipal principal, IAccessLevelEntry accessLevelEntry, IEnumerable<IAccessRight> accessRights)
//        {
//            return Target.HasAccess(principal, accessLevelEntry, accessRights);
//        }
//        public bool HasAccess(IPrincipal principal, IAccessLevelEntry accessLevelEntry, params IAccessRight[] accessRights)
//        {
//            return Target.HasAccess(principal, accessLevelEntry, accessRights);
//        }
//
//        public bool HasAccess(IPrincipal principal, string accessLevel, IEnumerable<IAccessRight> accessRights)
//        {
//            return Target.HasAccess(principal, accessLevel, accessRights);
//        }
//        public bool HasAccess(IPrincipal principal, string accessLevel, params IAccessRight[] accessRights)
//        {
//            return Target.HasAccess(principal, accessLevel, accessRights);
//        }
//
//        public bool HasAccess(IPrincipal principal, IAccessLevelEntry accessLevelEntry, IEnumerable<string> accessRights)
//        {
//            return Target.HasAccess(principal, accessLevelEntry, accessRights);
//        }
//        public bool HasAccess(IPrincipal principal, IAccessLevelEntry accessLevelEntry, params string[] accessRights)
//        {
//            return Target.HasAccess(principal, accessLevelEntry, accessRights);
//        }
//
//        public bool HasAccess(IPrincipal principal, string accessLevel, IEnumerable<string> accessRights)
//        {
//            return Target.HasAccess(principal, accessLevel, accessRights);
//        }
//        public bool HasAccess(IPrincipal principal, string accessLevel, params string[] accessRights)
//        {
//            return Target.HasAccess(principal, accessLevel, accessRights);
//        }
//    }
//}
