using AutoMapper;
using IconicFund.Helpers;
using IconicFund.Helpers.Enums;
using IconicFund.Models.Entities;
using IconicFund.Resources;
using IconicFund.Services.IServices;
using IconicFund.Services.Services;
using IconicFund.Web.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using X.PagedList;

namespace IconicFund.Web.Models
{
    public class BasicSystemSettingViewModel : BasicSystemSetting
    {
        [Required(ErrorMessageResourceName = "RequiredField", ErrorMessageResourceType = typeof(Messages))]
        [Display(Name = "DateTypeId_Fk", ResourceType = typeof(Labels))]
        public int? DateTypeId_Fk { get; set; }

        [Required(ErrorMessageResourceName = "RequiredField", ErrorMessageResourceType = typeof(Messages))]
        [Display(Name = "SystemTitle_Ar", ResourceType = typeof(Labels))]
        [RegularExpression(@"^[\u0600-\u06ff\s]+$", ErrorMessageResourceName = "NameArabic", ErrorMessageResourceType = typeof(Messages))]
        public string SystemTitle_Ar { get; set; }

        [Required(ErrorMessageResourceName = "RequiredField", ErrorMessageResourceType = typeof(Messages))]
        [Display(Name = "SystemTitle_En", ResourceType = typeof(Labels))]
        [RegularExpression(@"^\w+( \w+)*$", ErrorMessageResourceName = "NameEnglish", ErrorMessageResourceType = typeof(Messages))]
        public string SystemTitle_En { get; set; }


        [Required(ErrorMessageResourceName = "RequiredField", ErrorMessageResourceType = typeof(Messages))]
        [Display(Name = "SessionTime", ResourceType = typeof(Labels))]
        public double? SessionTime { get; set; }

        [Required(ErrorMessageResourceName = "RequiredField", ErrorMessageResourceType = typeof(Messages))]
        [Display(Name = "ManyWrongLoginAvailability", ResourceType = typeof(Labels))]
        public int? ManyWrongLoginAvailability { get; set; }


        [Required(ErrorMessageResourceName = "RequiredField", ErrorMessageResourceType = typeof(Messages))]
        [Display(Name = "PasswordExpiredAfter", ResourceType = typeof(Labels))]
        public int? PasswordExpiredAfter { get; set; }


        [Required(ErrorMessageResourceName = "RequiredField", ErrorMessageResourceType = typeof(Messages))]
        [Display(Name = "MinPassword", ResourceType = typeof(Labels))]
        public int? MinPassword { get; set; }

        [Required(ErrorMessageResourceName = "RequiredField", ErrorMessageResourceType = typeof(Messages))]
        [Display(Name = "PasswordComplexityId_Fk", ResourceType = typeof(Labels))]
        public int? PasswordComplexityId_Fk { get; set; }


        [Required(ErrorMessageResourceName = "RequiredField", ErrorMessageResourceType = typeof(Messages))]
        [Display(Name = "PermissionGroups", ResourceType = typeof(Labels))]
        public string GroupPermissionCode { get; set; }

        public string FullPathImage { get; set; }


        public int IncomingPrefixType { get; set; } = 1;
        public int ExportPrefixType { get; set; } = 1;

        #region Incoming Serial Number Settings 

        [Required(ErrorMessageResourceName = "RequiredField", ErrorMessageResourceType = typeof(Messages))]
        //[Remote(action: "ValidateIncomingSerialNumberPrefix", controller: "BasicSystemSetting",  AdditionalFields = "IncomingPrefixType"
        //    , ErrorMessageResourceName = "RequiredField", ErrorMessageResourceType = typeof(Messages))]
        [Display(Name = "IncomingSerialNumberPrefix", ResourceType = typeof(Labels))]
        public string IncomingSerialNumberPrefix { get; set; }              //بادئة الوارد

        [Required(ErrorMessageResourceName = "RequiredField", ErrorMessageResourceType = typeof(Messages))]
        [Display(Name = "IncomingSerialNumberStartValue", ResourceType = typeof(Labels))]
        public int IncomingSerialNumberStartValue { get; set; } = 1;        //بداية تسلسل الوارد

        [Display(Name = "IncomingSerialNumberDigitsCount", ResourceType = typeof(Labels))]
        public int? IncomingSerialNumberDigitsCount { get; set; }           //عدد الخانات لتسلسل الوارد

        [Display(Name = "IncomingSerialNumberPostfix", ResourceType = typeof(Labels))]
        public string IncomingSerialNumberPostfix { get; set; }             //خاتمة الوارد

        #endregion


        #region Export Serial Number Settings 

        [Required(ErrorMessageResourceName = "RequiredField", ErrorMessageResourceType = typeof(Messages))]
        [Display(Name = "ExportSerialNumberPrefix", ResourceType = typeof(Labels))]
        public string ExportSerialNumberPrefix { get; set; }              //بادئة الصادر

        [Required(ErrorMessageResourceName = "RequiredField", ErrorMessageResourceType = typeof(Messages))]
        [Display(Name = "ExportSerialNumberStartValue", ResourceType = typeof(Labels))]
        public int ExportSerialNumberStartValue { get; set; } = 1;        //بداية تسلسل الصادر

        [Display(Name = "ExportSerialNumberDigitsCount", ResourceType = typeof(Labels))]
        public int? ExportSerialNumberDigitsCount { get; set; }           //عدد الخانات لتسلسل الصادر

        [Display(Name = "ExportSerialNumberPostfix", ResourceType = typeof(Labels))]
        public string ExportSerialNumberPostfix { get; set; }             //خاتمة الصادر

        #endregion

        public List<SelectListItem> DateTypes { get; set; } = new List<SelectListItem>();
        public List<SelectListItem> PasswordComplexitys { get; set; } = new List<SelectListItem>();
        public List<SelectListItem> GroupPermissionList { get; set; } = new List<SelectListItem>();

        public async Task FillLists(ISessionService sessionService, IDateTypesService DateTypesService, IPasswordComplexityService PasswordComplexityService, IPermissionsGroupService PermissionsGroupService)
        {
             

         

            this.PasswordComplexitys = (await PasswordComplexityService.getAll()).OrderBy(i => i.Id).Select(i => new SelectListItem
            {
                Text = ((sessionService.IsArabic) ? i.ComplexityName_Ar : i.ComplexityName_En),
                Value = i.Id.ToString(),
                Selected = i.Id == Id
            }).ToList();


            this.GroupPermissionList = (await PermissionsGroupService.getAll()).OrderBy(i => i.Code).Select(i => new SelectListItem
            {
                Text = ((sessionService.IsArabic) ? i.Name : i.Name),
                Value = i.Code.ToString(),
            }).ToList();

        }
    }

}