using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Projekt_atrakcje.Models
{
    [Table("Attractions")]
    public class Attraction
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [StringLength(30)]
        [Display(Name = "Nazwa atrakcji")]
        public string? Name_attraction { get; set; }

        [Required]
        [StringLength(30)]
        [Display(Name = "Kategoria")]
        public string? Category { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name = "Opis")]
        public string? Description { get; set; }

        [ForeignKey("CityId")]
        public int? CityId { get; set; }

        [Display(Name = "Miasto")]
        public City? City { get; set; }

        [Display(Name = "Oceny")]
        public ICollection<Grade>? Grades { get; set; }

    }
}
