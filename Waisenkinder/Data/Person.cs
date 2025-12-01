using System.ComponentModel.DataAnnotations;
//Glück 
namespace ITP2Tree.Data
{
    /// <summary>
    /// Repräsentiert eine Person in der Anwendung.
    /// </summary>
    /// <remarks>
    /// Klasse im einfachen HTL-Stil dokumentiert: Felder sind soweit möglich streng validiert.
    /// - Der Eigentümer der Person ist ein <see cref="Benutzer"/>.
    /// - Einige Felder sind required und werden im UI zusätzlich geprüft.
    /// </remarks>
    public class Person
    {
        /// <summary>
        /// Eindeutige Identifikationsnummer der Person (Primärschlüssel).
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// FK: Id des Benutzers, dem diese Person gehört.
        /// </summary>
        public int BenutzerId { get; set; }

        /// <summary>
        /// Navigationseigenschaft zum Besitzer (Benutzer).
        /// </summary>
        public Benutzer? Benutzer { get; set; }

        /// <summary>
        /// Vollständiger Name der Person. Erlaubt Buchstaben, Leerzeichen und Bindestriche.
        /// </summary>
        [Required]
        [RegularExpression(@"^[A-Za-zÄÖÜäöüß\s\-]+$", ErrorMessage = "Name darf nur Buchstaben, Leerzeichen und Bindestriche enthalten.")]
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Geburtsort der Person. Nur Buchstaben, Leerzeichen und Bindestriche erlaubt.
        /// </summary>
        [Required]
        [RegularExpression(@"^[A-Za-zÄÖÜäöüß\s\-]+$", ErrorMessage = "Geburtsort darf nur Buchstaben, Leerzeichen und Bindestriche enthalten.")]
        public string Geburtsort { get; set; } = string.Empty;

        /// <summary>
        /// Geburtsdatum als String (Format: z. B. 12.12.2000). Nur Zahlen und Punkte erlaubt.
        /// </summary>
        [Required]
        [RegularExpression(@"^[0-9.]+$", ErrorMessage = "Geburtsdatum darf nur Zahlen und Punkte enthalten (z. B. 12.12.2000).")]
        public string Geburtsdatum { get; set; } = string.Empty;

        /// <summary>
        /// Bekannte Verwandte, als komma-getrennte Liste. Erlaubte Zeichen: Buchstaben, Leerzeichen, Kommas, Bindestriche.
        /// </summary>
        [Required]
        [RegularExpression(@"^[A-Za-zÄÖÜäöüß\s\-,]+$", ErrorMessage = "Verwandte dürfen nur Buchstaben, Leerzeichen, Kommas und Bindestriche enthalten.")]
        public string Verwandte { get; set; } = string.Empty;

        /// <summary>
        /// Optionale Notizen zu dieser Person. Kann null sein.
        /// </summary>
        public string? Notizen { get; set; }
    }
}
