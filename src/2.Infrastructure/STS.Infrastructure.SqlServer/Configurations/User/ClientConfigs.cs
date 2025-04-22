using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using STS.Domain.Core.Entities.User;
namespace STS.Infrastructure.SqlServer.Configurations.User;
public class ClientConfigs : IEntityTypeConfiguration<Client>
{
    public void Configure(EntityTypeBuilder<Client> builder)
    {
        builder.HasKey(x => x.Id);

        builder.HasMany(x => x.Orders)
            .WithOne(x => x.Client)
            .HasForeignKey(x => x.ClientId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(x => x.ApplicationUser).WithOne(x => x.Client);
    }
}
