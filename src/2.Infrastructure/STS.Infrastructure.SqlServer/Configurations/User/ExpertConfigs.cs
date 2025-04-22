using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using STS.Domain.Core.Entities.User;
namespace STS.Infrastructure.SqlServer.Configurations.User;
public class ExpertConfigs : IEntityTypeConfiguration<Expert>
{
    public void Configure(EntityTypeBuilder<Expert> builder)
    {
        builder.HasKey(x => x.Id);

        builder.HasOne(x => x.ApplicationUser).WithOne(x => x.Expert);
    }
}
