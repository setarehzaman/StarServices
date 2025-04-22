
using Microsoft.AspNetCore.Http;
using STS.Domain.Core.Entities.BaseEntities;
using STS.Domain.Core.Entities.Category;
using System.Security.Principal;

namespace STS.Domain.Core.Dtos.Category;

public class SubCategoryDto : BaseEntity
{
    public string? ImgPath { get; set; }
    public int MainCategoryId { get; set; }
    public string MainCategoryName { get; set; }
    public IFormFile? SubCategoryImg { get; set; }

}
