using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace IconicFund.Models.Entities
{
    public class Role
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }

        public ICollection<AdminRole> Admins { get; set; }
    }
}
