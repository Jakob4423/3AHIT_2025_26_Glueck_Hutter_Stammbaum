using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using ITP2Tree.Data;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.EntityFrameworkCore;

namespace ITP2Tree
{
    /// <summary>
    /// Data Transfer Object für die Benutzer-Erstellung über Test-API.
    /// Wird verwendet, um neue Benutzer programmatisch zu erstellen.
    /// </summary>
    /// <remarks>
    /// Dieses DTO ist nur für Entwicklungs- und Test-Zwecke gedacht.
    /// In der Produktionsversion sollten diese Endpoints entfernt werden.
    /// </remarks>
    public record CreateUserDto(string Email, string Name, string Password);

    /// <summary>
    /// Data Transfer Object für die Person-Erstellung über Test-API.
    /// Wird verwendet, um neue Familienmitglieder programmatisch zu erstellen.
    /// </summary>
    /// <remarks>
    /// Dieses DTO ist nur für Entwicklungs- und Test-Zwecke gedacht.
    /// In der Produktionsversion sollten diese Endpoints entfernt werden.
    /// </remarks>
    public record CreatePersonDto(int UserId, string Name, string Geburtsort, string Geburtsdatum, string Verwandte, string? Notizen);

    /// <summary>
    /// Haupteinstiegspunkt der ASP.NET Core Blazor Server-Anwendung.
    /// Konfiguriert alle Services, Datenbanken und Middleware.
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Hauptmethode der Anwendung.
        /// Erstellt den WebApplication-Builder, konfiguriert alle Services
        /// und startet die Anwendung.
        /// </summary>
        /// <param name="args">Kommandozeilenargumente der Anwendung.</param>
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Blazor Server Komponenten und Razor Pages aktivieren
            builder.Services.AddRazorPages();
            builder.Services.AddServerSideBlazor();

            // EntityFramework Core mit SQLite konfigurieren
            // Datenbank wird in der lokalen Datei "waisen.db" gespeichert
            builder.Services.AddDbContext<AppDBContext>(options =>
                options.UseSqlite("Data Source=waisen.db"));

            // Authentifizierungs-Services konfigurieren
            // AuthService: In-Memory Session Management
            builder.Services.AddScoped<ITP2Tree.Services.AuthService>();
            // Authorization: ASP.NET Core Authorization Core
            builder.Services.AddAuthorizationCore();
            // CustomAuthStateProvider: Integration mit Blazor Authorization
            builder.Services.AddScoped<AuthenticationStateProvider, ITP2Tree.Services.CustomAuthStateProvider>();

            var app = builder.Build();

            // Stelle sicher, dass die Datenbank beim Appstart erstellt wird
            // Dies ist wichtig für die automatische Initialisierung bei der Entwicklung
            using (var scope = app.Services.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<AppDBContext>();
                db.Database.EnsureCreated();
            }

            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
            }

            app.UseStaticFiles();

            app.UseRouting();

            // Test-Endpoints für Entwicklung und Verifikation
            // Diese sollten vor der Produktionsfreigabe entfernt werden
            
            /// <summary>
            /// Test-Endpoint zum Erstellen eines neuen Benutzers.
            /// POST /api/test/create-user
            /// </summary>
            app.MapPost("/api/test/create-user", async (AppDBContext db, CreateUserDto dto) =>
            {
                var user = new Benutzer
                {
                    Email = dto.Email,
                    Name = dto.Name,
                    PasswortHash = BCrypt.Net.BCrypt.HashPassword(dto.Password)
                };
                db.Benutzer.Add(user);
                await db.SaveChangesAsync();
                return Results.Ok(new { user.Id, user.Email, user.Name });
            });

            /// <summary>
            /// Test-Endpoint zum Erstellen einer neuen Person.
            /// POST /api/test/create-person
            /// </summary>
            app.MapPost("/api/test/create-person", async (AppDBContext db, CreatePersonDto dto) =>
            {
                var person = new Person
                {
                    BenutzerId = dto.UserId,
                    Name = dto.Name,
                    Geburtsort = dto.Geburtsort,
                    Geburtsdatum = dto.Geburtsdatum,
                    Verwandte = dto.Verwandte,
                    Notizen = dto.Notizen
                };
                db.Personen.Add(person);
                await db.SaveChangesAsync();
                return Results.Ok(new { person.Id });
            });

            /// <summary>
            /// Test-Endpoint zum Abrufen aller Personen eines Benutzers.
            /// GET /api/test/list-persons/{userId}
            /// </summary>
            app.MapGet("/api/test/list-persons/{userId:int}", async (AppDBContext db, int userId) =>
            {
                var list = await db.Personen.Where(p => p.BenutzerId == userId).ToListAsync();
                return Results.Ok(list);
            });

            app.MapBlazorHub();
            app.MapFallbackToPage("/_Host");

            // Starte die Webanwendung und höre auf Anfragen
            app.Run();
        }
    }
}
