using Microsoft.EntityFrameworkCore;

namespace ITP2Tree.Data
{
    public class AppDBContext : DbContext
    {
        public AppDBContext(DbContextOptions<AppDBContext> options) : base(options) { }

        public DbSet<Benutzer> Benutzer => Set<Benutzer>();
        public DbSet<Person> Personen => Set<Person>();

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
