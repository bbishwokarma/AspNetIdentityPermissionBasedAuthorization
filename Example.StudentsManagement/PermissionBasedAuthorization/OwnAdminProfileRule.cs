using AspNetMvc.Authorization.PermissionBased.Rules;
using Example.StudentsManagement.DAL;
using Example.StudentsManagement.Models;
using System;
using System.Linq;

namespace Example.StudentsManagement.PermissionBasedAuthorization
{
    public class OwnAdminProfileRule : AuthorizationRuleForPermission
    {
        public OwnAdminProfileRule(string permission, string resourceType = null) : base(permission, resourceType)
        {
        }

        public override bool Authorize(string userId, string resourceId)
        {
            InMemoryRepository repository = new InMemoryRepository();
            var resource = repository.GetAll<Administrator>().Where(a => a.Id.ToString() == resourceId).FirstOrDefault();
            return resource != null && resource.User.Username == userId;
        }
    }
}