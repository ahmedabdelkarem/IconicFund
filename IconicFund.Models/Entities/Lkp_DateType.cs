using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace IconicFund.Models.Entities
{
    public class Lkp_DateType
    {
        
        public int Id { get; set; }

        [Required]
        public string DateTypeName_Ar { get; set; }

        [Required]
        public string DateTypeName_En { get; set; }

        public virtual ICollection<BasicSystemSetting> BasicSystemSetting { get; set; }
    }
}
