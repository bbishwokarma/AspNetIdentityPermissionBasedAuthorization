using Example.StudentsManagement.App_Start;
using Example.StudentsManagement.DAL;
using Example.StudentsManagement.Models;
using Example.StudentsManagement.Models.Constants;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace Example.StudentsManagement
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            PermissionAuthorizationConfig.Configure();

            //For testing purpose
            InitialDataSetup();
        }

        /// <summary>
        /// Roles and users and permissions assigned to them can be changed through application.
        /// </summary>
        private void InitialDataSetup()
        {
            InMemoryRepository repository = new InMemoryRepository();

            var superUserRole = new ApplicationRole
            {
                Id = 1,
                Role = "Super User",
                Permissions = { AppPermissions.MANAGE_PERMISSIONS, AppPermissions.MANAGE_ROLES, AppPermissions.MANAGE_ADMINISTRATOR_PROFILE}
            };
            var adminManagerRole = new ApplicationRole
            {
                Id = 2,
                Role = "Administrators Manager",
                Permissions = { AppPermissions.VIEW_ADMINISTRATOR_PROFILES, AppPermissions.MANAGE_STUDENT_PROFILE }
            };
            var studentsAdministratorRole = new ApplicationRole
            {
                Id = 3,
                Role = "Students Manager",
                Permissions = { AppPermissions.VIEW_OWN_ADMIN_PROFILE,  AppPermissions.MANAGE_STUDENT_PROFILE }
            };
            var teachingAssistantRole = new ApplicationRole
            {
                Id = 4,
                Role = "Teaching Assistant",
                Permissions = { AppPermissions.VIEW_STUDENT_PROFILES }
            };
            var studentRole = new ApplicationRole { Id = 5, Role = "Student", Permissions = { AppPermissions.VIEW_OWN_STUDENT_PROFILE } };

            repository.Add(superUserRole);
            repository.Add(adminManagerRole);
            repository.Add(studentsAdministratorRole);
            repository.Add(teachingAssistantRole);
            repository.Add(studentRole);

            var superuser = new ApplicationUser
            {
                Id = 1,
                Username = "superuser",
                Roles = { superUserRole }
            };

            var superadmin = new ApplicationUser
            {
                Id = 2,
                Username = "superadmin",
                Roles = { adminManagerRole, studentsAdministratorRole }
            };

            var superadmin2 = new ApplicationUser
            {
                Id = 3,
                Username = "superadmin2",
                Roles = { adminManagerRole }
            };

            var admin = new ApplicationUser
            {
                Id = 4,
                Username = "admin",
                Roles = { studentsAdministratorRole }
            };

            var student1 = new ApplicationUser
            {
                Id = 5,
                Username = "student1",   //This student is TA who can view other student's profiles
                Roles = { studentRole, teachingAssistantRole }
            };

            var student2 = new ApplicationUser
            {
                Id = 6,
                Username = "student2",
                Roles = { studentRole }
            };

            repository.Add(superuser);
            repository.Add(superadmin);
            repository.Add(superadmin2);
            repository.Add(admin);
            repository.Add(student1);
            repository.Add(student2);

            repository.Add(new Administrator { Id = 1, Name = "Bob Smith", User = superuser });
            repository.Add(new Administrator { Id = 2, Name = "Paul Smith", User = superadmin });
            repository.Add(new Administrator { Id = 3, Name = "Paul J. Smith", User = superadmin2 });
            repository.Add(new Administrator { Id = 4, Name = "Michael Sindhu", User = admin });
            repository.Add(new Student { Id = 5, Name = "Jeff Studants", User = student1 });
            repository.Add(new Student { Id = 6, Name = "Mitchel Studants", User = student2 });
        }
    }
}
