using System.Security.Claims;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Components.Authorization;
using ITP2Tree.Data;

namespace ITP2Tree.Services
{
    /// <summary>
    /// Custom AuthenticationStateProvider, der den <see cref="AuthService"/> und die DB verwendet,
    /// um den aktuellen AuthenticationState für Blazor-Komponenten bereitzustellen.
    /// </summary>
    public class CustomAuthStateProvider : AuthenticationStateProvider
    {
        private readonly AuthService _authService;
        private readonly AppDBContext _db;

        /// <summary>
        /// Konstruktor: injiziert den AuthService und den DB-Kontext.
        /// Registriert sich beim AuthStateChanged-Event, damit Blazor benachrichtigt wird.
        /// </summary>
        public CustomAuthStateProvider(AuthService authService, AppDBContext db)
        {
            _authService = authService;
            _db = db;
            _authService.AuthStateChanged += NotifyAuthStateChanged;
        }

        /// <summary>
        /// Ruft Blazor auf, dass sich der AuthenticationState geändert hat.
        /// </summary>
        private void NotifyAuthStateChanged()
        {
            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
        }

        /// <summary>
        /// Liefert den aktuellen <see cref="AuthenticationState"/> basierend auf dem AuthService.
        /// - Falls kein Benutzer angemeldet ist, wird ein anonymer Principal zurückgegeben.
        /// - Falls ein Benutzer vorhanden ist, werden Claims mit Id und E-Mail erzeugt.
        /// </summary>
        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            if (_authService.UserId == null)
            {
                var anonymous = new ClaimsPrincipal(new ClaimsIdentity());
                return await Task.FromResult(new AuthenticationState(anonymous));
            }

            var user = await _db.Benutzer.FindAsync(_authService.UserId.Value);
            if (user == null)
            {
                var anonymous = new ClaimsPrincipal(new ClaimsIdentity());
                return await Task.FromResult(new AuthenticationState(anonymous));
            }

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Email ?? string.Empty)
            };

            var identity = new ClaimsIdentity(claims, "CustomAuth");
            var principal = new ClaimsPrincipal(identity);

            return await Task.FromResult(new AuthenticationState(principal));
        }
    }
}
