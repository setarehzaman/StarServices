namespace STS.Domain.Core.Dtos.OrderDtos;

public class AddOrderDto
{
    public double OrderedPrice { get; set; }
    public int TaskId { get; set; }
    public int ClientId { get; set; }
    public DateTime DoAt { get; set; }
    public string? Description { get; set; }
}
