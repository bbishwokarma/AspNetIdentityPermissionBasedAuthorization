using System.Web.Mvc;

namespace AspNetMvc.Authorization.PermissionBased
{
    public class AuthorizePermissionAttribute : AuthorizeAttribute
    {
        public string ResourceType { get; set; }
        /// <summary>
        /// Does not support parameter name "id". Please, use other parameters, such as "studentId"
        /// </summary>
        public string IdParameterName { get; set; }
        private string[] permissions = null;

        public AuthorizePermissionAttribute(string permission)
        {
            permissions = new string[] { permission };
        }

        public AuthorizePermissionAttribute(string[] permissions)
        {
            this.permissions = permissions;
        }

        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            base.OnAuthorization(filterContext);
            var user = ((Controller)filterContext.Controller).User;
            string resourceId = null;
            if (!string.IsNullOrEmpty(IdParameterName))
            {
                resourceId = filterContext.HttpContext.Request[IdParameterName.ToLower().Trim()]; 
            }
            bool hasPermission = user.HasPermissionIn(permissions, ResourceType, resourceId);
            if (!hasPermission)
            { 
                this.HandleUnauthorizedRequest(filterContext);
            }
        }

    }
}
