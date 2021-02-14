using IconicFund.Helpers.Enums;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IconicFund.Models.Entities
{
    public class Department
    {
        [Key]
        public string Code { get; set; }

        [Required]
        public string NameAr { get; set; }

        [Required]
        public string NameEn { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public DepartmentTypes Type { get; set; }

        #region ParentDepartment

        [ForeignKey("ParentDepartment")]
        public string ParentDepartmentCode { get; set; }
        public Department ParentDepartment { get; set; }

        #endregion


        #region Manager id
        [ForeignKey("Manager")]
        public Guid? ManagerId { get; set; }
        public Admin Manager { get; set; }

        #endregion
    }
}
