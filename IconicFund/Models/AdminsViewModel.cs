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
    public class AdminsListViewModel
    {
        //Filter Properties
        public string Name { get; set; }
        public string MobileNumber { get; set; }
        public string EmplyeeNo { get; set; } 

        public string DepartmentCode { get; set; }
        public string Email { get; set; }

        public Guid? RoleId { get; set; }
        public bool? IsActive { get; set; }

        public bool? IsManager { get; set; }
        public int PageSize { get; set; }

        public IEnumerable<SelectListItem> Roles { get; private set; }
        public IEnumerable<SelectListItem> Departments { get; private set; }

        //Admins List
        public IPagedList<Admin> Admins { get; set; }

        public async Task FillRolesList(IAdminsService adminsService)
        {
            Roles = (await adminsService.GetAllRoles()).Select(i => new SelectListItem()
            {
                Text = i.Name,
                Value = i.Id.ToString(),
                Selected = (i.Id == RoleId)
            });
        }



    }

    public class AdminViewModel
    {
        public Guid Id { get; set; }

        [Required(ErrorMessageResourceName = "RequiredField", ErrorMessageResourceType = typeof(Messages))]
        [Display(Name = "NationalId", ResourceType = typeof(Labels))]
        [MaxLength(10)]
        [RegularExpression(pattern: "[1-9]{1}[0-9]{9}", ErrorMessageResourceName = "NationalIdNumbers", ErrorMessageResourceType = typeof(Messages))]
        public string NationalId { get; set; }

        [Required(ErrorMessageResourceName = "RequiredField", ErrorMessageResourceType = typeof(Messages))]
        [Display(Name = "EmployeeNumber", ResourceType = typeof(Labels))]
        public string EmplyeeNo { get; set; }


        [Required(ErrorMessageResourceName = "RequiredField", ErrorMessageResourceType = typeof(Messages))]
        [Display(Name = "FirstName", ResourceType = typeof(Labels))]
        public string FirstName { get; set; }

        [Display(Name = "SecondName", ResourceType = typeof(Labels))]
        public string SecondName { get; set; }

        [Display(Name = "ThirdName", ResourceType = typeof(Labels))]
        public string ThirdName { get; set; }

        [Required(ErrorMessageResourceName = "RequiredField", ErrorMessageResourceType = typeof(Messages))]
        [Display(Name = "LastName", ResourceType = typeof(Labels))]
        public string LastName { get; set; }

        [Display(Name = "Title", ResourceType = typeof(Labels))]
        [Required(ErrorMessageResourceName = "RequiredField", ErrorMessageResourceType = typeof(Messages))]
        public string Title { get; set; }

        [Display(Name = "DepartmentCode", ResourceType = typeof(Labels))]
        public string DepartmentCode { get; set; }


        [Required(ErrorMessageResourceName = "RequiredField", ErrorMessageResourceType = typeof(Messages))]
        [Display(Name = "MobileNumber", ResourceType = typeof(Labels))]
        [RegularExpression(pattern: "^[5][0-9]{8}$", ErrorMessageResourceName = "mobileNumber", ErrorMessageResourceType = typeof(Messages))]
        public string MobileNumber { get; set; }

        [Required(ErrorMessageResourceName = "RequiredField", ErrorMessageResourceType = typeof(Messages))]
        [EmailAddress(ErrorMessageResourceName = "InvalidEmail", ErrorMessageResourceType = typeof(Messages))]
        [Display(Name = "Email", ResourceType = typeof(Labels))]
        public string Email { get; set; }

        [Display(Name = "Active", ResourceType = typeof(Labels))]
        public bool IsActive { get; set; } = true;

        [Display(Name = "IsManager", ResourceType = typeof(Labels))]
        public bool IsManager { get; set; }

        [Required(ErrorMessageResourceName = "RequiredField", ErrorMessageResourceType = typeof(Messages))]
        [Display(Name = "ActivationStartDate", ResourceType = typeof(Labels))]
        public DateTime ActivationStartDate { get; set; } = DateTime.Now;

        [Display(Name = "Ger_ActivationStartDate", ResourceType = typeof(Labels))]
        public DateTime? Ger_ActivationStartDate { get; set; }
        public string Ger_ActivationStartDate_string { get; set; }

        [Display(Name = "ActivationEndDate", ResourceType = typeof(Labels))]
        public DateTime? ActivationEndDate { get; set; }
        public string Ger_ActivationEndDate_string { get; set; }


        [Display(Name = "Ger_ActivationEndDate", ResourceType = typeof(Labels))]
        public DateTime? Ger_ActivationEndDate { get; set; }


        [Display(Name = "CanApprove", ResourceType = typeof(Labels))]
        public bool CanApprove { get; set; } = false;

        [Display(Name = "ProfileImage", ResourceType = typeof(Labels))]
        public string ProfileImage { get; set; }

        public string RootPath { get; set; }

        [Display(Name = "SignatureImage", ResourceType = typeof(Labels))]
        public string SignatureImage { get; set; }

        [Required(ErrorMessageResourceName = "RequiredField", ErrorMessageResourceType = typeof(Messages))]
        [Display(Name = "Roles", ResourceType = typeof(Labels))]
        public List<Guid> SelectedRolesIds { get; set; }

        public List<Role> Roles { get; set; }

        
        [Display(Name = "Content", ResourceType = typeof(Labels))]
        public string TransactionLetterDefaultStatment { get; set; }         //الافاده

        public IEnumerable<SelectListItem> Departments { get; private set; }

        public byte[] SignatureImageData { get; set; }

        public byte[] ProfileImageData { get; set; }


        public async Task FillRolesList(IAdminsService adminsService)
        {
            Roles = await adminsService.GetAllRoles();
        }


    }
}