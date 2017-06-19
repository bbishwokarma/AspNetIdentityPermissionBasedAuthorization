using System.Collections.Generic;

namespace AspNetMvc.Authorization.PermissionBased.Rules
{
    /// <summary>
    /// The rule that defines authorization of a user to the requested resource.
    /// </summary>
    public interface AuthorizationRule
    {
        /// <summary>
        /// Authorization rule applies when this method returns true.
        /// </summary>
        /// <param name="permissions">Permissions the application user has</param>
        /// <param name="resourceType">Type of resource user is requesting.</param>
        /// <returns></returns>
        bool For(ICollection<string> permissions, string resourceType);

        /// <summary>
        /// Identifies whether the user has access to the resource.
        /// </summary>
        /// <param name="userId">User ID of logged in user</param>
        /// <param name="resourceId">Resource ID the user is requesting</param>
        /// <returns></returns>
        bool Authorize(string userId, string resourceId);
    }
}
