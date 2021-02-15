using IconicFund.Helpers;
using IconicFund.Models;
using IconicFund.Models.Entities;
using IconicFund.Services.IServices;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

namespace IconicFund.Web.Core
{
    public class SessionService : ISessionService
    {
        public string Culture { get; }
        public bool IsArabic { get; }
        public AdminSessionUser User { get; private set; }

        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IPermissionGroupAdminService _PermissionsGroupAdminService;

        public SessionService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;

            var httpContext = httpContextAccessor.HttpContext;
            if (string.IsNullOrEmpty(httpContext.Session.GetString("user")))
            {
                if (httpContext.User?.Identity?.IsAuthenticated == true && httpContext.User?.Claims?.Count() > 0)
                {
                    try
                    {
                        AdminSessionUser sessionUser = new AdminSessionUser
                        {
                            Id = Guid.Parse(httpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value),
                            NationalId = httpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value,
                            FullName = httpContext.User.Claims.FirstOrDefault(c => c.Type == Constants.FullNameClaimType)?.Value,
                            PermissionsList = httpContext.User.Claims.FirstOrDefault(c => c.Type == Constants.PermissionsClaimType)?.Value,
                           // GroupPermissionCode = GetPermissionCodeOfUser(Guid.Parse(httpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value))
                        };

                        httpContext.Session.SetString("user", JsonConvert.SerializeObject(sessionUser));
                    }
                    catch (Exception ex) { }
                }
            }


            Culture = Thread.CurrentThread.CurrentCulture.Name.ToString();
            IsArabic = Thread.CurrentThread.CurrentCulture.Name == Constants.ARCultureCode;

            if (!string.IsNullOrEmpty(httpContext.Session.GetString("user")))
            {
                User = JsonConvert.DeserializeObject<AdminSessionUser>(httpContext.Session.GetString("user"));
            }
        }

        //public string GetPermissionCodeOfUser(Guid ID)
        //{
        //    string code = _PermissionsGroupAdminService.GetAdminGroupByID(ID).ToString();
        //    return code;
        //}

        public void SetSessionVariable(string key, object value)
        {
            _httpContextAccessor.HttpContext.Session.SetString(key, JsonConvert.SerializeObject(value));
        }

        public T GetSessionVariable<T>(string key) where T : class
        {
            var sessionValue = _httpContextAccessor.HttpContext.Session.GetString(key);

            if (!string.IsNullOrEmpty(sessionValue))
            {
                return JsonConvert.DeserializeObject<T>(sessionValue);
            }

            return null;
        }

        public async Task LoginUser(HttpContext httpContext, AdminSessionUser loggedInUser, bool rememberMe)
        {
            Claim[] claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, loggedInUser.Id.ToString()),
                new Claim(ClaimTypes.Name, loggedInUser.NationalId),
                new Claim(Constants.FullNameClaimType, loggedInUser.FullName),
                new Claim(Constants.PermissionsClaimType, loggedInUser.PermissionsList)
            };

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);
            var props = new AuthenticationProperties
            {
                IsPersistent = rememberMe,
                ExpiresUtc = DateTimeOffset.UtcNow.AddHours(1),
                //ExpiresUtc = model.RememberMe ? DateTimeOffset.UtcNow.AddYears(100) : DateTimeOffset.UtcNow.AddHours(12),
            };

            await httpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, props);
            // Fill Session
            httpContext.Session.SetString("user", JsonConvert.SerializeObject(loggedInUser));

            this.User = loggedInUser;
        }


    }

}
