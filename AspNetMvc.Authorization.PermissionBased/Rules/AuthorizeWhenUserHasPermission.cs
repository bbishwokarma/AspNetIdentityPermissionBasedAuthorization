using System.Collections.Generic;

namespace AspNetMvc.Authorization.PermissionBased.Rules
{
    /// <summary>
    /// When the user has permission(s), then this rule returns true.
    /// </summary>
    public class AuthorizeWhenUserHasPermission : AuthorizationRule
    {
        public bool Authorize(string userId, string resourceId)
        {
            return true;
        }

        public bool For(ICollection<string> permissions, string resourceType)
        {
            return permissions.Count > 0;
        }
    }
}
