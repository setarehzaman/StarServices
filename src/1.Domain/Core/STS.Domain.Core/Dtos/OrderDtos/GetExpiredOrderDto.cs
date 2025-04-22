namespace STS.Domain.Core.Dtos.OrderDtos;
public class GetExpiredOrderDto
{
    public int OrderId { get; set; }
    public string TaskName { get; set; }
    public string? PhoneNumber { get; set; }
    public string? FullName { get; set; }
}