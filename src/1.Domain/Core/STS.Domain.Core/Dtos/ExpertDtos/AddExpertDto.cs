namespace STS.Domain.Core.Dtos.ExpertDtos;

public class AddExpertDto
{
    public string? UserName { get; set; }
    public string? Email { get; set; }
    public string? Password { get; set; }
    public int CityId { get; set; }
}