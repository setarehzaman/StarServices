﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using STS.Domain.Core.Entities.BaseEntities;
namespace STS.Infrastructure.SqlServer.Configurations.BaseData;
public class CityConfigs : IEntityTypeConfiguration<City>
{
    public void Configure(EntityTypeBuilder<City> builder)
    {
        builder.HasKey(x => x.Id);

        builder.HasMany(x => x.Users).WithOne(x => x.City)
            .HasForeignKey(x => x.CityId)
            .OnDelete(DeleteBehavior.NoAction);

        #region SeedData
        builder.HasData(
        new City { Id = 1, Name = "آذربایجان شرقی" },
        new City { Id = 2, Name = "آذربایجان غربی" },
        new City { Id = 3, Name = "اردبیل" },
        new City { Id = 4, Name = "اصفهان" },
        new City { Id = 5, Name = "البرز" },
        new City { Id = 6, Name = "ایلام" },
        new City { Id = 7, Name = "بوشهر" },
        new City { Id = 8, Name = "تهران" },
        new City { Id = 9, Name = "چهارمحال و بختیاری" },
        new City { Id = 10, Name = "خراسان جنوبی" },
        new City { Id = 11, Name = "خراسان رضوی" },
        new City { Id = 12, Name = "خراسان شمالی" },
        new City { Id = 13, Name = "خوزستان" },
        new City { Id = 14, Name = "زنجان" },
        new City { Id = 15, Name = "سمنان" },
        new City { Id = 16, Name = "سیستان و بلوچستان" },
        new City { Id = 17, Name = "فارس" },
        new City { Id = 18, Name = "قزوین" },
        new City { Id = 19, Name = "قم" },
        new City { Id = 20, Name = "کردستان" },
        new City { Id = 21, Name = "کرمان" },
        new City { Id = 22, Name = "کرمانشاه" },
        new City { Id = 23, Name = "کهگیلویه و بویراحمد" },
        new City { Id = 24, Name = "گلستان" },
        new City { Id = 25, Name = "گیلان" },
        new City { Id = 26, Name = "لرستان" },
        new City { Id = 27, Name = "مازندران" },
        new City { Id = 28, Name = "مرکزی" },
        new City { Id = 29, Name = "هرمزگان" },
        new City { Id = 30, Name = "همدان" },
        new City { Id = 31, Name = "یزد" }
        );
        #endregion
    }
}
