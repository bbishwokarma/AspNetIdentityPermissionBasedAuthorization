using Example.StudentsManagement.DAL;
using Example.StudentsManagement.Models;
using System.Collections.Generic;
using System.Linq;

namespace Example.StudentsManagement.PermissionBasedAuthorization
{
    public class MyPermissionsProvider
    {
        public static List<string> GetPermissions(string username)
        {
            InMemoryRepository repository = new InMemoryRepository();
            var user = repository.GetAll<ApplicationUser>().Where(u => u.Username == username).First();
            var permissions = user != null ? user.Roles.SelectMany(r => r.Permissions).ToList() : new List<string>();
            return permissions;
        }
    }
}