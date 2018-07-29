using System;
using System.Collections.Generic;
using System.Linq;
using AspNetMvc.Authorization.PermissionBased.Rules;

namespace AspNetMvc.Authorization.PermissionBased
{
    /// <summary>
    /// Main service that checks whether a user has a permission
    /// </summary>
    public class PermissionAuthorizationService
    {
        private static bool initialized = false;
        private static PermissionsProvider provider = null;
        private static PermissionsCache<string> cache = new PermissionsCache<string>();

        private static List<AuthorizationRule> specialRules = new List<AuthorizationRule>();

        public static void Initialize(PermissionsProvider permissionProvider)
        {
            provider = permissionProvider;
            if (provider != null)
            {
                provider.Configure(cache);
                initialized = true;
            }
        }

        /// <summary>
        /// Adds rules to be applied based on permissions. The added rules will be checked in the order they are added.
        /// </summary>
        public static void AddRule(AuthorizationRule rule)
        {
            specialRules.Add(rule);
        }

        public static bool HasPermissionIn(string userId, ICollection<string> permissions, string resourceType = null, string resourceId = null)
        {
            if (!initialized)
                throw new Exception("Authorization Service has not been initialized");
            if (string.IsNullOrWhiteSpace(userId))
            {
                throw new ArgumentException("User ID must not be blank or null.");
            }
            var matchedPermissions = provider.GetPermissions(userId).Where(p => permissions.Contains(p)).ToList();
            foreach (var rule in specialRules)
            {
                if (rule.For(matchedPermissions, resourceType))
                {
                    if (rule.Authorize(userId, resourceId) == false)
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        internal static List<string> GetPermissions(string userId)
        {
            return provider.GetPermissions(userId).ToList();
        }

        internal static void Logout(string userId)
        {
            cache.Remove(userId);
        }
    }
}
