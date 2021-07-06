using System.Collections.Generic;

namespace Axle.Security.AccessControl.Authorization
{
    public interface IAuthorizer
    {
        /// <summary>
        /// Determines if there is hierarchical relationship between the selected principals. 
        /// </summary>
        /// <param name="principal1"></param>
        /// <param name="principal2"></param>
        /// <returns>
        /// <c>true</c> if any of the following conditions is met:
        /// <list type="bullet">
        ///   <item>
        ///     <description>one of the principals is a group and the other is a member of that group</description>
        ///   </item>
        ///   <item>
        ///     <description>one of the principals is a user, and the other is a role the user is in</description>
        ///   </item>
        ///   <item>
        ///     <description>the principals are the same</description>
        ///   </item>
        /// </list>
        /// otherwise returns <c>false</c>
        /// </returns>
        bool ArePrincipalsRelated(IPrincipal principal1, IPrincipal principal2);
        bool ArePrincipalsRelated(string principal1, string principal2);

        void DemandAccess(IPrincipal principal, IAccessLevelEntry accessLevelEntry, IEnumerable<IAccessRight> accessRights);
        void DemandAccess(IPrincipal principal, string accessLevel, IEnumerable<IAccessRight> accessRights);
        void DemandAccess(IPrincipal principal, IAccessLevelEntry accessLevelEntry, IEnumerable<string> accessRights);
        void DemandAccess(IPrincipal principal, string accessLevel, IEnumerable<string> accessRights);

        bool HasAccess(IPrincipal principal, IAccessLevelEntry accessLevelEntry, IEnumerable<IAccessRight> accessRights);
        bool HasAccess(IPrincipal principal, string accessLevel, IEnumerable<IAccessRight> accessRights);
        bool HasAccess(IPrincipal principal, IAccessLevelEntry accessLevelEntry, IEnumerable<string> accessRights);
        bool HasAccess(IPrincipal principal, string accessLevel, IEnumerable<string> accessRights);
    }
}