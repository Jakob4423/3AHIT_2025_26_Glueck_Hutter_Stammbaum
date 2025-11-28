using Microsoft.EntityFrameworkCore;

namespace ITP2Tree.Data
{
    /// <summary>
    /// Entity Framework Core Datenkontext der Anwendung.
    /// </summary>
    /// <remarks>
    /// Definiert die DbSets für <see cref="Benutzer"/> und <see cref="Person"/>
    /// und konfiguriert einfache Constraints (z. B. eindeutige E-Mail, Cascade Delete).
    /// </remarks>
    public class AppDBContext : DbContext
    {
        /// <summary>
        /// Konstruktor: initialisiert den DbContext mit Options.
        /// </summary>
        public AppDBContext(DbContextOptions<AppDBContext> options) : base(options) { }

        /// <summary>
        /// DbSet für Benutzer (Accounts).
        /// </summary>
        public DbSet<Benutzer> Benutzer => Set<Benutzer>();

        /// <summary>
        /// DbSet für Personen, die Benutzer zugeordnet sind.
        /// </summary>
        public DbSet<Person> Personen => Set<Person>();

        /// <summary>
        /// Model-Konfiguration: Indices, Beziehungen, Delete-Verhalten.
        /// </summary>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Benutzer>()
                .HasIndex(b => b.Email)
                .IsUnique();

            modelBuilder.Entity<Person>()
                .HasOne(p => p.Benutzer)
                .WithMany(b => b.Personen)
                .HasForeignKey(p => p.BenutzerId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
