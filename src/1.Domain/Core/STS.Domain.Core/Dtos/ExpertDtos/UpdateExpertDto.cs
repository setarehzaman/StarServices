using STS.Domain.Core.Entities.User;

namespace STS.Domain.Core.Dtos.ExpertDtos;

public class UpdateExpertDto
{
    public int Id { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? CardNumber { get; set; }
    public int CityId { get; set; }
    public string? Biography { get; set; }
    public double Balance { get; set; }
    public string? PhoneNumber { get; set; }
    public List<ExpertSkills>? Skills { get; set; }
    public string? Address { get; set; }
    public string? ImageProfile { get; set; }
}