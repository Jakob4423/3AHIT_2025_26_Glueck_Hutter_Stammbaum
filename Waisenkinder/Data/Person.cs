using System.ComponentModel.DataAnnotations;

namespace ITP2Tree.Data
{
    public class Person
    {
        public int Id { get; set; }

        // Zugehörigkeit zum Benutzer (Owner)
        public int BenutzerId { get; set; }
        public Benutzer? Benutzer { get; set; }

        [Required]
        [RegularExpression(@"^[A-Za-zÄÖÜäöüß\s\-]+$", ErrorMessage = "Name darf nur Buchstaben, Leerzeichen und Bindestriche enthalten.")]
        public string Name { get; set; } = string.Empty;

        [Required]
        [RegularExpression(@"^[A-Za-zÄÖÜäöüß\s\-]+$", ErrorMessage = "Geburtsort darf nur Buchstaben, Leerzeichen und Bindestriche enthalten.")]
        public string Geburtsort { get; set; } = string.Empty;

        [Required]
        [RegularExpression(@"^[0-9.]+$", ErrorMessage = "Geburtsdatum darf nur Zahlen und Punkte enthalten (z. B. 12.12.2000).")]
        public string Geburtsdatum { get; set; } = string.Empty;

        // Bekannte Verwandte: Komma-separiert, aber nur Buchstaben prüfen
        [Required]
        [RegularExpression(@"^[A-Za-zÄÖÜäöüß\s\-,]+$", ErrorMessage = "Verwandte dürfen nur Buchstaben, Leerzeichen, Kommas und Bindestriche enthalten.")]
        public string Verwandte { get; set; } = string.Empty;

        // Optional: weitere Informationen, 5+ Felder werden im UI erzwungen
        public string? Notizen { get; set; }
    }
}
