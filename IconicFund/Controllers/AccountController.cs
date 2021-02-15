using AutoMapper;
using IconicFund.CaptchaProvider;
using IconicFund.Helpers;
using IconicFund.Models;
using IconicFund.Models.Entities;
using IconicFund.Resources;
using IconicFund.Services.IServices;
using IconicFund.Services.Services;
using IconicFund.Web.Core;
using IconicFund.Web.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using X.PagedList;

namespace IconicFund.FrontEnd.Controllers
{
    [AllowAnonymous]
    public class AccountController : Controller
    {
        private readonly IAdminsService adminsService;
        private readonly IPermissionsService permissionsService;
        private readonly ISessionService sessionService;
        private readonly IHasherService hasherService;
        private readonly IMapper mapper;
        private readonly IMailService mailService;


        public AccountController(IMailService mailService,IAdminsService adminsService, ISessionService sessionService, IHasherService hasherService, IMapper mapper, IPermissionsService permissionsService)
        {
            this.adminsService = adminsService;
            this.sessionService = sessionService;
            this.hasherService = hasherService;
            this.mapper = mapper;
            this.permissionsService = permissionsService;
            this.mailService = mailService;
        }

        [HttpGet]
        public IActionResult Login()
        {
            if (User?.Identity?.IsAuthenticated == true && User.Claims?.Count() > 0)
            {
                return RedirectToAction("Index", "Dashboard");
            }

            return View(new LoginViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            //Validate Captcha Code
            if (!Captcha.ValidateCaptchaCode(model.CaptchaCode, HttpContext))
            {
                TempData[Constants.ErrorMessage] = Messages.InvalidCaptchaCode;
                return View(model);
            }


            string errorMessage;
            Admin account;

            if (adminsService.CheckLogin(model.UserName, model.Password, out errorMessage, out account))
            {
                var sessionUser = mapper.Map<AdminSessionUser>(account);
                sessionUser.PermissionsList = GetUserActivePermissions(account.PermissionGroups);

                await sessionService.LoginUser(HttpContext, sessionUser, model.RememberMe);

                return RedirectToAction("Index", "Dashboard");
            }
            else
            {
                TempData[Constants.ErrorMessage] = errorMessage;
                return View(model);
            }
        }

        private string GetUserActivePermissions(ICollection<PermissionGroupAdmin> userPermissionGroups)
        {
            var permissions = string.Empty;

            if (userPermissionGroups?.Count > 0)
            {
                var DateNow = DateTime.Now.Date;
                var groupsPermissionsList = userPermissionGroups.Select(i => i.PermissionGroup)
                                                                  .Where(i => DateNow >= i.StartDate.Date && DateNow <= i.EndDate.Date)
                                                                  .Select(i => i.PermissionsList);
                permissions = string.Join(',', groupsPermissionsList);
          
                permissions = permissionsService.GetPermissionsListNamesByIDS(permissions.Split(',', StringSplitOptions.RemoveEmptyEntries).Select(i => Convert.ToInt32(i)).ToList());
            }

            return permissions;
        }


        [HttpGet]
        public IActionResult Logout()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            HttpContext.Session.Clear();

            return RedirectToAction("Login");
        }

        [HttpGet]
        [Authorize]
        public IActionResult AccessDenied()
        {
            return View();
        }

        [HttpGet, Authorize]
        public IActionResult ChangePassword()
        {

            return View();

        }

        [HttpPost, Authorize]
        public async Task<IActionResult> ChangePassword(changePasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var account = await adminsService.GetAdminById(sessionService.User.Id);
                if (account != null)
                {
                    var password = hasherService.ComputeSha256Hash(model.CurrentPassword);
                    if (password == account.Password)
                    {
                        account.Password = hasherService.ComputeSha256Hash(model.NewPassword);

                        await adminsService.Update(account);
                        TempData[Constants.ChangePasswordSuccessMessage] = Messages.EditSuccess;
                        return View();
                    }
                    else
                    {
                        TempData[Constants.ChangePasswordErrorMessage] = Messages.InCorrectPassword;
                        return View(model);
                    }
                }
                else
                    return RedirectToAction(nameof(AccessDenied));

            }
            return View(model);

        }

        [HttpGet, Authorize]
        public IActionResult SetPassword()
        {

            return View();

        }

        [HttpPost, Authorize]
        public async Task<IActionResult> SetPassword(setPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var account = await adminsService.GetById(sessionService.User.Id);
                if (account != null)
                {
                   
                        account.Password = hasherService.ComputeSha256Hash(model.NewPassword);

                        await adminsService.Update(account);
                        TempData[Constants.ChangePasswordSuccessMessage] = Messages.EditSuccess;
                        return RedirectToAction("Index", "Home");

                }
                else
                    return RedirectToAction(nameof(AccessDenied));

            }
            return View(model);

        }


        // [Route("get-captcha-image")]
        public IActionResult GetCaptchaImage()
        {
            int width = 100;
            int height = 36;
            var captchaCode = Captcha.GenerateCaptchaCode();
            var result = Captcha.GenerateCaptchaImage(width, height, captchaCode);
            HttpContext.Session.SetString("CaptchaCode", result.CaptchaCode);
            Stream s = new MemoryStream(result.CaptchaByteData);
            return new FileStreamResult(s, "image/png");
        }

        public async Task GetAdminsDetails(Guid AdminId)
        {

            var admin = await adminsService.GetById(AdminId);
          
                var sessionUser = mapper.Map<AdminSessionUser>(admin);
                sessionUser.PermissionsList = GetUserActivePermissions(admin.PermissionGroups);

                await sessionService.LoginUser(HttpContext, sessionUser,true);

               // return RedirectToAction("Index", "Home");
           
        }

        [AllowAnonymous]
        public IActionResult findAccount()
        {

            return View();

        }
        [HttpPost]
        public async Task<IActionResult> findAccount(ResetPasswordViewModel model)
        {
            var account = await adminsService.GetByUserName(model.UserName);
            if (account == null)
            {
                TempData[Constants.ErrorMessage] = Messages.InCorrectUserName;
                return View();
            }

            string email = account?.Email;
            if (!string.IsNullOrEmpty(email))
            {
                Random generator = new Random();
                string code = generator.Next(0, 99999).ToString("D5");
                mailService.Send(Messages.ResetPasswordMessageSubject, string.Format(Messages.ResetPasswordMessageBody, code), email);

                var sessionUser = mapper.Map<AdminSessionUser>(account);
                sessionUser.Code = code;
                sessionUser.Action = "SetPassword";
                sessionUser.Controller = "Account";
                TempData["user"] = JsonConvert.SerializeObject(sessionUser);


                TempData[Constants.SuccessMessage] = Messages.CodeSentTo + Regex.Replace(email, @"[^\s]{3}@", "***@");

                return RedirectToAction(nameof(ConfirmCode));
            }

            TempData[Constants.ErrorMessage] = Messages.InCorrectUserName;
            return View();
        }
        public IActionResult ConfirmCode()
        {
            var model = new ConfirmViewModel();
            return View("Confirm", model);

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ConfirmCode(ConfirmViewModel model)
        {
            
            var user = JsonConvert.DeserializeObject<AdminSessionUser>((string)TempData["user"]);
            if (model.Code == user.Code)
            {
                var account = await adminsService.GetById(user.Id);
                user.PermissionsList = GetUserActivePermissions(account.PermissionGroups);

                await sessionService.LoginUser(HttpContext, user, model.RememberMe);

                return RedirectToAction(user.Action, user.Controller);

            }
            TempData["user"] = JsonConvert.SerializeObject(user);
            TempData[Constants.ErrorMessage] = Messages.InvalidCaptchaCode;
            return View("Confirm", model);
        }


    }
}