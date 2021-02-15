using IconicFund.Models.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace IconicFund.Web.Models
{
    public class UsersChecksViewModel
    {
        public List<Admin> DepartmentAdmins { get; set; }

        public int Order { get; set; }

    }
}
