using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using IdentityServer.Api.Enitities;
using Microsoft.EntityFrameworkCore;
using IdentityServer.Api.Data.EntityTypeConfigurations;

namespace IdentityServer.Api.Data
{
    public class ApplicationContext : IdentityDbContext<User>
    {
        public DbSet<RefreshToken> RefreshTokens { get; set; }
        public ApplicationContext( DbContextOptions options) : base(options) {
            
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfiguration(new RoleConfiguration());
            builder.ApplyConfiguration(new UserConfiguration());
        }
    }
}
