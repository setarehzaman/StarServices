using STS.Domain.Core.Contracts.Repository;
using STS.Domain.Core.Contracts.Service;
using STS.Domain.Core.Dtos.OrderDtos;
using STS.Domain.Core.Entities;
using STS.Domain.Core.Entities.Feature;
using STS.Domain.Core.Enums;

namespace STS.Domain.Service.Feature;
public class OrderService(IOrderRepository orderRepository) : IOrderService
{
    public async Task<Result<Order>> Add(AddOrderDto model, CancellationToken cancellationToken)
    {
        return await orderRepository.Add(model, cancellationToken);
    }

    public async Task<Result<bool>> Update(UpdateOrderDto model, CancellationToken cancellationToken)
    {
        return await orderRepository.Update(model, cancellationToken);
    }

    public async Task<Result<bool>> Delete(int id, CancellationToken cancellationToken)
    {
        return await orderRepository.Delete(id, cancellationToken);
    }

    public async Task<Result<OrderDto>> GetBy(int id, CancellationToken cancellationToken)
    {
        return await orderRepository.GetBy(id, cancellationToken);
    }

    public async Task<Result<IEnumerable<OrderDto>>> GetAll(CancellationToken cancellationToken)
    {
        return await orderRepository.GetAll(cancellationToken);
    }

    public async Task<Result<IEnumerable<OrderDto>>> GetAllByClientId(int clientId, CancellationToken cancellationToken)
    {
        return await orderRepository.GetAllByClientId(clientId, cancellationToken);
    }

    public async Task<Result<bool>> ChangeStatus(int orderId, OrderStatusEnum status, CancellationToken cancellationToken)
        => await orderRepository.ChangeStatus(orderId, status, cancellationToken);

    public async Task<Result<bool>> Accept(int orderId, CancellationToken cancellationToken)
    {
        return await orderRepository.Accept(orderId, cancellationToken);
    }

    public async Task<Result<bool>> Reject(int orderId, CancellationToken cancellationToken)
    {
        return await orderRepository.Reject(orderId, cancellationToken);
    }
    public async Task<Result<bool>> HasAnySuggestion(int orderId, CancellationToken cancellationToken)
        => await orderRepository.HasAnySuggestion(orderId, cancellationToken);

    public async Task<Result<IEnumerable<OrderStatus>>> GetAllOrderStatuses(CancellationToken cancellationToken)
        => await orderRepository.GetAllOrderStatuses(cancellationToken);

    public async Task<Result<IEnumerable<OrderDto>>> GetAllBy(List<int> taskIds, int expertId, CancellationToken cancellationToken)
        => await orderRepository.GetAllBy(taskIds, expertId, cancellationToken);

    public async Task<Result<IEnumerable<GetExpiredOrderDto>>> GetExpiredOrders(CancellationToken cancellationToken)
        => await orderRepository.GetExpiredOrders(cancellationToken);

    public async Task<Result<bool>> ChageStatusToExpired(IEnumerable<int> ids, CancellationToken cancellationToken)
        => await orderRepository.ChageStatusToExpired(ids, cancellationToken);
}
