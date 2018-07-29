using System;
using System.Collections.Generic;

namespace AspNetMvc.Authorization.PermissionBased
{
   public class PermissionsCache<TUserId> where TUserId : System.IConvertible
    {
        private Dictionary<TUserId, ICollection<string>> permissionsCache = new Dictionary<TUserId, ICollection<string>>();

        private static PermissionsCache<TUserId> _instance = null;

        public static PermissionsCache<TUserId> Instance
        {
            get
            {
                if(_instance == null)
                {
                    _instance = new PermissionsCache<TUserId>();
                }
                return _instance;
            }
        }

        public ICollection<string> Get(TUserId userId)
        {
            if(userId == null)
            {
                throw new ArgumentException("User ID must not be null.");
            }
            return permissionsCache[userId];
        }

        public void Set(TUserId userId, ICollection<string> permissions)
        {
            if (userId == null)
            {
                throw new ArgumentException("User ID must not be null.");
            }
            Remove(userId);
            permissionsCache.Add(userId, permissions);
        }

        public void Remove(TUserId userId)
        {
            if (userId == null)
            {
                throw new ArgumentException("User ID must not be null.");
            }
            if (Has(userId))
            {
                permissionsCache.Remove(userId);
            }
        }

        public bool Has(TUserId userId)
        {
            if (userId == null)
            {
                throw new ArgumentException("User ID must not be null.");
            }
            return permissionsCache.ContainsKey(userId);
        }
        
    }
}
