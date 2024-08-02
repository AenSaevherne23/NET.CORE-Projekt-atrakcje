using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace Projekt_atrakcje.Models
{
    [PrimaryKey(nameof(UserId), nameof(AttractionId))]
    [Table("Grades")]
    public class Grade
    {
        [Required]
        [ForeignKey("UserId")]
        public int UserId { get; set; }

        [Required]
        [ForeignKey("AttractionId")]
        public int AttractionId { get; set; }

        [Display(Name = "Użytkownik")]
        public User? User { get; set; }

        [Display(Name = "Atrakcja")]
        public Attraction? Attraction { get; set; }

        [Display(Name = "Ocena")]
        //[Range(1, 10, ErrorMessage = "Ocena musi być z zakresu od 1 do 10.")]
        [Precision(3, 1)]
        public decimal Ocena { get; set; }
    }
}
