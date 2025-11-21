using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using ITP2Tree.Data;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.EntityFrameworkCore;

namespace ITP2Tree
{
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

            app.MapBlazorHub();
            app.MapFallbackToPage("/_Host");

            app.Run();
        }
    }
}
