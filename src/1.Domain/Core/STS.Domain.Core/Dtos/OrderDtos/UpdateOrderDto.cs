using STS.Domain.Core.Enums;

namespace STS.Domain.Core.Dtos.OrderDtos;
public class UpdateOrderDto
{
    public int Id { get; set; }
    public double OrderedPrice { get; set; }
    public DateTime DoAt { get; set; }
    public OrderStatusEnum OrderStatus { get; set; }
}
