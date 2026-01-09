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
        /// Geburtsort der Person. Nur Buchstaben, Leerzeichen und Bindestriche erlaubt. Optional.
        /// </summary>
        [RegularExpression(@"^[A-Za-zÄÖÜäöüß\s\-]+$", ErrorMessage = "Geburtsort darf nur Buchstaben, Leerzeichen und Bindestriche enthalten.")]
        public string? Geburtsort { get; set; }

        /// <summary>
        /// Geburtsdatum als String (Format: z. B. 12.12.2000). Nur Zahlen und Punkte erlaubt.
        /// </summary>
        [Required]
        [RegularExpression(@"^[0-9.]+$", ErrorMessage = "Geburtsdatum darf nur Zahlen und Punkte enthalten (z. B. 12.12.2000).")]
        public string Geburtsdatum { get; set; } = string.Empty;

        /// <summary>
        /// Bekannte Verwandte, als komma-getrennte Liste. Erlaubte Zeichen: Buchstaben, Leerzeichen, Kommas, Bindestriche. Optional.
        /// </summary>
        [RegularExpression(@"^[A-Za-zÄÖÜäöüß\s\-,|:]+$", ErrorMessage = "Verwandte dürfen nur Buchstaben, Leerzeichen, Kommas, Bindestriche, Doppelpunkte und Pipes enthalten.")]
        public string? Verwandte { get; set; }

        /// <summary>
        /// Optionale Notizen zu dieser Person. Kann null sein.
        /// </summary>
        public string? Notizen { get; set; }

        /// <summary>
        /// Navigationseigenschaft für die Verwandtschaftsbeziehungen dieser Person.
        /// </summary>
        public List<Verwandtschaft> VerwandtschaftenAlsQuelle { get; set; } = new();
        public List<Verwandtschaft> VerwandtschaftenAlsZiel { get; set; } = new();
    }

    /// <summary>
    /// Repräsentiert eine Verwandtschaftsbeziehung zwischen zwei Personen.
    /// </summary>
    public class Verwandtschaft
    {
        /// <summary>
        /// Eindeutige Identifikationsnummer.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// FK: Id der Person, die die Beziehung ausgeht (z.B. der Vater/die Mutter).
        /// </summary>
        public int PersonAId { get; set; }

        /// <summary>
        /// FK: Id der Person, die die Beziehung empfängt (z.B. das Kind).
        /// </summary>
        public int PersonBId { get; set; }

        /// <summary>
        /// Der Typ der Beziehung (z.B. "Vater", "Mutter", "Sohn", "Tochter", "Bruder", "Schwester", "Ehepartner").
        /// </summary>
        [Required]
        public string Beziehungstyp { get; set; } = string.Empty;

        /// <summary>
        /// Navigationseigenschaften.
        /// </summary>
        public Person? PersonA { get; set; }
        public Person? PersonB { get; set; }
    }
}