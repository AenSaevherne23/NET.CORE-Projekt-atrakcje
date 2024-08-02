using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Projekt_atrakcje.Models
{
    [Table("Users")]
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [StringLength(30)]
        [Display(Name = "Nazwa użytkownika")]
        public string? Name_user { get; set; }

        [Required]
        [StringLength(30)]
        [Display(Name = "E-mail")]
        public string? Email { get; set; }

        [Required]
        [StringLength(30)]
        [Display(Name = "Hasło")]
        public string? Password { get; set; }

        [Display(Name = "Oceny")]
        public ICollection<Grade>? Grades { get; set; }

        public virtual ICollection<City>? Cities { get; set; }
    }
}
