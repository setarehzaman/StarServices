namespace STS.Domain.Core.Dtos.ClientDtos;

public class UpdateClientDto
{
    public int Id { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? CardNumber { get; set; }
    public int CityId { get; set; }
    public double Balance { get; set; }
    public string? PhoneNumber { get; set; }
    public List<Order>? Orders { get; set; }
    public string? Address { get; set; }
    public string? ImageProfile { get; set; }
}
