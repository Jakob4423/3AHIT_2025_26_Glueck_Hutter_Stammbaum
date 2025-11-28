namespace ITP2Tree.Services
{
    /// <summary>
    /// Einfacher Service zur Verwaltung des aktuell angemeldeten Benutzers.
    /// </summary>
    /// <remarks>
    /// Diese Klasse speichert nur die Id des angemeldeten Benutzers in-memory
    /// und bietet ein Event für den Wechsel des Authentifizierungszustands.
    /// Geeignet für einfache Demo-/Schulprojekte.
    /// </remarks>
    public class AuthService
    {
        /// <summary>
        /// Id des aktuell angemeldeten Benutzers, oder null wenn niemand angemeldet ist.
        /// </summary>
        public int? UserId { get; private set; }

        /// <summary>
        /// Event, das ausgelöst wird, wenn sich der Authentifizierungszustand ändert (Anmelden/Abmelden).
        /// Komponenten können sich daran registrieren, um UI zu aktualisieren.
        /// </summary>
        public event Action? AuthStateChanged;

        /// <summary>
        /// Meldet einen Benutzer mit der gegebenen Id an.
        /// </summary>
        /// <param name="id">Id des Benutzers.</param>
        public void SignIn(int id)
        {
            UserId = id;
            AuthStateChanged?.Invoke();
        }

        /// <summary>
        /// Meldet den aktuell angemeldeten Benutzer ab.
        /// </summary>
        public void SignOut()
        {
            UserId = null;
            AuthStateChanged?.Invoke();
        }
    }
}
