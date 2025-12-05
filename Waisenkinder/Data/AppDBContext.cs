using Microsoft.EntityFrameworkCore;
//Hutter
namespace ITP2Tree.Data
{
    /// <summary>
    /// Entity Framework Core Datenkontext der Anwendung.
    /// </summary>
    /// <remarks>
    /// Definiert die DbSets für <see cref="Benutzer"/>, <see cref="Person"/> und <see cref="Verwandtschaft"/>
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
        /// DbSet für Verwandtschaftsbeziehungen.
        /// </summary>
        public DbSet<Verwandtschaft> Verwandtschaften => Set<Verwandtschaft>();

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

            // Verwandtschaftsbeziehungen konfigurieren
            modelBuilder.Entity<Verwandtschaft>()
                .HasOne(v => v.PersonA)
                .WithMany(p => p.VerwandtschaftenAlsQuelle)
                .HasForeignKey(v => v.PersonAId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Verwandtschaft>()
                .HasOne(v => v.PersonB)
                .WithMany(p => p.VerwandtschaftenAlsZiel)
                .HasForeignKey(v => v.PersonBId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}

