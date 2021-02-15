using IconicFund.Helpers.Enums;
using IconicFund.Models.Entities;
using IconicFund.Resources;
using IconicFund.Services.IServices;
using IconicFund.Web.Core;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using X.PagedList;

namespace IconicFund.Web.Models
{
    public class PermissionsGroupsViewModel
    {
        public string Code { get; set; }

        [Display(Name = "Description", ResourceType = typeof(Labels))]
        [Required(ErrorMessageResourceName = "RequiredField", ErrorMessageResourceType = typeof(Messages))]
        public string Name { get; set; }

        [Display(Name = "StartDate", ResourceType = typeof(Labels))]
        [Required(ErrorMessageResourceName = "RequiredField", ErrorMessageResourceType = typeof(Messages))]
        public DateTime? StartDate { get; set; }//= DateTime.Now;

        [Display(Name = "Ger_StartDate", ResourceType = typeof(Labels))]
        public DateTime? Ger_StartDate { get; set; } 
        public string Ger_StartDate_string { get; set; } 

        [Display(Name = "EndDate", ResourceType = typeof(Labels))]
        [Required(ErrorMessageResourceName = "RequiredField", ErrorMessageResourceType = typeof(Messages))]
        public DateTime? EndDate { get; set; }//= DateTime.Now;

        [Display(Name = "Ger_EndDate", ResourceType = typeof(Labels))]
        public DateTime? Ger_EndDate { get; set; }
        public string Ger_EndDate_string { get; set; }

        [Display(Name = "ViewSecretTransactions", ResourceType = typeof(Labels))]
        [Required(ErrorMessageResourceName = "RequiredField", ErrorMessageResourceType = typeof(Messages))]
        public bool ViewSecretTransactions { get; set; }

        [Display(Name = "PermissionsList", ResourceType = typeof(Labels))]
        public string PermissionsList { get; set; }                 //Comma separated Permissinos IDs

        public List<string> PermissionsListValues { get; set; }

        [Display(Name = "GroupAdmins", ResourceType = typeof(Labels))]
        public List<Admin> Admins { get; set; }

        public List<SelectListItem> Employees { get; set; } = new List<SelectListItem>();
        public IPagedList<PermissionsGroupsViewModel> PermissionsGroupsList { get; set; }

        public async Task FillLists(ISessionService sessionService, IAdminsService adminsService)
        {
            this.Employees = (await adminsService.getAll()).OrderBy(i => i.Id).Select(i => new SelectListItem
            {
                Text = string.Concat(i.FirstName, " ", i.LastName),
                Value = i.Id.ToString(),
                // Selected = i.Id == AssignedEmployeeId
            }).ToList();


        }

    }
    }

