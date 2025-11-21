namespace ITP2Tree.Services
{
    public class AuthService
    {
        public int? UserId { get; private set; }

        // Raised when the authentication state changes (sign in / sign out)
        public event Action? AuthStateChanged;

        public void SignIn(int id)
        {
            UserId = id;
            AuthStateChanged?.Invoke();
        }

        public void SignOut()
        {
            UserId = null;
            AuthStateChanged?.Invoke();
        }
    }
}
