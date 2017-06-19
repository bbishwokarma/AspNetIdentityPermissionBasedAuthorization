using System;
using System.Collections.Generic;

namespace AspNetMvc.Authorization.PermissionBased
{
    public class PermissionsProvider
    {
        private Func<string, ICollection<string>> providerFunction = null;
        private bool cacheEnabled = false;
        private PermissionsCache<string> permissionsCache = null;

        public PermissionsProvider(Func<string, ICollection<string>> providerFunction, bool cacheEnabled = true)
        {
            this.providerFunction = providerFunction;
            this.cacheEnabled = cacheEnabled;
        }

        public void Configure(PermissionsCache<string> permissionsCache)
        {
            if (cacheEnabled)
                this.permissionsCache = permissionsCache;
        }


        public ICollection<string> GetPermissions(string userId)
        {
            if (permissionsCache != null)
            {
                if (permissionsCache.Has(userId) == false)
                {
                    permissionsCache.Set(userId, providerFunction.Invoke(userId));
                }
                return permissionsCache.Get(userId);
            }
            return providerFunction.Invoke(userId);
        }

    }
}
