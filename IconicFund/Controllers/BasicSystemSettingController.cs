using AutoMapper;
using IconicFund.Models.Entities;
using IconicFund.Services.IServices;
using IconicFund.Web.Core;
using IconicFund.Helpers;
using IconicFund.Resources;
using IconicFund.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Configuration;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using SysIO = System.IO;
using Microsoft.AspNetCore.Hosting;
using System.Globalization;
using IconicFund.Services.IServices;

namespace IconicFund.Web.Controllers
{
    [Authorize]
    public class BasicSystemSettingController : Controller
    {
        private readonly IBasicSystemSettingService BasicSystemSettingService;
        private readonly IMapper mapper;
        private readonly ILogger<BasicSystemSettingController> _logger;
        private readonly ILoggingService loggingService;
        private readonly ISessionService sessionService;
        private readonly IDateTypesService DateTypesService;
        private readonly IPasswordComplexityService PasswordComplexityService;
        private readonly IWebHostEnvironment hostEnvironment;
        private readonly IPermissionsGroupService PermissionsGroupService;
        public BasicSystemSettingController(IBasicSystemSettingService BasicSystemSettingService, IDateTypesService DateTypesService, IMapper mapper, IPasswordComplexityService PasswordComplexityService, ISessionService sessionService, ILogger<BasicSystemSettingController> logger, ILoggingService loggingService, IWebHostEnvironment hostEnvironment, IPermissionsGroupService permissionsGroupService)
        {
            this.BasicSystemSettingService = BasicSystemSettingService;
            this.sessionService = sessionService;
            this.DateTypesService = DateTypesService;
            this.PasswordComplexityService = PasswordComplexityService;
            _logger = logger;
            this.mapper = mapper;
            this.loggingService = loggingService;
            this.hostEnvironment = hostEnvironment;
            this.PermissionsGroupService = permissionsGroupService;
    }

        [PermissionAuthorize(new[] { "ViewSystemsetting" })]

        public async Task<IActionResult> Index()
        {
            var set =  BasicSystemSettingService.GetLastBasicSystemSetting();

            var _SystemSettingViewModel = set != null ? mapper.Map<BasicSystemSettingViewModel>(set) : new BasicSystemSettingViewModel();

            await _SystemSettingViewModel.FillLists(sessionService, DateTypesService, PasswordComplexityService, PermissionsGroupService);


            var folderPath ="/Uploads/Admins";
            _SystemSettingViewModel.FullPathImage = folderPath + "/" + _SystemSettingViewModel.SystemLogo;


            return View(_SystemSettingViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [PermissionAuthorize(new[] { "EditSystemsetting" })]
        //public async Task<ActionResult> EditSystemSetting(BasicSystemSettingViewModel viewModel)
        public async Task<ActionResult> Index(BasicSystemSettingViewModel viewModel)
        {
            try
            {

                viewModel.IncomingSerialNumberPrefix = viewModel.IncomingPrefixType == 2 ? await GetCurrentGerogianYear() :
                    viewModel.IncomingPrefixType == 3 ? await GetCurrentHijriYear() : viewModel.IncomingSerialNumberPrefix;

                viewModel.ExportSerialNumberPrefix = viewModel.ExportPrefixType == 2 ? await GetCurrentGerogianYear() :
                    viewModel.ExportPrefixType == 3 ? await GetCurrentHijriYear() : viewModel.ExportSerialNumberPrefix;


                if (ModelState.IsValid)
                {
                    var BasicSystemSetting = mapper.Map<BasicSystemSetting>(viewModel);
                    if (Request.Form.Files?.Count > 0)
                    {
                        BasicSystemSetting.SystemLogo = await SaveFile(Request.Form.Files["NewProfileImage"]);
                    }
                    if (BasicSystemSetting.Id == 0)
                    {
                        await BasicSystemSettingService.Add(BasicSystemSetting, sessionService.User.Id);
                    }
                    else
                    {
                        await BasicSystemSettingService.Update(BasicSystemSetting);

                    }
                    TempData[Constants.SuccessMessage] = Messages.SaveSuccess;

                    return RedirectToAction("Index", "BasicSystemSetting");
                }

                return View(viewModel);
            }
            catch (Exception ex)
            {
                TempData[Constants.ErrorMessage] = Messages.ResourceManager.GetString(ex.Message);

                return View(viewModel);
            }
        }

        private async Task<string> SaveFile(IFormFile file, bool deleteOld = false, string oldFileName = null)
        {
            if (file != null && file.Length > 0)
            {

                //Save the uploaded file

                //  Read the file content
                var contentBytes = new byte[file.Length];

                file.OpenReadStream().Read(contentBytes, 0, contentBytes.Length);


                //  Determine the file path
                var newFileName = $"{Guid.NewGuid()}{SysIO.Path.GetExtension(file.FileName)}";

                var path = $"{hostEnvironment.WebRootPath}{Constants.AdminsUploadDirectory.Replace('/', '\\')}{newFileName}";

                //  Write the file to the disk
                await SysIO.File.WriteAllBytesAsync(path, contentBytes);

                return newFileName;
            }
            return oldFileName;
        }

        private async Task<string> GetCurrentGerogianYear()
        {
            return DateTime.Now.Year.ToString();
        }

        private async Task<string> GetCurrentHijriYear()
        {
            HijriCalendar hijri = new HijriCalendar();
            return hijri.GetYear(DateTime.Now).ToString();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> ValidateIncomingSerialNumberPrefix()
        {
            //if (IncomingPrefixType == 1)
            //{
            //    if (string.IsNullOrEmpty(IncomingSerialNumberPrefix))
            //        return Json(false);
            //}

            return Json(false);
        }
    }
}
