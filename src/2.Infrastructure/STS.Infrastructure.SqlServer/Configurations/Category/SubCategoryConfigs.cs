using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using STS.Domain.Core.Entities.Category;
namespace STS.Infrastructure.SqlServer.Configurations.Category;
public class SubCategoryConfigs : IEntityTypeConfiguration<SubCategory>
{
    public void Configure(EntityTypeBuilder<SubCategory> builder)
    {
        builder.HasKey(x => x.Id);

        builder.HasMany(x => x.Tasks)
            .WithOne(x => x.SubCategory)
            .HasForeignKey(x => x.SubCategoryId)
            .OnDelete(DeleteBehavior.NoAction);

        #region SeedData
        builder.HasData(
               new SubCategory { Id = 1, MainCategoryId = 1, Name = "بنایی" },
               new SubCategory { Id = 2, MainCategoryId = 1, Name = "دکوراسیون" },
               new SubCategory { Id = 3, MainCategoryId = 1, Name = "نقاشی ساختمان" },
               new SubCategory { Id = 4, MainCategoryId = 1, Name = "درب و پنجره" },
               new SubCategory { Id = 5, MainCategoryId = 1, Name = "آهنگری و جوشکاری" },
               new SubCategory { Id = 6, MainCategoryId = 1, Name = "باغبانی" },
               new SubCategory { Id = 7, MainCategoryId = 2, Name = "خودرو" },
               new SubCategory { Id = 8, MainCategoryId = 3, Name = "اسباب کشی" },
               new SubCategory { Id = 9, MainCategoryId = 3, Name = "حمل بار" },
               new SubCategory { Id = 10, MainCategoryId = 4, Name = "لوازم آشپزخانه" },
               new SubCategory { Id = 11, MainCategoryId = 4, Name = "لوازم شست و شو و نظافت" },
               new SubCategory { Id = 12, MainCategoryId = 4, Name = "لوازم صوتی و تصویری" },
               new SubCategory { Id = 13, MainCategoryId = 5, Name = "ماشین اداری" },
               new SubCategory { Id = 14, MainCategoryId = 5, Name = "مبلمان اداری" },
               new SubCategory { Id = 15, MainCategoryId = 6, Name = "نظافت" },
               new SubCategory { Id = 16, MainCategoryId = 6, Name = "خشکشویی و اتوشویی" },
               new SubCategory { Id = 17, MainCategoryId = 6, Name = "قالیشویی و مبل شویی" },
               new SubCategory { Id = 18, MainCategoryId = 6, Name = "سمپاشی" }

               //new SubCategory { Id = 23, MainCategoryId = 8, Name = "موبایل و تبلت" },
               //new SubCategory { Id = 24, MainCategoryId = 8, Name = "خدمات کامپیوتری" },
               //new SubCategory { Id = 25, MainCategoryId = 8, Name = "امنیت و شبکه" },
               //new SubCategory { Id = 26, MainCategoryId = 9, Name = "پزشکی" }

               );
        #endregion
    }
}
