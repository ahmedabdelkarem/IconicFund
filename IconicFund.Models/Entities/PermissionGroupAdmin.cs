using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace IconicFund.Models.Entities
{
    public class PermissionGroupAdmin
    {
        #region PermissionGroup

        [Key, Column(Order = 1)]
        public string PermissionGroupCode { get; set; }

        [ForeignKey("PermissionGroupCode")]
        public PermissionGroup PermissionGroup { get; set; }

        #endregion

        #region Admin

        [Key, Column(Order = 2)]
        public Guid AdminId { get; set; }
        [ForeignKey("AdminId")]
        public Admin Admin { get; set; }

        #endregion
    }
}
