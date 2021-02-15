using IconicFund.Helpers;
using IconicFund.Helpers.Enums;
using IconicFund.Models.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using IconicFund.Services.IServices;

namespace IconicFund.Web.Core
{
    public static class Helper
    {
        public static List<string> GetUserPermissions(HttpContext httpContext)
        {
            if (httpContext?.User?.Identity?.IsAuthenticated == true && httpContext.User.HasClaim(t => t.Type == Constants.PermissionsClaimType))
            {
                var strPermissions = httpContext.User.Claims.FirstOrDefault(t => t.Type == Constants.PermissionsClaimType)?.Value;
                if (!string.IsNullOrEmpty(strPermissions))
                {
                    return strPermissions.Split(',', StringSplitOptions.RemoveEmptyEntries).Select(i => i).ToList();
                    
                }
            }

            return null;
        }


    }
}
