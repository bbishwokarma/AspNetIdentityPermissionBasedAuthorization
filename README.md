# Asp.Net MVC Identity - Permission Based Authorization
## Adds permission based authorization capability on Asp.Net MVC Identity Framework

## Motivation
#### Problem
There are instances where role based authorization itself is not sufficient or not appropriate. One example is when the business team is not precise on the number of roles the application need to have, or the permissions a role need to have. Or, the business team keeps on changing permissions on the roles defined for an application.

In such situation, if the application was designed to use merely role based authorization, then the changes require additional support effort for development and operational support team.

#### One Solution
Permissions of an application don't change unless new features are added, or some features are removed. So, authorizing requests based on permission and providing the business team (or admin) the capability of assigning permissions to application users will help minimize the operational support effort.

As an example, the admin of an application creates a new role for an application, assigns certain permissions to the that role, and assigns the role to application users. Providing this capability to admin will address the changing part, i.e., the roles and accesses associated with the roles.

This framework provides a mechanism for authorizing resources based on permissions.

## Using this Framework

For checking permission within Razor view, or inside Action method of a controller, this framework provides three extension methods for IPrincipal interface.

```CS
//The string constants in Permissions and ResourceTypes should be defined
class Permissions{
	public const string EditOwnStudentProfile = "Edit Own Student Profile";
	public const string EditAllStudentProfiles = "Edit All Student Profiles";
}

class ResourceTypes{
	public const string STUDENT = "Student";
}

if (User.HasPermission(Permissions.EditOwnStudentProfile, ResourceTypes.STUDENT, Model.Id.ToString())) { 
	//A student can edit only his/her details
}

if (User.HasPermissionIn(new string[] {Permissions.EditAllStudentProfiles, Permissions.EditOwnStudentProfile}, ResourceTypes.STUDENT, Model.Id.ToString())) { 
    //Department staff can edit profile of all students (Permissions.EditAllStudentProfiles)
	//A student can edit only his/own profile (Permissions.EditOwnStudentProfile)
}

if (User.GetPermissions().Contains(Permissions.EditAllStudentProfiles)) { 
	//Department staff can edit profile of all students 
}
```

For authorizing the controller, or action method of an controller, this framework provides AuthorizePermissionAttribute inherited from AuthorizeAttribute.

```CS
using AspNetMvc.Authorization.PermissionBased;
...
[AuthorizePermission(Permission.ViewAllStudents)]
public ActionResult Index(int? studentId)
{
	...
}

[AuthorizePermission(new string[] { Permission.EditAllStudentProfiles, Permission.EditOwnStudentProfile}, IdParameterName = "consultantId", ResourceType = ResourceTypes.CONSULTANT)]
public ActionResult Edit(int? studentId)
{
	...
}

```

## Initialization and adding authorization rules are must
Initialization and adding authorization rules should be done in when application starts.

In ```Application_Start()``` event of ```Global.asax.cs```

```CS
using AspNetMvc.Authorization.PermissionBased;
using AspNetMvc.Authorization.PermissionBased.Rules;

var permissionProvider = new PermissionsProvider(userId => {
	//returns List<string> permissions for the userId (User.Identity.GetUserId())
});

PermissionAuthorizationService.Initialize(permissionProvider);

//Rules should be added in the order they will be processed. If a rule fails, then the user is not authorized for the resource
PermissionAuthorizationService.AddRule(new AuthorizeWhenUserHasPermission());

PermissionAuthorizationService.AddRule(new MyPermissionRule(Permissions.EditOwnStudentProfile, ResourceTypes.STUDENT));
Or,
PermissionAuthorizationService.AddRule(new AuthorizationRuleFunctionForPermission(Permissions.EditOwnStudentProfile, ResourceTypes.STUDENT) {
	 RuleFunction =   (userId, resourceId) => {
		return true; //Check if userId has access to resourceId
	}
});

...

class MyPermissionRule : AuthorizationRuleForPermission{
	public MyPermissionRule(string permission, string resourceType = null) : base(permission, resourceType){}
	
	public override bool Authorize(string userId, string resourceId)
	{
		return true; //Check if userId has access to resourceId
	}
}

Or,
class MyPermissionRule : AuthorizationRule{
	public bool For(ICollection<string> permissions, string resourceType)
	{
		//return true if this rule is to be applied for the input permissions and input resourceType
	}
	
	public override bool Authorize(string userId, string resourceId)
	{
		return true; //Check if userId has access to resourceId
	}
}
```

##### [Bikash Bishwokarma](https://bbishwokarma.github.io/)

