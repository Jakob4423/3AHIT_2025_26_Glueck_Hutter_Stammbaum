using System.ComponentModel.DataAnnotations;

namespace ITP2Tree.Data
{
    public class Benutzer
    {
        public int Id { get; set; }

        [Required]
        [EmailAddress(ErrorMessage = "Ungültige E-Mail-Adresse.")]
        public string Email { get; set; } = string.Empty;

        [Required]
        [MinLength(8, ErrorMessage = "Passwort muss mindestens 8 Zeichen enthalten.")]
        public string PasswortHash { get; set; } = string.Empty;

        [Required]
        [RegularExpression(@"^[A-Za-zÄÖÜäöüß\s\-]+$", ErrorMessage = "Name darf nur Buchstaben, Leerzeichen und Bindestriche enthalten.")]
        public string Name { get; set; } = string.Empty;

        // Navigation
        public List<Person> Personen { get; set; } = new();
    }
}
