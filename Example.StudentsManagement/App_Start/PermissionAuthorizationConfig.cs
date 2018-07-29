using AspNetMvc.Authorization.PermissionBased;
using AspNetMvc.Authorization.PermissionBased.Rules;
using Example.StudentsManagement.DAL;
using Example.StudentsManagement.Models;
using Example.StudentsManagement.Models.Constants;
using Example.StudentsManagement.PermissionBasedAuthorization;
using System.Linq;

namespace Example.StudentsManagement.App_Start
{
    public class PermissionAuthorizationConfig
    {
        public static void Configure()
        {
            var permissionProvider = new PermissionsProvider(MyPermissionsProvider.GetPermissions);

            //Initialization of Permission based Authorization library
            PermissionAuthorizationService.Initialize(permissionProvider);

            //Add authorization rules
            //Rules should be added in the order they will be processed. If a rule fails, then the user is not authorized for the resource

            //1. Do not authorize when the logged in user does not have specified permission
            PermissionAuthorizationService.AddRule(new AuthorizeWhenUserHasPermission());

            //2. If user has VIEW OWN STUDENT PROFILE permission, and requesting to view a student resource. 
            PermissionAuthorizationService.AddRule(new AuthorizationRuleFunctionForPermission(AppPermissions.VIEW_OWN_STUDENT_PROFILE, ResourceTypes.STUDENT)
            {
                RuleFunction = (userId, resourceId) =>
                {
                    InMemoryRepository repository = new InMemoryRepository();
                    var resource = repository.GetAll<Student>().Where(s => s.Id.ToString() == resourceId).FirstOrDefault();
                    return resource != null && resource.User.Username == userId; //Logged in user can view only his/her student profile
                }
            });

            //2. If user has VIEW OWN ADMIN PROFILE permission, and requesting to view an administrator resource. 
            PermissionAuthorizationService.AddRule(new OwnAdminProfileRule(AppPermissions.VIEW_OWN_ADMIN_PROFILE, ResourceTypes.ADMINISTRATOR));
        }
    }
}