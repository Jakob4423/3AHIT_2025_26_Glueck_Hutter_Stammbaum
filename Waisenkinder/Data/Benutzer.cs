using System.ComponentModel.DataAnnotations;

namespace ITP2Tree.Data
{
    /// <summary>
    /// Repräsentiert einen Benutzer (Account) der Anwendung.
    /// </summary>
    /// <remarks>
    /// Die Klasse enthält die minimalen Felder für Authentifizierung und Navigation zu Personen.
    /// `PasswortHash` speichert das gehashte Passwort (nicht Klartext).
    /// </remarks>
    public class Benutzer
    {
        /// <summary>
        /// Eindeutige Id des Benutzers (Primärschlüssel).
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// E-Mail-Adresse des Benutzers. Muss ein gültiges E-Mail-Format haben.
        /// </summary>
        [Required]
        [EmailAddress(ErrorMessage = "Ungültige E-Mail-Adresse.")]
        public string Email { get; set; } = string.Empty;

        /// <summary>
        /// Gehashter Passwort-String. Mindestens 8 Zeichen (Rohdaten vor dem Hashen).
        /// </summary>
        [Required]
        [MinLength(8, ErrorMessage = "Passwort muss mindestens 8 Zeichen enthalten.")]
        public string PasswortHash { get; set; } = string.Empty;

        /// <summary>
        /// Anzeigename des Benutzers.
        /// </summary>
        [Required]
        [RegularExpression(@"^[A-Za-zÄÖÜäöüß\s\-]+$", ErrorMessage = "Name darf nur Buchstaben, Leerzeichen und Bindestriche enthalten.")]
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Navigation: Liste aller Personen, die diesem Benutzer gehören.
        /// </summary>
        public List<Person> Personen { get; set; } = new();
    }
}
