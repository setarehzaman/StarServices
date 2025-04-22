using STS.Domain.Core.Contracts.AppService;
using STS.Domain.Core.Contracts.Service;
using STS.Domain.Core.Dtos.OrderDtos;
using STS.Domain.Core.Dtos.SuggestionDtos;
using STS.Domain.Core.Entities;
using STS.Domain.Core.Entities.Feature;
using STS.Domain.Core.Enums;

namespace STS.Domain.AppService.Feature;
public class SuggestionAppService(ISuggestionService suggestionService, IOrderService orderService) : ISuggestionAppService
{
    public async Task<Result<bool>> Accept(int suggestionId, CancellationToken cancellationToken)
        => await suggestionService.Accept(suggestionId, cancellationToken);

    public async Task<Result<Suggestion>> Add(AddSuggestionDto model, CancellationToken cancellationToken)
    {
        var result = await suggestionService.Add(model, cancellationToken);

        if (result.IsSuccess)
        {
            return new Result<Suggestion> { IsSuccess = true, Message = "پیشنهاد شما با موفقیت ثبت شد" };
        }
        else
        {
            return new Result<Suggestion> { IsSuccess = false, Message = "خطایی در سیستم رخ داده است" };
        }
    }

    public async Task<Result<IEnumerable<SuggestionDto>>> GetAllBy(int expertId, CancellationToken cancellationToken)
        => await suggestionService.GetAllBy(expertId, cancellationToken);

    public async Task<Result<IEnumerable<SuggestionDto>>> GetAllSuggestionsPerOrder(int orderId, CancellationToken cancellationToken)
        => await suggestionService.GetAllSuggestionsPerOrder(orderId, cancellationToken);

    public async Task<Result<bool>> Reject(int suggestionId, CancellationToken cancellationToken)
        => await suggestionService.Reject(suggestionId, cancellationToken);

    public async Task<Result<bool>> SubmitSuggestion(int suggestionId, int orderId, CancellationToken cancellationToken)
    {
        try
        {
            await suggestionService.Accept(suggestionId, cancellationToken);

            var orderResult = await orderService.GetBy(orderId, cancellationToken);
            var order = orderResult.Data!;

            var updatingOrder = new UpdateOrderDto()
            {
                Id = orderId,
                OrderStatus = OrderStatusEnum.JobInProgress,
                OrderedPrice = order.OrderedPrice,
                DoAt = order.DoAt,
            };

            var result = await orderService.Update(updatingOrder, cancellationToken);

            if (!result.IsSuccess)
            {
                return new Result<bool> { IsSuccess = false, Message = "سیستم با خطایی مواجه شد مجددا تلاش کنید" };
            }
            else
            {
                return new Result<bool> { IsSuccess = true, Message = "این پیشنهاد با موفقیت برای سفارش شما ثبت شد", Data = true };
            }

        }
        catch (Exception e)
        {

            return new Result<bool> { IsSuccess = false, Message = e.Message };
        }
    }


}
