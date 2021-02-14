using IconicFund.Helpers.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection.Metadata.Ecma335;
using System.Text;

namespace IconicFund.Models.Entities
{
    public class SystemLogging
    {
        [Key]
        public long ID { get; set; }

        public LoggingCategory LoggingCategory { get; set; }

        public LoggingAction LoggingAction { get; set; }

        [MaxLength(100)]
        public string RowID { get; set; }

        [Required]
        public DateTime ActionDate { get; set; } = DateTime.Now;

        
        public string OldData { get; set; }

        public string NewData { get; set; }

        [ForeignKey("UserData")]
        public Guid UserID { get; set; }


        public Admin UserData { get; set; }

    }
}
