using IconicFund.Models;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace IconicFund.Web.Core
{
    public interface ISessionService
    {
        string Culture { get; }
        bool IsArabic { get; }
        AdminSessionUser User { get; }


        void SetSessionVariable(string key, object value);

        T GetSessionVariable<T>(string key) where T : class;

        Task LoginUser(HttpContext httpContext, AdminSessionUser loggedInUser, bool rememberMe);
    }
}
