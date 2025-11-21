using System.Security.Claims;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Components.Authorization;
using ITP2Tree.Data;

namespace ITP2Tree.Services
{
    public class CustomAuthStateProvider : AuthenticationStateProvider
    {
        private readonly AuthService _authService;
        private readonly AppDBContext _db;

        public CustomAuthStateProvider(AuthService authService, AppDBContext db)
        {
            _authService = authService;
            _db = db;
            _authService.AuthStateChanged += NotifyAuthStateChanged;
        }

        private void NotifyAuthStateChanged()
        {
            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
        }

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
