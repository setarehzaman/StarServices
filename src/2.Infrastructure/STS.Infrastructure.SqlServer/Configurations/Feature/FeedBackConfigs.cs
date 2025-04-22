using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using STS.Domain.Core.Entities.Feature;
namespace STS.Infrastructure.SqlServer.Configurations.Feature;
public class FeedBackConfigs : IEntityTypeConfiguration<FeedBack>
{
    public void Configure(EntityTypeBuilder<FeedBack> builder)
    {
        builder.HasKey(x => x.Id);

        builder.HasOne(x => x.Client)
            .WithMany(x => x.FeedBacks)
            .HasForeignKey(x => x.ClientId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(x => x.Expert)
            .WithMany(x => x.FeedBacks)
            .HasForeignKey(x => x.ExpertId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}
