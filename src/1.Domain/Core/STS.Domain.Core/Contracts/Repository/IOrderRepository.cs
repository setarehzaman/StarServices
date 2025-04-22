using STS.Domain.Core.Dtos.OrderDtos;
using STS.Domain.Core.Entities;
using STS.Domain.Core.Entities.Feature;
using STS.Domain.Core.Enums;

namespace STS.Domain.Core.Contracts.Repository;
public interface IOrderRepository
{
    Task<Result<Order>> Add(AddOrderDto model, CancellationToken cancellationToken);
    Task<Result<bool>> Update(UpdateOrderDto model, CancellationToken cancellationToken);
    Task<Result<bool>> Delete(int id, CancellationToken cancellationToken);
    Task<Result<OrderDto>> GetBy(int id, CancellationToken cancellationToken);
    Task<Result<IEnumerable<OrderDto>>> GetAll(CancellationToken cancellationToken);
    Task<Result<IEnumerable<OrderDto>>> GetAllBy(List<int> taskIds, int expertId, CancellationToken cancellationToken);
    Task<Result<IEnumerable<OrderDto>>> GetAllByClientId(int clientId, CancellationToken cancellationToken);
    Task<Result<bool>> ChangeStatus(int orderId, OrderStatusEnum status, CancellationToken cancellationToken);
    Task<Result<bool>> Accept(int orderId, CancellationToken cancellationToken);
    Task<Result<bool>> Reject(int orderId, CancellationToken cancellationToken);
    Task<Result<bool>> HasAnySuggestion(int orderId, CancellationToken cancellationToken);
    Task<Result<IEnumerable<OrderStatus>>> GetAllOrderStatuses(CancellationToken cancellationToken);
    Task<Result<IEnumerable<GetExpiredOrderDto>>> GetExpiredOrders(CancellationToken cancellationToken);
    Task<Result<bool>> ChageStatusToExpired(IEnumerable<int> ids, CancellationToken cancellationToken);
}
