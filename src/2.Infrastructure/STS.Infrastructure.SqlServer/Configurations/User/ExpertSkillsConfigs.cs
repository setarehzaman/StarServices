using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using STS.Domain.Core.Entities.User;
namespace STS.Infrastructure.SqlServer.Configurations.User;
public class ExpertSkillsConfigs : IEntityTypeConfiguration<ExpertSkills>
{
    public void Configure(EntityTypeBuilder<ExpertSkills> builder)
    {
        builder.HasKey(x => new { x.ExpertId, x.TaskId });

        builder.HasOne(x => x.Expert)
            .WithMany(x => x.Skills)
            .HasForeignKey(x => x.ExpertId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(x => x.Task)
            .WithMany(x => x.Experts)
            .HasForeignKey(x => x.TaskId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}
