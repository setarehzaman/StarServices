using Microsoft.EntityFrameworkCore.Query.Internal;
using STS.Domain.Core.Contracts.AppService;
using STS.Domain.Core.Contracts.Service;
using STS.Domain.Core.Dtos.ExpertDtos;
using STS.Domain.Core.Dtos.OrderDtos;
using STS.Domain.Core.Entities;

namespace STS.Domain.AppService.User;
public class ExpertAppService(IExpertService expertService, IOrderService orderService) : IExpertAppService
{
    public async Task<Result<ExpertDto>> GetBy(int id, CancellationToken cancellationToken)
        => await expertService.GetBy(id, cancellationToken);

    public async Task<Result<IEnumerable<OrderDto>>> GetOrdersBasedOnSkills(int expertId, CancellationToken cancellationToken)
    {
        var skillIdsResult = await expertService.GetSkillIdsBy(expertId, cancellationToken);

        var orders = await orderService.GetAllBy(skillIdsResult.Data.ToList(), expertId, cancellationToken);

        return orders;
    }

    public async Task<Result<IEnumerable<OrderDto>>> GetOrdersPerSkill(int taskId, int expertId, CancellationToken cancellationToken)
    {
        var orders = await orderService.GetAllBy(new List<int> { taskId }, expertId, cancellationToken);

        return orders;
    }

    public Task<Result<bool>> UpdateSkills(int expertId, List<int> skillIds, CancellationToken cancellationToken)
        => expertService.UpdateSkills(expertId, skillIds, cancellationToken);
}
