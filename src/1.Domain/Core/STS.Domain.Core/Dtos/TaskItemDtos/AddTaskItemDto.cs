namespace STS.Domain.Core.Dtos.TaskItemDtos;

public class AddTaskItemDto
{
    public string? Name { get; set; }
    public int SubCategoryId { get; set; }
    public string? ImgPath { get; set; }
    public int BasePrice { get; set; }
}