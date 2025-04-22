using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using STS.Domain.Core.Entities.Feature;
namespace STS.Infrastructure.SqlServer.Configurations.Feature;
public class OrderConfigs : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.HasKey(x => x.Id);

        builder.HasOne(x => x.Client)
            .WithMany(x => x.Orders)
            .HasForeignKey(x => x.ClientId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(x => x.Task)
            .WithMany(x => x.Orders)
            .HasForeignKey(x => x.TaskId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}
