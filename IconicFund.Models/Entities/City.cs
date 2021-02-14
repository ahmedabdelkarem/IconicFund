using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace IconicFund.Models.Entities
{
    public class City
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public virtual ICollection<Region> Regions { get; set; }
    }
}
