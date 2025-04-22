using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using STS.Domain.Core.Entities.Feature;
namespace STS.Infrastructure.SqlServer.Configurations.Feature;
public class TaskItemConfigs : IEntityTypeConfiguration<TaskItem>
{
    public void Configure(EntityTypeBuilder<TaskItem> builder)
    {
        builder.HasKey(x => x.Id);

        #region SeedData
        builder.HasData(
               new TaskItem { Id = 1, SubCategoryId = 1, Name = "کاشی و سرامیک", ImgPath = "/Images/TaskItems/2ce018e0-9826-4003-bcfa-3ddacc7d37b0.webp", BasePrice = 5000000 },
               new TaskItem { Id = 2, SubCategoryId = 1, Name = "بنایی ساختمان", ImgPath = "/Images/TaskItems/e7472819-17a4-4d71-827a-89289c031373.webp", BasePrice = 4000000 },
               new TaskItem { Id = 3, SubCategoryId = 1, Name = "گچ کاری", ImgPath = "/Images/TaskItems/34b130ba-5bf6-4ca4-8d34-f8071d707ff8.webp", BasePrice = 6000000 },
               new TaskItem { Id = 4, SubCategoryId = 1, Name = "کارگر ساده", ImgPath = "/Images/TaskItems/35de9364-d86b-4fdb-8758-31e89a7305f5.webp", BasePrice = 2500000 },
               new TaskItem { Id = 5, SubCategoryId = 1, Name = "بازسازی", ImgPath = "/Images/TaskItems/2cd703db-36aa-4921-9f12-0033d6b44ffe.webp", BasePrice = 30000000 },
               new TaskItem { Id = 6, SubCategoryId = 1, Name = "کانال سازی و دریچه کولر", ImgPath = "/Images/TaskItems/b6b376ac-3bcf-48a3-b055-6a7c20dd4dd0.webp", BasePrice = 5000000 },
               new TaskItem { Id = 7, SubCategoryId = 1, Name = "عایق کاری و ایزوگام", ImgPath = "/Images/TaskItems/dac1bb2d-374d-4ef9-8de2-bb2f1082714b.webp", BasePrice = 6800000 },
               new TaskItem { Id = 8, SubCategoryId = 1, Name = "سنگ کاری", ImgPath = "/Images/TaskItems/a264b5eb-c52a-42d0-b70b-db5091869be7.webp", BasePrice = 30000000 },
               new TaskItem { Id = 9, SubCategoryId = 1, Name = "سیمان کاری", ImgPath = "/Images/TaskItems/a845202d-032c-406a-b8c9-b07217340951.webp", BasePrice = 30000000 },
               new TaskItem { Id = 10, SubCategoryId = 2, Name = "نقاشی ساختمان", ImgPath = "/Images/TaskItems/f172da2f-9444-4891-b693-03099e238fbc.webp", BasePrice = 30000000 },
               new TaskItem { Id = 11, SubCategoryId = 2, Name = "کابینت", ImgPath = "/Images/TaskItems/1498af45-eef9-4b71-a733-42a6b62b3ee7.webp", BasePrice = 30000000 },
               new TaskItem { Id = 12, SubCategoryId = 2, Name = "کاغذ دیواری", ImgPath = "/Images/TaskItems/a836a67f-da40-4a4c-905e-48bf386d4726.webp", BasePrice = 30000000 },
               new TaskItem { Id = 13, SubCategoryId = 2, Name = "نجاری", ImgPath = "/Images/TaskItems/ea35c1c1-c87e-403a-811f-a04ed5da8224.webp", BasePrice = 30000000 },
               new TaskItem { Id = 14, SubCategoryId = 2, Name = "کفسابی", ImgPath = "/Images/TaskItems/8dfe930c-1ed7-4592-855e-252c3105e237.webp", BasePrice = 30000000 },
               new TaskItem { Id = 15, SubCategoryId = 2, Name = "کفپوش", ImgPath = "/Images/TaskItems/d6724f46-cb4c-4f4f-bd3f-2c1fec484043.webp", BasePrice = 30000000 },
               new TaskItem { Id = 16, SubCategoryId = 2, Name = "پارکت", ImgPath = "/Images/TaskItems/4407bd89-224a-4967-bef7-39e337936d23.webp", BasePrice = 30000000 },
               new TaskItem { Id = 17, SubCategoryId = 2, Name = "لمینت", ImgPath = "/Images/TaskItems/844dedd7-0876-4a81-bc4c-ff6beddafa06.webp", BasePrice = 30000000 },
               new TaskItem { Id = 18, SubCategoryId = 2, Name = "موکت", ImgPath = "/Images/TaskItems/7a30dcfa-8d95-4759-84fb-2b211ac4b4ed.webp", BasePrice = 30000000 },
               new TaskItem { Id = 19, SubCategoryId = 2, Name = "دوخت پرده", ImgPath = "/Images/TaskItems/ed828d72-5110-40b3-8a53-be2f8ed4bb7f.webp", BasePrice = 30000000 },
               new TaskItem { Id = 20, SubCategoryId = 2, Name = "مبلمان", ImgPath = "/Images/TaskItems/4d79e2a7-edac-473c-9753-543225e70346.webp", BasePrice = 30000000 },
               new TaskItem { Id = 21, SubCategoryId = 2, Name = "سرویس خواب", ImgPath = "/Images/TaskItems/329f3061-42b5-428f-b793-5f6362631cc4.webp", BasePrice = 30000000 },
               new TaskItem { Id = 22, SubCategoryId = 2, Name = "سقف کاذب", ImgPath = "/Images/TaskItems/30fda66e-b645-46bf-8f50-135ab117e695.webp", BasePrice = 30000000 },
               new TaskItem { Id = 23, SubCategoryId = 3, Name = "نمای ساختمان", ImgPath = "/Images/TaskItems/8e0d0856-315f-43f3-907a-3c2271f10628.webp", BasePrice = 30000000 },
               new TaskItem { Id = 24, SubCategoryId = 3, Name = "تعمیر نمای ساختمان", ImgPath = "/Images/TaskItems/3e8e8c0f-99eb-44d1-ab2a-b1db73e666bb.webp", BasePrice = 30000000 },
               new TaskItem { Id = 25, SubCategoryId = 4, Name = "شیشه بری", ImgPath = "/Images/TaskItems/ff709f66-fbb0-47e9-a622-106e4a1b0d03.webp", BasePrice = 30000000 },
               new TaskItem { Id = 26, SubCategoryId = 4, Name = "توری پنجره", ImgPath = "/Images/TaskItems/554b399f-6347-40c6-aaae-a11d173766ad.webp", BasePrice = 30000000 },
               new TaskItem { Id = 27, SubCategoryId = 4, Name = "نصب درب چوبی", ImgPath = "/Images/TaskItems/6d3e0935-6c7c-4a7b-92c8-bff55e6ddcbe.webp", BasePrice = 30000000 },
               new TaskItem { Id = 28, SubCategoryId = 5, Name = "جوشکاری و آهنگری", ImgPath = "/Images/TaskItems/35cb61d4-637a-4cc7-96a2-34e39782bf96.webp", BasePrice = 30000000 },
               new TaskItem { Id = 29, SubCategoryId = 5, Name = "کلید سازی", ImgPath = "/Images/TaskItems/d01f38eb-92ed-4a75-93b1-e512ee485d93.webp", BasePrice = 30000000 },
               new TaskItem { Id = 30, SubCategoryId = 6, Name = "گل و گیاه آپارتمانی", ImgPath = "/Images/TaskItems/7b9f726c-0d1b-4d80-8bdd-1da5d4ddfeb6.webp", BasePrice = 30000000 },
               new TaskItem { Id = 31, SubCategoryId = 6, Name = "باغبانی", ImgPath = "/Images/TaskItems/b86725c4-2d69-4911-af66-74ba8eaf0a2c.webp", BasePrice = 30000000 },
               new TaskItem { Id = 32, SubCategoryId = 7, Name = "صافکاری و نقاشی خودرو", ImgPath = "/Images/TaskItems/7583b4a7-2ed5-4699-8bde-f416e78dd226.webp", BasePrice = 30000000 },
               new TaskItem { Id = 33, SubCategoryId = 7, Name = "تعویض روغن", ImgPath = "/Images/TaskItems/0258e8d5-9668-4893-8d15-0183145d0fab.webp", BasePrice = 30000000 },
               new TaskItem { Id = 34, SubCategoryId = 7, Name = "تعمیر خودرو", ImgPath = "/Images/TaskItems/454eca89-8ba7-4fbf-9270-2aeb3fc4879f.webp", BasePrice = 30000000 },
               new TaskItem { Id = 35, SubCategoryId = 7, Name = "برق خودرو", ImgPath = "/Images/TaskItems/7014ff29-f51c-4e56-83b7-32f78341c1e4.webp", BasePrice = 30000000 },
               new TaskItem { Id = 36, SubCategoryId = 8, Name = "اسباب کشی", ImgPath = "/Images/TaskItems/15b1016e-eede-448d-8a3e-c832030d4507.webp", BasePrice = 30000000 },
               new TaskItem { Id = 37, SubCategoryId = 9, Name = "حمل بار ", ImgPath = "/Images/TaskItems/d7946c6f-43bb-4ad5-8684-043e70fed7f7.webp", BasePrice = 30000000 },
               new TaskItem { Id = 38, SubCategoryId = 10, Name = "یخچال", ImgPath = "/Images/TaskItems/3a805751-f8ce-47fd-8e78-0429ee6a4c52.webp", BasePrice = 30000000 },
               new TaskItem { Id = 39, SubCategoryId = 10, Name = "ماشین ظرفشویی", ImgPath = "/Images/TaskItems/69c43dcd-d644-4156-8e0f-06002ad6b3e8.webp", BasePrice = 30000000 },
               new TaskItem { Id = 40, SubCategoryId = 10, Name = "مایکروفر", ImgPath = "/Images/TaskItems/83bdaeea-f908-405b-9208-eeddbed88c5e.webp", BasePrice = 30000000 },
               new TaskItem { Id = 41, SubCategoryId = 10, Name = "اجاق برقی", ImgPath = "/Images/TaskItems/4e58b3cb-36de-43a4-83e0-72632d1e1e86.webp", BasePrice = 30000000 },
               new TaskItem { Id = 42, SubCategoryId = 10, Name = "هود آشپزخانه", ImgPath = "/Images/TaskItems/4df97a51-4335-4863-86de-1cc0bee8356d.webp", BasePrice = 30000000 },
               new TaskItem { Id = 43, SubCategoryId = 10, Name = "اجاق گاز", ImgPath = "/Images/TaskItems/0b84d3f6-89d4-4873-9157-b2d9fc2f51c5.webp", BasePrice = 30000000 },
               new TaskItem { Id = 44, SubCategoryId = 11, Name = "ماشین لباسشویی", ImgPath = "/Images/TaskItems/aded05f5-8c4b-451b-b58f-d692c6e08d84.webp", BasePrice = 30000000 },
               new TaskItem { Id = 45, SubCategoryId = 11, Name = "اتو بخار", ImgPath = "/Images/TaskItems/7f227268-ba59-457b-afcb-dd76a256d07d.webp", BasePrice = 30000000 },
               new TaskItem { Id = 46, SubCategoryId = 11, Name = "اتو پرس", ImgPath = "/Images/TaskItems/35bc08ad-7f9c-4a10-8ea6-c5f9edf9ea0e.webp", BasePrice = 30000000 },
               new TaskItem { Id = 47, SubCategoryId = 11, Name = "جاروبرقی", ImgPath = "/Images/TaskItems/d04efd6a-2c16-4e57-afb1-cdaaccf91427.webp", BasePrice = 30000000 },
               new TaskItem { Id = 48, SubCategoryId = 11, Name = "جارو شارژی", ImgPath = "/Images/TaskItems/a01bef88-058c-4caf-ad4a-7fedcedf9ba6.webp", BasePrice = 30000000 },
               new TaskItem { Id = 49, SubCategoryId = 12, Name = "تلویزیون", ImgPath = "/Images/TaskItems/4c7b8c4e-c292-486c-bf1a-0d15e551c0e8.webp", BasePrice = 30000000 },
               new TaskItem { Id = 50, SubCategoryId = 12, Name = "سینما خانگی", ImgPath = "/Images/TaskItems/cb580eff-96fb-4ecd-bc3a-39e43c9e7062.webp", BasePrice = 30000000 },
               new TaskItem { Id = 51, SubCategoryId = 13, Name = "دستگاه کپی", ImgPath = "/Images/TaskItems/587fb9ab-11ca-4e29-a6f2-0404ca1c1ef2.webp", BasePrice = 30000000 },
               new TaskItem { Id = 52, SubCategoryId = 13, Name = "فکس", ImgPath = "/Images/TaskItems/88e26199-0184-4b06-865b-dfab22950800.webp", BasePrice = 30000000 },
               new TaskItem { Id = 53, SubCategoryId = 13, Name = "پرینتر", ImgPath = "/Images/TaskItems/7296d9ec-b48d-4225-bb36-23ceb5d75888.webp", BasePrice = 30000000 },
               new TaskItem { Id = 54, SubCategoryId = 14, Name = "پارتیشن اداری", ImgPath = "/Images/TaskItems/15dd3bcd-8c99-496c-b53d-a4c6be1f7cf1.webp", BasePrice = 30000000 },
               new TaskItem { Id = 55, SubCategoryId = 15, Name = "نظافت دوره ای", ImgPath = "/Images/TaskItems/d35afbab-349a-40a5-9920-c7aaf0088bf8.webp", BasePrice = 30000000 },
               new TaskItem { Id = 56, SubCategoryId = 15, Name = "نظافت منزل", ImgPath = "/Images/TaskItems/53b6758b-6df7-4149-a1fb-179f8c14fe69.webp", BasePrice = 30000000 },
               new TaskItem { Id = 57, SubCategoryId = 15, Name = "نظافت ساختمان", ImgPath = "/Images/TaskItems/428cfcf0-6cbb-4fc5-a425-24950f13c60d.webp", BasePrice = 30000000 },
               new TaskItem { Id = 58, SubCategoryId = 15, Name = "نظافت شرکت و اداره", ImgPath = "/Images/TaskItems/12b6bd85-b5fb-4a6e-b3ef-6f4a7393601d.webp", BasePrice = 30000000 },
               new TaskItem { Id = 59, SubCategoryId = 15, Name = "ضدعفونی منزل و محل کار", ImgPath = "/Images/TaskItems/eb16cbf2-f8ef-4bcc-99fc-1ce8a42b7f5f.webp", BasePrice = 30000000 },
               new TaskItem { Id = 60, SubCategoryId = 16, Name = "خشکشویی آنلاین", ImgPath = "/Images/TaskItems/6e672608-99a6-4f78-88b1-380fc3a884ba.webp", BasePrice = 30000000 },
               new TaskItem { Id = 61, SubCategoryId = 16, Name = "خشکشویی پرده", ImgPath = "/Images/TaskItems/a08223f4-a911-41d9-9efc-6173f7771021.webp", BasePrice = 30000000 },
               new TaskItem { Id = 62, SubCategoryId = 17, Name = "قالیشویی آنلاین", ImgPath = "/Images/TaskItems/65dc2285-4c94-4a31-aa6e-bb8000fc9402.webp", BasePrice = 30000000 },
               new TaskItem { Id = 63, SubCategoryId = 17, Name = "مبل شویی", ImgPath = "/Images/TaskItems/3e8bf209-ea00-40a5-992c-b5e360bab4fb.webp", BasePrice = 30000000 },
               new TaskItem { Id = 64, SubCategoryId = 18, Name = "سمپاشی منازل", ImgPath = "/Images/TaskItems/2fd6426a-ee1a-4bf3-befb-0fa5f3c17885.webp", BasePrice = 30000000 }



               //new TaskItem { Id = 97, SubCategoryId = 23, Name = "تعمیر موبایل", ImgPath = "958a54ca-c84f-47f2-8b66-7b84de6d9d2d.webp", BasePrice = 30000000 },
               //new TaskItem { Id = 98, SubCategoryId = 24, Name = "تعمیر لپ‌تاپ", ImgPath = "8ddec483-9b8d-4fc9-8a39-987a4ab03873.webp", BasePrice = 30000000 },
               //new TaskItem { Id = 99, SubCategoryId = 24, Name = "تعمیر سخت افزار کامپیوتر", ImgPath = "c068e5dd-bcaf-4b83-b9dd-9bfbb060c4e7.webp", BasePrice = 30000000 },
               //new TaskItem { Id = 100, SubCategoryId = 24, Name = "نصب نرم افزار", ImgPath = "f7fb5686-f130-4f13-86a0-b6f79d0ce790.webp", BasePrice = 30000000 },
               //new TaskItem { Id = 101, SubCategoryId = 24, Name = "نصب ویندوز در محل", ImgPath = "f2d25592-d8c4-4430-badc-839a2fa9eff0.webp", BasePrice = 30000000 },
               //new TaskItem { Id = 102, SubCategoryId = 25, Name = "تعمیر مودم اینترنت", ImgPath = "7eeea6b2-0a32-4aa5-8244-7f84bcfe200e.webp", BasePrice = 30000000 },
               //new TaskItem { Id = 103, SubCategoryId = 25, Name = "راه‌ اندازی شبکه کامپیوتری", ImgPath = "6d22ccda-5eda-48dd-bb26-4095c48447af.webp", BasePrice = 30000000 },
               //new TaskItem { Id = 104, SubCategoryId = 26, Name = "آزمایش در محل", ImgPath = "c214ac59-9605-48b4-95bf-1b12a79d870c.webp", BasePrice = 30000000 },
               //new TaskItem { Id = 105, SubCategoryId = 26, Name = "پرستاری در منزل", ImgPath = "9d49d3fc-88fe-4fd8-bd12-eba51013a8d6.webp", BasePrice = 30000000 },
               //new TaskItem { Id = 106, SubCategoryId = 26, Name = "ویزیت پزشک در منزل", ImgPath = "3f4df8f1-7e51-4d57-b98a-568f1eef5396.webp", BasePrice = 30000000 },
               //new TaskItem { Id = 107, SubCategoryId = 26, Name = "نوار قلب در محل", ImgPath = "f73d4d27-3e0b-422e-bad2-deebd9d0467d.webp", BasePrice = 30000000 },
               //new TaskItem { Id = 108, SubCategoryId = 26, Name = "فیزیوتراپی در منزل", ImgPath = "64bd6636-1560-4dd6-9499-c72dde33b07e.webp", BasePrice = 30000000 }

           );
        #endregion SeedData
    }
}
