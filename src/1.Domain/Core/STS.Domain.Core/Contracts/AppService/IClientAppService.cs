using STS.Domain.Core.Entities;

namespace STS.Domain.Core.Contracts.AppService;
public interface IClientAppService
{
    Task<Result<bool>> PayForSuggestion(int orderId, int expertId, int clientId, int userId, CancellationToken cancellationToken);
}
