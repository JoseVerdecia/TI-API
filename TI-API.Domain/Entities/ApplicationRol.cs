using Microsoft.AspNetCore.Identity;

namespace TI_API.Domain.Entities
{
    public class ApplicationRol : IdentityRole<Guid>
    {
        public ApplicationRol() : base() { }

        public ApplicationRol(string roleName) : base(roleName) { }
    }
}
