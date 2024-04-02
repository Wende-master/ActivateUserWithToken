using ActivateUserWithToken.Models;
using Microsoft.EntityFrameworkCore;

namespace ActivateUserWithToken.Data
{
    public class UserContext : DbContext
    {
        public UserContext(DbContextOptions<UserContext> options) : base(options) { }

        public DbSet<Usuario> Usuarios { get; set; }

    }
}
