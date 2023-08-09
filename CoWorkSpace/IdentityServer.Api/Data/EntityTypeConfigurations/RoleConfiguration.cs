using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IdentityServer.Api.Data.EntityTypeConfigurations
{
    public class RoleConfiguration: IEntityTypeConfiguration<IdentityRole>
    {
        public void Configure(EntityTypeBuilder<IdentityRole> builder) {
            builder.HasData(
                new IdentityRole { 
                       Name = "User",
                       NormalizedName = "USER"
                },
                new IdentityRole
                {
                    Name = "Admin",
                    NormalizedName = "ADMIN"
                }
            );
            
        }

    }
}
