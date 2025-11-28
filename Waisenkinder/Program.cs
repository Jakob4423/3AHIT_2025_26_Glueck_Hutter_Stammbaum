using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using ITP2Tree.Data;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.EntityFrameworkCore;

namespace ITP2Tree
{
    // DTOs for temporary test endpoints
    public record CreateUserDto(string Email, string Name, string Password);
    public record CreatePersonDto(int UserId, string Name, string Geburtsort, string Geburtsdatum, string Verwandte, string? Notizen);

    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services for Blazor Server
            builder.Services.AddRazorPages();
            builder.Services.AddServerSideBlazor();

            // Configure DbContext - use SQLite file in app folder
            builder.Services.AddDbContext<AppDBContext>(options =>
                options.UseSqlite("Data Source=waisen.db"));

            // Simple auth state for demo (scoped per connection)
            builder.Services.AddScoped<ITP2Tree.Services.AuthService>();
            // Authorization and a custom provider backed by our AuthService
            builder.Services.AddAuthorizationCore();
            builder.Services.AddScoped<AuthenticationStateProvider, ITP2Tree.Services.CustomAuthStateProvider>();

            var app = builder.Build();

            // Ensure database created
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

            // Temporary test API endpoints for automated verification
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

            app.MapGet("/api/test/list-persons/{userId:int}", async (AppDBContext db, int userId) =>
            {
                var list = await db.Personen.Where(p => p.BenutzerId == userId).ToListAsync();
                return Results.Ok(list);
            });

            app.MapBlazorHub();
            app.MapFallbackToPage("/_Host");

            // DTOs are declared at namespace scope above

            app.Run();
        }
    }
}
