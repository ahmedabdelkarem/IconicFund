using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IconicFund.Models.Entities
{
    public class AdminRole
    {
        #region Admin

        [Key, Column(Order = 1)]
        [ForeignKey("Admin")]
        public Guid AdminId { get; set; }
        public Admin Admin { get; set; }

        #endregion

        #region Role

        [Key, Column(Order = 2)]
        [ForeignKey("Role")]
        public Guid RoleId { get; set; }
        public Role Role { get; set; }

        #endregion
    }
}
