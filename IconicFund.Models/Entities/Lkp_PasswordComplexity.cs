using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace IconicFund.Models.Entities
{
    public class Lkp_PasswordComplexity
    {
        
        public int Id { get; set; }

        [Required]
        public string ComplexityName_Ar { get; set; }

        [Required]
        public string ComplexityName_En { get; set; }

        public virtual ICollection<BasicSystemSetting> BasicSystemSetting { get; set; }

    }
}
