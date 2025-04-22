using Microsoft.AspNetCore.Http;
using STS.Domain.Core.Dtos.OrderDtos;
using STS.Domain.Core.Entities;
using STS.Domain.Core.Entities.Feature;
using STS.Domain.Core.Enums;

namespace STS.Domain.Core.Contracts.AppService;
public interface IOrderAppService
{
    Task<Result<Order>> Add(AddOrderDto model,List<IFormFile> images , CancellationToken cancellationToken);
    Task<Result<IEnumerable<OrderDto>>> GetAll(CancellationToken cancellationToken);
    Task<Result<IEnumerable<OrderDto>>> GetAllByClientId(int clientId, CancellationToken cancellationToken);
    Task<Result<OrderDto>> GetBy(int id, CancellationToken cancellationToken);
    Task<Result<bool>> Accept(int orderId, CancellationToken cancellationToken);
    Task<Result<bool>> Reject(int orderId, CancellationToken cancellationToken);
    Task<Result<bool>> ChangeStatus(int orderId, OrderStatusEnum newStatus, CancellationToken cancellationToken);
    Task<Result<IEnumerable<OrderStatus>>> GetAllOrderStatuses(CancellationToken cancellationToken);
    Task AutoReject(CancellationToken cancellationToken);
}
