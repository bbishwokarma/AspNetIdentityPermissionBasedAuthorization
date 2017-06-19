using System.Collections.Generic;
using System.Linq;

namespace AspNetMvc.Authorization.PermissionBased.Rules
{
    public abstract class AuthorizationRuleForPermission : AuthorizationRule
    {
        ICollection<string> permissions;
        string resourceType;

        public AuthorizationRuleForPermission(string permission, string resourceType = null)
        {
            this.permissions = new string[] { permission };
            this.resourceType = resourceType;
        }

        public AuthorizationRuleForPermission(string[] permissions, string resourceType = null)
        {
            if (permissions != null)
                this.permissions = permissions;
            else
                this.permissions = new string[0];
            this.resourceType = resourceType;
        }

        public bool For(ICollection<string> permissions, string resourceType)
        {
            bool permissionMatches = this.permissions == null ? false : this.permissions.Count(p => permissions.Contains(p)) > 0;
            bool resourceMatches = this.resourceType == null ? true : this.resourceType == resourceType;
            return permissionMatches && resourceMatches;
        }

        public abstract bool Authorize(string userId, string resourceId);
    }
}
