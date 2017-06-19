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
            return permissionsCache[userId];
        }

        public void Set(TUserId userId, ICollection<string> permissions)
        {
            Remove(userId);
            permissionsCache.Add(userId, permissions);
        }

        public void Remove(TUserId userId)
        {
            if (Has(userId))
            {
                permissionsCache.Remove(userId);
            }
        }

        public bool Has(TUserId userId)
        {
            return permissionsCache.ContainsKey(userId);
        }

        public void Clear()
        {
            permissionsCache.Clear();
        }
    }
}
