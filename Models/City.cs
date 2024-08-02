using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace Projekt_atrakcje.Models
{
    [Table("Cities")]
    public class City
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [StringLength(30)]
        [Display(Name = "Nazwa")]
        public string? Name { get; set; }


        [Required]
        [StringLength(30)]
        [Display(Name = "Region")]
        public string? Region { get; set; }

        [Required]
        [Display(Name = "Populacja")]
        public int? Population {  get; set; }

        [ForeignKey("CountryId")]
        public int? CountryId { get; set; }

        [Display(Name = "Kraj")]
        public Country? Country { get; set; }

        public virtual ICollection<Attraction>? Attrations{ get; }

        public ICollection<User>? Users { get; set; }
    }
}
