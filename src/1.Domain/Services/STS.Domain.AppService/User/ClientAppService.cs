using STS.Domain.Core.Contracts.AppService;
using STS.Domain.Core.Contracts.Service;
using STS.Domain.Core.Entities;
using STS.Domain.Core.Entities.Configs;
using STS.Domain.Core.Enums;

namespace STS.Domain.AppService.User;
public class ClientAppService(ISuggestionService suggestionService, IApplicationUserService applicationUserService,
    SiteSettings settings, IOrderService orderService) : IClientAppService
{
    public async Task<Result<bool>> PayForSuggestion(int orderId, int expertId, int clientId, int userId, CancellationToken cancellationToken)
    {
        var suggestedPrice = await suggestionService.GetAcceptedSuggestionPrice(orderId, cancellationToken);
        var clientBalance = await applicationUserService.GetBalance(userId, cancellationToken);

        if (clientBalance.Data < suggestedPrice.Data)
        {
            return new Result<bool>()
            { IsSuccess = false, Data = false, Message = "موجودی حساب شما برای تکمیل کردن سفارش کافی نمی باشد." };
        }

        var expertUserId = await applicationUserService.GetUserIdByExpertId(expertId, cancellationToken);

        await applicationUserService.Withdraw(userId, suggestedPrice.Data, cancellationToken);

        var fee = suggestedPrice.Data * settings.FeeAmount;

        await applicationUserService.Deposit(expertUserId.Data, suggestedPrice.Data - fee, cancellationToken);

        await applicationUserService.Deposit(1, fee, cancellationToken);

        await orderService.ChangeStatus(orderId, OrderStatusEnum.Paid, cancellationToken);

        return new Result<bool>() { IsSuccess = true, Data = true, Message = "سفارش با موفقیت تکمیل شد" };
    }
}
