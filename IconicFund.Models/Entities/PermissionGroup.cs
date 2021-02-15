using IconicFund.Helpers.Enums;
using IconicFund.Models.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace IconicFund.Models.Entities
{
    public class PermissionGroup : BaseEntity
    {
        [Key]
        public string Code { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        [Required]
        public bool ViewSecretTransactions { get; set; }

        [Required]
        public string PermissionsList { get; set; }                 //Comma separated Permissinos IDs

        public virtual ICollection<PermissionGroupAdmin> Admins { get; set; }

      //  [NotMapped]
        //public List<Permission> Permissions
        //{
        //    get
        //    {
        //        return PermissionsList?.Split(',', StringSplitOptions.RemoveEmptyEntries).Select(i => (Permission)Convert.ToInt32(i)).ToList();
        //    }
        //}

    }
}
