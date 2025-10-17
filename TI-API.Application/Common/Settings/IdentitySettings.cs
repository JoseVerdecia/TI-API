namespace TI_API.Application.Common.Settings
{
    public class IdentitySettings
    {
        public PasswordOptions Password { get; init; } = null!;
        public LockoutOptions Lockout { get; init; } = null!;
        public UserOptions User { get; init; } = null!;
        public SignInOptions SignIn { get; init; } = null!;
        public DefaultAdminOptions DefaultAdmin { get; init; } = null!;

        public class PasswordOptions
        {
            public bool RequireDigit { get; init; } = true;
            public bool RequireLowercase { get; init; } = true;
            public bool RequireUppercase { get; init; } = true;
            public bool RequireNonAlphanumeric { get; init; } = false;
            public int RequiredLength { get; init; } = 6;
        }

        public class LockoutOptions
        {
            public int DefaultLockoutTimeSpanInMinutes { get; init; } = 30;
            public int MaxFailedAccessAttempts { get; init; } = 5;
            public bool AllowedForNewUsers { get; init; } = true;
        }

        public class UserOptions
        {
            public bool RequireUniqueEmail { get; init; } = true;
        }

        public class SignInOptions
        {
            public bool RequireConfirmedEmail { get; init; } = false;
        }

        public class DefaultAdminOptions
        {
            public string Email { get; init; } = "admin@universidad.edu";
            public string Password { get; init; } = "Admin123!";
            public string Nombre { get; init; } = "Administrador General";
        }
    }
}
