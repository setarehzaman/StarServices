
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using STS.Domain.Core.Entities.Category;
namespace STS.Infrastructure.SqlServer.Configurations.Category;
public class MainCategoryConfigs : IEntityTypeConfiguration<MainCategory>
{
    public void Configure(EntityTypeBuilder<MainCategory> builder)
    {
        builder.HasKey(x => x.Id);

        builder.HasMany(x => x.SubCategories)
            .WithOne(x => x.MainCategory)
            .HasForeignKey(x => x.MainCategoryId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasData(
            new MainCategory { Id = 1, Name = "دکوراسیون ساختمان", ImgPath = "/Images/MainCategories/dekorasion.png" },
            new MainCategory { Id = 2, Name = "وسایل نقلیه", ImgPath = "/Images/MainCategories/vasayelnaghlie.png" },
            new MainCategory { Id = 3, Name = "اسباب کشی و باربری", ImgPath = "/Images/MainCategories/asaskeshi.png" },
            new MainCategory { Id = 4, Name = "لوازم خانگی", ImgPath = "/Images/MainCategories/lavazemkhanegi.png" },
            new MainCategory { Id = 5, Name = "خدمات اداری", ImgPath = "/Images/MainCategories/edari.png" },
            new MainCategory { Id = 6, Name = "نظافت و بهداشت", ImgPath = "/Images/MainCategories/nezafat.png" }

            //new MainCategory { Id = 8, Name = "دیجیتال و نرم افزار", ImgPath = "/Images/MainCategories/99535998-ca15-4f98-801e-5777ad46f663digital.png" },
            //new MainCategory { Id = 9, Name = "پزشکی و سلامت", ImgPath = "59e143b3-669e-431a-9a3a-306457529e4c.svg" }
        );

    }
}
