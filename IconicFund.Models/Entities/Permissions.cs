using IconicFund.Helpers.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IconicFund.Models.Entities
{
    public class Permissions
    {
        [Key]
        public int ID { get; set; }

        public string Name { get; set; }

        public int? ApplicationID { get; set; }
    }
}
