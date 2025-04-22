using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using STS.Domain.Core.Contracts.AppService;
using STS.Domain.Core.Contracts.Infrastructure;
using STS.Domain.Core.Contracts.Service;
using STS.Domain.Core.Dtos.OrderDtos;
using STS.Domain.Core.Entities;
using STS.Domain.Core.Entities.Feature;
using STS.Domain.Core.Enums;

namespace STS.Domain.AppService.Feature;
public class OrderAppService(IOrderService orderService,
    IBaseDataService baseDataService, ILogger<OrderAppService> logger , ISmsService smsService) : IOrderAppService
{
    public async Task<Result<bool>> Accept(int orderId, CancellationToken cancellationToken)
        => await orderService.Accept(orderId, cancellationToken);
    public async Task<Result<Order>> Add(AddOrderDto model, List<IFormFile> images, CancellationToken cancellationToken)
    {
        var orderResult = await orderService.Add(model, cancellationToken);

        if (orderResult.IsSuccess)
        {
            var addresses = await baseDataService
                .UploadImages(images, "/UserTemplate/orders", cancellationToken);

            await baseDataService.AddOrderImages(addresses, orderResult.Data!.Id, cancellationToken);

            return new Result<Order>() { IsSuccess = true, Data = orderResult.Data };
        }
        else
        {
            return new Result<Order>() { IsSuccess = false, Message = "ثبت درخواست با مشکل مواجه شد." };
        }
    }

    public async Task AutoReject(CancellationToken cancellationToken)
    {
        var orderResult = await orderService.GetExpiredOrders(cancellationToken);

        if (orderResult.IsSuccess) 
        {
            if(orderResult.Data!.Any())
            {
                await orderService.ChageStatusToExpired(orderResult.Data!.Select(x=>x.OrderId), cancellationToken);
            }

            foreach(var order in orderResult.Data!)
            {
                if(!string.IsNullOrEmpty(order.PhoneNumber))
                {
                    await smsService.Send(order.PhoneNumber!, $"{order.FullName} {Environment.NewLine}" +
                        $"سرویس {order.TaskName} به دلیل ثبت نشدن پیشنهاد منقضی شد.");
                }
            }
        }
    }

    public async Task<Result<bool>> ChangeStatus(int orderId, OrderStatusEnum newStatus, CancellationToken cancellationToken)
    {
        var result = await orderService.GetBy(orderId, cancellationToken);
        var order = result.Data;

        if (result.Data is null)
        {
            return new Result<bool> { IsSuccess = false, Message = "سفارش مورد نظر یافت نشد", Data = false };

        }
        var isTransitionValid = await IsTransitionValid(order!.Id, order.OrderStatus, newStatus, cancellationToken);

        if (isTransitionValid is false)
        {
            logger.LogInformation("تلاش برای تغییر وضعیت سفارش، ناموفق توسط ادمین");

            return new Result<bool> { IsSuccess = false, Message = "متاسفانه این عملیات قابل انجام نیست", Data = false };
        }

        var savingResult = await orderService.ChangeStatus(orderId, newStatus, cancellationToken);

        if (!savingResult.IsSuccess)
        {
            return new Result<bool> { IsSuccess = false, Message = "در ذخیره تغییراتی خطایی رخ داد دوباره تلاش کنید", Data = false };
        }

        return new Result<bool> { IsSuccess = true, Message = "عملیات با موفقیت انجام شد", Data = true };
    }
    public async Task<Result<IEnumerable<OrderDto>>> GetAll(CancellationToken cancellationToken)
        => await orderService.GetAll(cancellationToken);
    public async Task<Result<IEnumerable<OrderDto>>> GetAllByClientId(int clientId, CancellationToken cancellationToken)
    {
        var orderResult = await orderService.GetAllByClientId(clientId, cancellationToken);

        if (orderResult.Data is null || !orderResult.Data.Any()) return orderResult;

        foreach (var order in orderResult.Data)
        {
            order.HasAcceptedSuggestion = order.Suggestions.Any(x => x.IsAccepted);
            order.Suggestions = order.Suggestions.OrderByDescending(x => x.IsAccepted).ToList();
        }

        return orderResult;
    }
    public async Task<Result<IEnumerable<OrderStatus>>> GetAllOrderStatuses(CancellationToken cancellationToken)
    {
        return await orderService.GetAllOrderStatuses(cancellationToken);
    }
    public async Task<Result<OrderDto>> GetBy(int id, CancellationToken cancellationToken)
        => await orderService.GetBy(id, cancellationToken);
    public async Task<Result<bool>> Reject(int orderId, CancellationToken cancellationToken)
        => await orderService.Reject(orderId, cancellationToken);
    private async Task<bool> IsTransitionValid(int orderId, OrderStatusEnum currentStatus, OrderStatusEnum newStatus, CancellationToken cancellationToken)
    {
        var result = await orderService.HasAnySuggestion(orderId, cancellationToken);

        switch (currentStatus)
        {
            case OrderStatusEnum.AwaitingSuggestions:
                if (result.Data)
                {
                    return newStatus == OrderStatusEnum.SelectingExpert;
                }
                return false;

            case OrderStatusEnum.SelectingExpert:
                if (!result.Data)
                {
                    return newStatus == OrderStatusEnum.AwaitingSuggestions;
                }
                return false;

            case OrderStatusEnum.ExpertEnRoute:
                return false;

            case OrderStatusEnum.JobInProgress:
                return newStatus == OrderStatusEnum.JobCompleted;

            case OrderStatusEnum.JobCompleted:
                return newStatus == OrderStatusEnum.Paid;

            case OrderStatusEnum.Paid:
                return false;

            default:
                return false;
        }
    }
}
