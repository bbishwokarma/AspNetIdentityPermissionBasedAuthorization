using System.Collections.Generic;

namespace AspNetMvc.Authorization.PermissionBased.Rules
{
    /// <summary>
    /// When the signed in user does not have the specified permission(s), then 
    /// this rule will return false, signigying the user does not have permission.
    /// </summary>
    public class AuthorizeWhenUserHasPermission : AuthorizationRule
    {
        public bool For(ICollection<string> permissions, string resourceType)
        {
            return permissions.Count == 0;
        }

        public bool Authorize(string userId, string resourceId)
        {
            return false;
        }
    }
}
