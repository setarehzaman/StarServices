using Microsoft.AspNetCore.Http;
using STS.Domain.Core.Entities.BaseEntities;
using STS.Domain.Core.Entities.Category;
namespace STS.Domain.Core.Dtos.TaskItemDtos;

public class TaskItemDto : BaseEntity
{
    public int? SubCategoryId { get; set; }   
    public string? SubCategoryName { get; set; }
    public string? ImgPath { get; set; }
    public IFormFile? TaskItemImg { get; set; }
    public int BasePrice { get; set; }
}
