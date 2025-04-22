using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using STS.Domain.Core.Entities.Feature;

namespace STS.Infrastructure.SqlServer.Configurations.Feature;

public class SuggestionConfigs : IEntityTypeConfiguration<Suggestion>
{
    public void Configure(EntityTypeBuilder<Suggestion> builder)
    {
        builder.HasKey(x => x.Id);

        builder.HasOne(x => x.Expert)
            .WithMany(x => x.Suggestions)
            .HasForeignKey(x => x.ExpertId)
            .OnDelete(DeleteBehavior.NoAction);


    }
}
