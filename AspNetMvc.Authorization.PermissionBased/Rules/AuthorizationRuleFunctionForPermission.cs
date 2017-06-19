using System;

namespace AspNetMvc.Authorization.PermissionBased.Rules
{
    public class AuthorizationRuleFunctionForPermission : AuthorizationRuleForPermission
    {
        public Func<string, string, bool> RuleFunction { get; set; }

        public AuthorizationRuleFunctionForPermission(string permission, string resourceType = null) : base(permission, resourceType)
        {
        }

        public AuthorizationRuleFunctionForPermission(string[] permissions, string resourceType = null) : base(permissions, resourceType)
        {
        }

        public override bool Authorize(string userId, string resourceId)
        {
            if(RuleFunction == null)
            {
                throw new NullReferenceException("RuleFunction must be defined");
            }
            return RuleFunction.Invoke(userId, resourceId);
        }
    }
}
