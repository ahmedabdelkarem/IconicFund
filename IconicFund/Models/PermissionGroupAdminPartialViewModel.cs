using IconicFund.Models.Entities;
using IconicFund.Services.IServices;
using IconicFund.Web.Core;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IconicFund.Web.Models
{
    public class PermissionGroupAdminPartialViewModel
    {

       public List<PermissionGroupAdmin> PermissionGroupAdminList { get; set; }

        public  List<Admin> AdminsList { get; set; }

        public List<SelectListItem> Employees { get; set; } = new List<SelectListItem>();
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
