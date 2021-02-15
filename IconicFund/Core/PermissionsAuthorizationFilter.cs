using IconicFund.Helpers.Enums;
using IconicFund.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IconicFund.Web.Core
{
    public class PermissionAuthorizeAttribute : TypeFilterAttribute
    {
        public PermissionAuthorizeAttribute(string[] requiredPermissions) : base(typeof(PermissionAuthorizeFilter))
        {
            Arguments = new object[] { requiredPermissions };
        }
    }

    public class PermissionAuthorizeFilter : IAuthorizationFilter
    {
        readonly string[] _requiredPermissions;

        public PermissionAuthorizeFilter(string[] requiredPermissions)
        {
            _requiredPermissions = requiredPermissions;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var userPermissions = Helper.GetUserPermissions(context.HttpContext);            

            if (UserHasPermission(userPermissions, _requiredPermissions.ToList()) == false)
            {
                context.Result = new ForbidResult();
            }
        }

        private bool UserHasPermission(List<string> userPermissions, List<string> allowedPermissions)
            {
            if (userPermissions?.Count > 0)
            {
                foreach (var allowedPermission in allowedPermissions)
                {
                    if (userPermissions.Contains(allowedPermission))
                    {
                        return true;
                    }
                }
            }

            return false;
        }


    }
}
