namespace STS.Domain.Core.Dtos.ClientDtos;

public class ClientDto
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? CardNumber { get; set; }
    public int CityId { get; set; }
    public double Balance { get; set; }
    public string? PhoneNumber { get; set; }
    public string? Address { get; set; }
    public string? ImageProfile { get; set; }
    public string? Email { get; set; }
    public string? UserName { get; set; }
    public List<Order>? Orders { get; set; }
}   