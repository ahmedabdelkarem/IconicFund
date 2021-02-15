using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using IconicFund.Helpers;
using IconicFund.Models.Entities;
using IconicFund.Resources;
using IconicFund.Services.IServices;
using IconicFund.Web.Core;
using IconicFund.Web.Models;
using Microsoft.AspNetCore.Mvc;
using X.PagedList;
using Microsoft.Extensions.Configuration;
using System.Globalization;
using Microsoft.AspNetCore.Http;
using IconicFund.Helpers.Enums;
 
namespace IconicFund.Web.Controllers
{
    public class PermissionsGroupController : Controller
    {
        private readonly IPermissionsGroupService permissionsGroupService;
        private readonly IAdminsService adminsService;
        private readonly IPermissionGroupAdminService permissionGroupAdminService;
        private readonly IMapper mapper;
        private readonly ISessionService sessionService;
        private readonly IConfiguration configuration;
        private readonly ILoggingService loggingService;

        public PermissionsGroupController(IPermissionsGroupService permissionsGroupService, IAdminsService adminsService, 
            IPermissionGroupAdminService permissionGroupAdminService, ISessionService sessionService, IMapper mapper 
            , IConfiguration configuration, ILoggingService loggingService)
        {
            this.permissionsGroupService = permissionsGroupService;
            this.adminsService = adminsService;
            this.permissionGroupAdminService = permissionGroupAdminService;
            this.mapper = mapper;
            this.configuration = configuration;
            this.loggingService = loggingService;
            this.sessionService = sessionService;
        }


        #region PermissionGroup
        [PermissionAuthorize(new[] { "ViewPermissionGroup" })]
        public async Task<IActionResult> Index(int page = 1,string Name = null , DateTime? StartDate = null, DateTime? EndDate = null)
        {
            var model = new PermissionsGroupsViewModel
            { Name = Name 
            , StartDate = StartDate.HasValue ? ConvertDatetime.ConvertToGregorianDate(StartDate.Value) : StartDate  
            , EndDate = EndDate.HasValue ? ConvertDatetime.ConvertToGregorianDate(EndDate.Value) : EndDate };

            var permissionGroups = await permissionsGroupService.Search( Name , StartDate , EndDate);

            //if (permissionGroups.Count > 0)
            //{
            //    var List = mapper.Map<List<PermissionsGroupsViewModel>>(permissionGroups);
            //    model.PermissionsGroupsList = List.ToPagedList(page, Constants.PageSize);
            //}

            var List = mapper.Map<List<PermissionsGroupsViewModel>>(permissionGroups);
            model.PermissionsGroupsList = List.ToPagedList(page, Constants.PageSize);

            await model.FillLists(sessionService, adminsService);
            ViewBag.sessionService = sessionService;
            return View(model);

        }
        [HttpGet]
        [PermissionAuthorize(new[] { "CreatePermissionGroup" })]
        public async Task<ActionResult> Create()

        {
            var model = new PermissionsGroupsViewModel();
            await model.FillLists(sessionService, adminsService);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [PermissionAuthorize(new[] { "CreatePermissionGroup" })]
        public async Task<ActionResult> Create(PermissionsGroupsViewModel permissionsGroupsViewModel)
        {
            try
            { 
                ModelState.Remove("Ger_StartDate");
                ModelState.Remove("Ger_EndDate");

                if (ModelState.IsValid)
                {
                    if (permissionsGroupsViewModel.EndDate < permissionsGroupsViewModel.StartDate)
                    {
                        TempData[Constants.ErrorMessage] = Messages.EndDateMustExceedStartDate;

                        //ModelState.AddModelError("EndDate", Messages.EndDateMustExceedStartDate);
                        return View(permissionsGroupsViewModel);
                    }
                    #region Generate Random ID For Test

                    Random random = new Random();
                    int PermissionGroupId = random.Next(1000);
                    string PermissionGroupPrefix = configuration.GetSection("AppSettings").GetSection("PermissionGroupPrefix").Value;
                    permissionsGroupsViewModel.Code = string.Concat(PermissionGroupPrefix, PermissionGroupId.ToString());

                    #endregion

                    var IsCodeExist = await permissionsGroupService.IsCodeExist(permissionsGroupsViewModel.Code);

                    if (IsCodeExist)
                    {
                        ModelState.AddModelError("Code", Messages.CodeAlreadyExists);
                        return View(permissionsGroupsViewModel);
                    }

                    permissionsGroupsViewModel.PermissionsList = "test";

                    var PermissionGroup = mapper.Map<PermissionGroup>(permissionsGroupsViewModel);

                    await permissionsGroupService.Add(PermissionGroup , sessionService.User.Id);

                    TempData[Constants.SuccessMessage] = Messages.CreateSuccess;

                    return RedirectToAction("Index", "PermissionsGroup");
                }
             
                return View(permissionsGroupsViewModel);
            }
            catch (Exception ex)
            {
                TempData[Constants.ErrorMessage] = Messages.ResourceManager.GetString(ex.Message);

                return View(permissionsGroupsViewModel);
            }
        }

        [HttpGet]
        [PermissionAuthorize(new[] { "EditPermissionGroup" })]
        public async Task<ActionResult> Edit(string id)
        {
            var permissionGroups = await permissionsGroupService.GetPermissionGroupById(id);
            if (permissionGroups == null)
            {
                return NotFound();
            }
            var model = mapper.Map<PermissionsGroupsViewModel>(permissionGroups);
            string spletedStartDate = "";
            string spletedEndDate = "";
            if (model.StartDate != null)
            {
                var gregorianDate = ConvertDatetime.ConvertDateCalendar(((DateTime)model.StartDate).AddDays(1), "Gregorian", "en-US");
                spletedStartDate = gregorianDate.Split()[0];
            }
            if (model.EndDate != null)
            {
                var gregorianDate = ConvertDatetime.ConvertDateCalendar(((DateTime)model.EndDate).AddDays(1), "Gregorian", "en-US");
                spletedEndDate = gregorianDate.Split()[0];
            }

            model.Ger_StartDate_string = spletedStartDate;
            model.Ger_EndDate_string = spletedEndDate;
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [PermissionAuthorize(new[] { "EditPermissionGroup" })]
        public async Task<ActionResult> Edit(PermissionsGroupsViewModel permissionsGroupsViewModel)
        {
            try
            {

                var _permissionGroups = await permissionsGroupService.GetPermissionGroupById(permissionsGroupsViewModel.Code);

                if (_permissionGroups == null)
                {
                    return NotFound();
                }

                ModelState.Remove("Ger_StartDate");
                ModelState.Remove("Ger_EndDate");

                if (ModelState.IsValid)
                {
                    if (permissionsGroupsViewModel.EndDate < permissionsGroupsViewModel.StartDate)
                    {
                        TempData[Constants.ErrorMessage] = Messages.EndDateMustExceedStartDate;

                        //ModelState.AddModelError("EndDate", Messages.EndDateMustExceedStartDate);
                        return View(permissionsGroupsViewModel);
                    }
                    // var PermissionGroups = mapper.Map<PermissionGroup>(permissionsGroupsViewModel);
                    var  permissionGroups = _permissionGroups;
                    permissionGroups.Name = permissionsGroupsViewModel.Name;
                    permissionGroups.StartDate = (DateTime)permissionsGroupsViewModel.StartDate;
                    permissionGroups.EndDate = (DateTime)permissionsGroupsViewModel.EndDate;
                    permissionGroups.ViewSecretTransactions = permissionsGroupsViewModel.ViewSecretTransactions;

                    await permissionsGroupService.Update(permissionGroups);

                    await loggingService.LogActionData<PermissionGroup>(LoggingCategory.PermissionGroup, LoggingAction.Edit,
                     _permissionGroups , permissionGroups, sessionService.User.Id, _permissionGroups.Code);



                    TempData[Constants.SuccessMessage] = Messages.EditSuccess;

                    return RedirectToAction("Index", "PermissionsGroup");
                }

                return View(permissionsGroupsViewModel);
            }
            catch (Exception ex)
            {
                TempData[Constants.ErrorMessage] = Messages.ResourceManager.GetString(ex.Message);

                return View(permissionsGroupsViewModel);
            }
        }

        [HttpGet]
        [PermissionAuthorize(new[] { "DeletePermissionGroup" })]
        public async Task<ActionResult> Delete(string id)
        {
            var permissionGroups = await permissionsGroupService.GetPermissionGroupById(id);
            if (permissionGroups == null)
            {
                return NotFound();
            }

            await permissionsGroupService.Delete(permissionGroups , sessionService.User.Id);

            TempData[Constants.SuccessMessage] = Messages.DeleteSuccess;

            return RedirectToAction("Index", "PermissionsGroup");
        }

        #endregion

        #region PermissionsGroupAdmins

        [HttpGet]
        public async Task<ActionResult> PermissionGroupAdmins(string id)
        {
            var model = new PermissionGroupAdminPartialViewModel();

            HttpContext.Session.SetString("GroupCode", id);

            model.PermissionGroupAdminList = await permissionGroupAdminService.GetAdminsByGroupId(id);
            model.AdminsList = await adminsService.getAll();
            return PartialView("_PermissionGroupAdmins", model);

        }

        [HttpPost]

        public async Task<ActionResult> CreatePermissionGroupAdmins(Guid AdminId, bool CheckedValue)
        {

            try
            {
                string GroupId = HttpContext.Session.GetString("GroupCode");

                if (CheckedValue != false)
                {

                    await permissionGroupAdminService.Add(new PermissionGroupAdmin
                    {
                        AdminId = AdminId,
                        PermissionGroupCode = GroupId
                    } , sessionService.User.Id);

                    TempData[Constants.SuccessMessage] = Messages.CreateSuccess;
                }
                else
                {
                    var model = new PermissionGroupAdmin();
                    model.AdminId = AdminId;
                    model.PermissionGroupCode = GroupId;
                    await permissionGroupAdminService.Delete(model , sessionService.User.Id);
                }

                var permissionGroup = new PermissionGroupAdminPartialViewModel();
                permissionGroup.PermissionGroupAdminList = await permissionGroupAdminService.GetAdminsByGroupId(GroupId);
                permissionGroup.AdminsList = await adminsService.getAll();
                return PartialView("_PermissionGroupAdmins", permissionGroup);



            }
            catch (Exception ex)
            {

                return RedirectToAction("Index", "PermissionsGroup");
            }



        }




        #endregion

        #region Permissions
        public async Task<ActionResult> PermissionsList(string id)
        {

            var model = new PermissionsGroupsViewModel();

            HttpContext.Session.SetString("GroupCode", id);
            model.PermissionsListValues = (await permissionsGroupService.GetPermissionListByGroupID(id)).Split(",").ToList();

            return PartialView("_PermissionsList", model);
        }
        public async Task<ActionResult> CreatePermissionList(List<string> Permissions)
        {

            try
            {
                string GroupId = HttpContext.Session.GetString("GroupCode");
                string permissionList = string.Join(",", Permissions);

                var permissionGroup = await permissionsGroupService.GetPermissionGroupById(GroupId);

                permissionGroup.PermissionsList = permissionList;
                await permissionsGroupService.Update(permissionGroup);

                 TempData[Constants.SuccessMessage] = Messages.CreateSuccess;
                 return RedirectToAction("Index", "PermissionsGroup");
            }
            catch (Exception ex)
            {
                TempData[Constants.ErrorMessage] = Messages.ResourceManager.GetString(ex.Message);
                return RedirectToAction("Index", "PermissionsGroup");
            }
        }


        #endregion


    }
}
