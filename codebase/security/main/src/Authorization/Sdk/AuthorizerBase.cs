using System;
using System.Collections.Generic;
using System.Linq;

using Axle.Extensions.EqualityComparer;
using Axle.Extensions.String;
using Axle.Security.Authentication;


namespace Axle.Security.Authorization.Sdk
{
    //[Maturity(CodeMaturity.Stable)]
    public abstract class AuthorizerBase : IAuthorizer
    {
        protected abstract IEnumerable<IAccessPolicy> GetAccessPolicies(string accessLevel, IEnumerable<IAccessRight> accessRights);
        protected abstract IEnumerable<IAccessPolicy> GetParentPolicies(IAccessPolicy accessPolicy);
        protected abstract IEnumerable<IGroup> GetGroups(IGroupMember groupMember);
        protected abstract IPrincipal GetPrincipal(string principal);
        [Obsolete]
        protected abstract IAccessRight GetAccessRight(string value);

        private void ExpandPrincipals(string principal, ICollection<IPrincipal> expandedPrincipals)
        {
            var p = GetPrincipal(principal);
            var groupMember = p as IGroupMember;
            foreach (var group in GetGroups(groupMember))
            {
                expandedPrincipals.Add(group);
            }  
            var account = p as IAccount;
            if (account != null && account.Role != null)
            {
                ExpandPrincipals(account.Role.Name, expandedPrincipals);
            }
        
            expandedPrincipals.Add(p);
        }
        private IEnumerable<IPrincipal> ExpandPrincipals(string principal)
        {
            var hashSet = new HashSet<IPrincipal>(StringComparer.Ordinal.Adapt((IPrincipal p) => p.Name));
            ExpandPrincipals(principal, hashSet);
            return hashSet;
        }
        
        private void ExpandPolicies(IEnumerable<IAccessPolicy> policies, ICollection<IAccessRule> expanded)
        {
            foreach (var accessPolicy in policies)
            {
                //var key = GenerateKey(accessPolicy);
                //ICollection<IAccessRule> ruleCollection;
                //if (!expanded.TryGetValue(key, out ruleCollection))
                //{
                //    expanded.Add(key, ruleCollection = new LinkedList<IAccessRule>());
                //}
                //ruleCollection.Add(accessPolicy);
                expanded.Add(accessPolicy);
                ExpandPolicies(GetParentPolicies(accessPolicy), expanded);
            }
        }
        private IEnumerable<IAccessRule> ExpandPolicies(string accessLevel, IEnumerable<IAccessRight> accessRights)
        {
            //IDictionary<string, ICollection<IAccessRule>> rules = new Dictionary<string, ICollection<IAccessRule>>(StringComparer.Ordinal);
            var rules = new LinkedList<IAccessRule>();
            ExpandPolicies(GetAccessPolicies(accessLevel, accessRights), rules);
            //var tmp = new TempAccessRule
            //{
            //    AccessLevel = accessLevel,
            //    AccessRights = accessRights
            //};
            //var key = GenerateKey(tmp);
            //if (!rules.ContainsKey(key))
            //{
            //    rules.Add(key, new List<IAccessRule>(1){tmp});
            //}
            return rules;//.SelectMany(x => x.Value);
        }

        protected abstract bool ValidateRules(IEnumerable<IPrincipal> principals, IEnumerable<IAccessRule> rules);

        private bool DoHasAccess(string principal, string accessLevel, IEnumerable<IAccessRight> accessRights)
        {
            var expandedPrincipals = ExpandPrincipals(principal);
            var rules = ExpandPolicies(accessLevel, accessRights).ToArray();
            return rules.Length == 0 ? false : ValidateRules(expandedPrincipals, rules);
        }

        public virtual bool ArePrincipalsRelated(IPrincipal principal1, IPrincipal principal2)
        {
            if (principal1 == null)
            {
                throw new ArgumentNullException("principal1");
            }
            if (principal2 == null)
            {
                throw new ArgumentNullException("principal2");
            }
            var comparer = StringComparer.Ordinal;
            return comparer.Equals(principal1.Name, principal2.Name)
                || ExpandPrincipals(principal1.Name).Any(x => comparer.Equals(x.Name, principal2.Name))
                || ExpandPrincipals(principal2.Name).Any(x => comparer.Equals(x.Name, principal1.Name));
        }
        public virtual bool ArePrincipalsRelated(string principal1, string principal2) { return ArePrincipalsRelated(GetPrincipal(principal1), GetPrincipal(principal2)); }

        public void DemandAccess(IPrincipal principal, IAccessLevelEntry accessLevelEntry, IEnumerable<IAccessRight> accessRights)
        {
            if (!HasAccess(principal, accessLevelEntry, accessRights))
            {
                ThrowInsufficientAccessRightsException();
            }
        }
        [Obsolete]
        public void DemandAccess(IPrincipal principal, IAccessLevelEntry accessLevelEntry, IEnumerable<string> accessRights)
        {
            if (!HasAccess(principal, accessLevelEntry, accessRights))
            {
                ThrowInsufficientAccessRightsException();
            }
        }
        public void DemandAccess(IPrincipal principal, string accessLevel, IEnumerable<IAccessRight> accessRights)
        {
            if (!HasAccess(principal, accessLevel, accessRights))
            {
                ThrowInsufficientAccessRightsException();
            }
        }
        [Obsolete]
        public void DemandAccess(IPrincipal principal, string accessLevel, IEnumerable<string> accessRights)
        {
            if (!HasAccess(principal, accessLevel, accessRights))
            {
                ThrowInsufficientAccessRightsException();
            }
        }

        public bool HasAccess(IPrincipal principal, IAccessLevelEntry accessLevelEntry, IEnumerable<IAccessRight> accessRights)
        {
            if (principal == null)
            {
                throw new ArgumentNullException("principal");
            }
            if (accessLevelEntry == null)
            {
                throw new ArgumentNullException("accessLevelEntry");
            }
            return principal.Name.EqualsOrdinal(accessLevelEntry.Owner) 
                || DoHasAccess(principal.Name, accessLevelEntry.AccessLevel, accessRights);
        }
        [Obsolete]
        public bool HasAccess(IPrincipal principal, IAccessLevelEntry accessLevelEntry, IEnumerable<string> accessRights)
        {
            if (principal == null)
            {
                throw new ArgumentNullException("principal");
            }
            if (accessLevelEntry == null)
            {
                throw new ArgumentNullException("accessLevelEntry");
            }
            return principal.Name.EqualsOrdinal(accessLevelEntry.Owner) 
                || DoHasAccess(principal.Name, accessLevelEntry.AccessLevel, accessRights.Select(GetAccessRight));
        }
        public bool HasAccess(IPrincipal principal, string accessLevel, IEnumerable<IAccessRight> accessRights)
        {
            if (principal == null)
            {
                throw new ArgumentNullException("principal");
            }
            if (accessLevel == null)
            {
                throw new ArgumentNullException("accessLevel");
            } 
            return DoHasAccess(principal.Name, accessLevel, accessRights);
        }
        [Obsolete]
        public bool HasAccess(IPrincipal principal, string accessLevel, IEnumerable<string> accessRights)
        {
            if (principal == null)
            {
                throw new ArgumentNullException("principal");
            }
            if (accessLevel == null)
            {
                throw new ArgumentNullException("accessLevel");
            }
            return DoHasAccess(principal.Name, accessLevel, accessRights.Select(GetAccessRight));
        }

        private static void ThrowInsufficientAccessRightsException()
        {
            throw new UnauthorizedAccessException();
        }
    }
}
