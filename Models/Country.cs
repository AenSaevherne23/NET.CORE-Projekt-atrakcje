using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Projekt_atrakcje.Models
{
    [Table("Countries")]
    public class Country
    {
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Nazwa")]
        public string? Name { get; set; }


        [Required]
        [Display(Name = "Stolica")]
        public string? Capital { get; set; }

        [Required]
        [Display(Name = "Powierzchnia [km2]")]
        public int? Area { get; set; }

        public virtual ICollection<City>? Cities { get; }

    }
}
