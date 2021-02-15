using AutoMapper;
using IconicFund.Helpers;
using IconicFund.Helpers.Enums;
using IconicFund.Models;
using IconicFund.Models.Entities;
using IconicFund.Resources;
using IconicFund.Services.IServices;
using IconicFund.Web.Core;
using IconicFund.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using X.PagedList;
using SysIO = System.IO;

namespace IconicFund.Web.Controllers
{
    [Authorize]
    public class AdminsController : Controller
    {
        private readonly IAdminsService adminsService;
        private readonly IHasherService hasherService;
        private readonly IWebHostEnvironment hostEnvironment;
        private readonly ISessionService sessionService;
        private readonly ILoggingService loggingService;
        private readonly IMapper mapper;

        public AdminsController(IAdminsService adminsService, IHasherService hasherService, IWebHostEnvironment hostEnvironment,
            ISessionService sessionService, ILoggingService loggingService, IMapper mapper)
        {
            this.adminsService = adminsService;
            this.hasherService = hasherService;
            this.hostEnvironment = hostEnvironment;
            this.sessionService = sessionService;
            this.loggingService = loggingService;
            this.mapper = mapper;

        }
        [PermissionAuthorize(new[] { "ViewAdmins" })]
        public async Task<IActionResult> Index(int page = 1, Guid? roleId = null, bool? isActive = null, string EmplyeeNo = null, bool? isManager = null, string departmentCode = "", string name = "", string email = "", string mobileNumber = null, int? pageSize = null)
        {
            var model = new AdminsListViewModel { Name = name, Email = email, EmplyeeNo = EmplyeeNo, MobileNumber = mobileNumber, IsActive = isActive, RoleId = roleId, IsManager = isManager, DepartmentCode = departmentCode, PageSize = pageSize.HasValue ? pageSize.Value : Constants.PageSize };

            model.Admins = (await adminsService.Search(name, mobileNumber, email, EmplyeeNo, isActive, roleId, isManager, departmentCode)).ToPagedList(page, model.PageSize);

            await model.FillRolesList(adminsService);
            ViewBag.sessionService = sessionService;

            return View(model);
        }

        [HttpGet]
        [PermissionAuthorize(new[] { "CreateAdmin" })]
        public async Task<ActionResult> Create()
        {
            var model = new AdminViewModel();
            await model.FillRolesList(adminsService);

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [PermissionAuthorize(new[] { "CreateAdmin" })]
        public async Task<ActionResult> Create(AdminViewModel model)
        {
            try
            {
                ModelState.Remove("Ger_ActivationStartDate");
                ModelState.Remove("Ger_ActivationEndDate");

                if (ModelState.IsValid)
                {
                    if (await adminsService.IsNationalIdAlreadyExists(model.NationalId))
                    {
                        TempData[Constants.ErrorMessage] = Messages.NationalIdAlreadyExists;
                        await model.FillRolesList(adminsService);
                        return View(model);
                    }
                    if (adminsService.IsEmailExists(model.Email))
                    {
                        TempData[Constants.ErrorMessage] = Messages.EmailAlreadyExists;
                        await model.FillRolesList(adminsService);
                        return View(model);
                    }
                    if (adminsService.IsPhoneNumberExists(model.MobileNumber))
                    {
                        TempData[Constants.ErrorMessage] = Messages.MobileNumberAlreadyExists;
                        await model.FillRolesList(adminsService);
                        return View(model);
                    }

                    if (Request.Form.Files?.Count > 0)
                    {
                        MemoryStream ms = new MemoryStream();
                        Request.Form.Files["ProfileImage"].CopyTo(ms);
                        model.ProfileImageData = ms.ToArray();
                        Request.Form.Files["SignatureImage"].CopyTo(ms);
                        model.SignatureImageData = ms.ToArray();

                        ms.Close();
                        ms.Dispose();

                        model.ProfileImage = await SaveFile(Request.Form.Files["ProfileImage"]);
                        model.SignatureImage = await SaveFile(Request.Form.Files["SignatureImage"]);
                    }

                    var admin = new Admin
                    {
                        FirstName = model.FirstName,
                        SecondName = model.SecondName,
                        ThirdName = model.ThirdName,
                        LastName = model.LastName,
                        NationalId = model.NationalId,
                        EmplyeeNo = model.EmplyeeNo,
                        MobileNumber = model.MobileNumber,
                        Email = model.Email,
                        IsActive = model.IsActive,
                        ActivationStartDate = model.ActivationStartDate,
                        ActivationEndDate = model.ActivationEndDate,
                        Title = model.Title,
                        CanApprove = model.CanApprove,
                        ProfileImage = model.ProfileImage,
                        SignatureImage = model.SignatureImage,
                        IsManager = model.IsManager,
                        DepartmentCode = model.DepartmentCode,
                        ProfileImageData = model.ProfileImageData,
                        SignatureImageData = model.SignatureImageData,

                        Password = hasherService.ComputeSha256Hash(model.MobileNumber),

                        Roles = model.SelectedRolesIds.Select(i => new AdminRole { RoleId = i }).ToList()

                    };

                    await adminsService.Add(admin, sessionService.User.Id);

                    TempData[Constants.SuccessMessage] = Messages.CreateSuccess;

                    return RedirectToAction("Index", "Admins");
                }

                await model.FillRolesList(adminsService);
                return View(model);
            }
            catch (Exception ex)
            {
                TempData[Constants.ErrorMessage] = Messages.ResourceManager.GetString(ex.Message);

                await model.FillRolesList(adminsService);
                return View(model);
            }
        }

        [HttpGet]
        [PermissionAuthorize(new[] { "EditAdmin" })]
        public async Task<ActionResult> Edit(Guid id)
        {
            var admin = await adminsService.GetAdminById(id);
            if (admin == null)
            {
                return NotFound();
            }
            string spletedGregorianDate = "";
            string spletedActivationEndDate = "";
            if (admin.ActivationStartDate != null)
            {
                var gregorianDate = ConvertDatetime.ConvertDateCalendar(admin.ActivationStartDate.AddDays(1), "Gregorian", "en-US");
                spletedGregorianDate = gregorianDate.Split()[0];
            }
            if (admin.ActivationEndDate != null)
            {
                var gregorianDate = ConvertDatetime.ConvertDateCalendar(((DateTime)admin.ActivationEndDate).AddDays(1), "Gregorian", "en-US");
                spletedActivationEndDate = gregorianDate.Split()[0];
            }
            var model = new AdminViewModel()
            {
                Id = admin.Id,
                FirstName = admin.FirstName,
                SecondName = admin.SecondName,
                ThirdName = admin.ThirdName,
                LastName = admin.LastName,
                Title = admin.Title,
                NationalId = admin.NationalId,
                MobileNumber = admin.MobileNumber,
                Email = admin.Email,
                CanApprove = admin.CanApprove,
                IsActive = admin.IsActive,
                ActivationStartDate = admin.ActivationStartDate,
                ActivationEndDate = admin.ActivationEndDate,
                EmplyeeNo = admin.EmplyeeNo,
                RootPath = $"{Constants.AdminsUploadDirectory.Replace('/', '\\')}",
                ProfileImage = admin.ProfileImage,
                SignatureImage = admin.SignatureImage,
                IsManager = admin.IsManager,
                DepartmentCode = admin.DepartmentCode,
                SelectedRolesIds = admin.Roles.Select(i => i.RoleId).ToList(),
                Ger_ActivationStartDate_string = spletedGregorianDate,
                Ger_ActivationEndDate_string = spletedActivationEndDate
            };
            await model.FillRolesList(adminsService);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [PermissionAuthorize(new[] { "EditAdmin" })]
        public async Task<ActionResult> Edit(AdminViewModel model)
        {
            try
            {
                var _adminData = await adminsService.GetAdminById(model.Id);
                if (_adminData == null)
                {
                    return NotFound();
                }
                ModelState.Remove("Ger_ActivationStartDate");
                ModelState.Remove("Ger_ActivationEndDate");
                if (ModelState.IsValid)
                {
                    var admin = _adminData;
                    if (await adminsService.IsNationalIdAlreadyExists(model.NationalId, admin.Id))
                    {
                        TempData[Constants.ErrorMessage] = Messages.NationalIdAlreadyExists;
                        await model.FillRolesList(adminsService);
                        return View(model);
                    }

                    if (Request.Form.Files?.Count > 0)
                    {
                        MemoryStream ms = new MemoryStream();
                        Request.Form.Files["NewProfileImage"].CopyTo(ms);
                        model.ProfileImageData = ms.ToArray();
                        Request.Form.Files["NewSignatureImage"].CopyTo(ms);
                        model.SignatureImageData = ms.ToArray();

                        ms.Close();
                        ms.Dispose();

                        model.ProfileImage = await SaveFile(Request.Form.Files["NewProfileImage"], true, model.ProfileImage);
                        model.SignatureImage = await SaveFile(Request.Form.Files["NewSignatureImage"], true, model.SignatureImage);
                    }

                    admin.FirstName = model.FirstName;
                    admin.SecondName = model.SecondName;
                    admin.ThirdName = model.ThirdName;
                    admin.LastName = model.LastName;
                    admin.NationalId = model.NationalId;
                    admin.EmplyeeNo = model.EmplyeeNo;
                    admin.MobileNumber = model.MobileNumber;
                    admin.Email = model.Email;
                    admin.IsActive = model.IsActive;
                    admin.ActivationStartDate = model.ActivationStartDate;
                    admin.ActivationEndDate = model.ActivationEndDate;
                    admin.Title = model.Title;
                    admin.CanApprove = model.CanApprove;
                    admin.ProfileImage = model.ProfileImage;
                    admin.SignatureImage = model.SignatureImage;
                    admin.IsManager = model.IsManager;
                    admin.DepartmentCode = model.DepartmentCode;
                    admin.Roles = model.SelectedRolesIds.Select(i => new AdminRole { RoleId = i }).ToList();
                    admin.ProfileImageData = model.ProfileImageData;
                    admin.SignatureImageData = model.SignatureImageData;

                    await adminsService.Update(admin);


                    await loggingService.LogActionData<Admin>(LoggingCategory.Administrator, LoggingAction.Edit,
                       _adminData, admin, sessionService.User.Id, _adminData.Id.ToString());


                    TempData[Constants.SuccessMessage] = Messages.EditSuccess;

                    return RedirectToAction("Index", "Admins");
                }

                await model.FillRolesList(adminsService);
                return View(model);
            }
            catch (Exception ex)
            {
                TempData[Constants.ErrorMessage] = Messages.ResourceManager.GetString(ex.Message);

                await model.FillRolesList(adminsService);
                return View(model);
            }
        }

        [HttpGet]
        [PermissionAuthorize(new[] { "DeleteAdmin" })]
        public async Task<ActionResult> Delete(Guid id)
        {
            var admin = await adminsService.GetAdminById(id);

            if (admin == null)
            {
                return NotFound();
            }

            await adminsService.Delete(admin, sessionService.User.Id);

            TempData[Constants.SuccessMessage] = Messages.DeleteSuccess;

            return RedirectToAction("Index", "Admins");
        }

        private async Task<string> SaveFile(IFormFile file, bool deleteOld = false, string oldFileName = null)
        {
            if (file != null && file.Length > 0)
            {
                //Delete the old file (used by Edit action)
                if (deleteOld)
                {
                    DeleteFile(oldFileName);
                }

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

        private void DeleteFile(string fileName)
        {
            try
            {
                SysIO.File.Delete($"{hostEnvironment.WebRootPath}{Constants.AdminsUploadDirectory.Replace('/', '\\')}{fileName}");
            }
            catch { }
        }

        [HttpGet]
        public async Task<IActionResult> TransactionLetterDefaultStatment()
        {
            if (!String.IsNullOrEmpty(adminsService.GetDefaultLetterStatementByAdminId(sessionService.User.Id)))
            {
                var model = new AdminViewModel();
                model.TransactionLetterDefaultStatment = adminsService.GetDefaultLetterStatementByAdminId(sessionService.User.Id);
                return View("TransactionLetterDefaultStatment", model);
            }
            return View("TransactionLetterDefaultStatment", new AdminViewModel());

        }

        [HttpPost]

        public async Task<IActionResult> SaveAdminDefaultLetterStatment(AdminViewModel model)
        {
            var adminData = await adminsService.GetAdminById(sessionService.User.Id);

            if (adminData != null)
            {
                adminData.DefaultLetterStatement = model.TransactionLetterDefaultStatment;
                await adminsService.Update(adminData);

            }
            TempData[Constants.SuccessMessage] = Messages.CreateSuccess;
            return View("TransactionLetterDefaultStatment");
        }




    }
}