using Microsoft.AspNetCore.Http;
using STS.Domain.Core.Entities.BaseEntities;
using STS.Domain.Core.Entities.Category;
using STS.Domain.Core.Entities.Feature;
namespace STS.Domain.Core.Dtos.Category;
public class MainCategoryDto : BaseEntity
{
    public string ImgPath { get; set; }
    public IFormFile? MainCategoryImg { get; set; }
}
