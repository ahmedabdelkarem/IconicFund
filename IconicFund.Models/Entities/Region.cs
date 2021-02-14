using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IconicFund.Models.Entities
{
    public class Region
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        #region City
        [Required]
        [ForeignKey("City")]
        public int CityId { get; set; }
        public City City { get; set; }
        #endregion

    }
}
