using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using STS.Domain.Core.Entities.BaseEntities;
namespace STS.Infrastructure.SqlServer.Configurations.BaseData;

public class PictureConfigs : IEntityTypeConfiguration<Picture>
{
    public void Configure(EntityTypeBuilder<Picture> builder)
    {
        builder.HasKey(x => x.Id);

        builder.HasOne(x => x.Order)
            .WithMany(x => x.Pictures)
            .HasForeignKey(x => x.OrderId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}
