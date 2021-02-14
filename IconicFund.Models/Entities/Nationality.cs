using System.ComponentModel.DataAnnotations;

namespace IconicFund.Models.Entities
{
    public class Nationality
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
    }
}
