using STS.Domain.Core.Dtos.ExpertDtos;
using STS.Domain.Core.Dtos.OrderDtos;
using STS.Domain.Core.Entities;

namespace STS.Domain.Core.Contracts.AppService;
public interface IExpertAppService 
{
    Task<Result<ExpertDto>> GetBy(int id, CancellationToken cancellationToken);
    Task<Result<bool>> UpdateSkills(int expertId, List<int> skillIds, CancellationToken cancellationToken);
    Task<Result<IEnumerable<OrderDto>>> GetOrdersBasedOnSkills(int expertId, CancellationToken cancellationToken);
    Task<Result<IEnumerable<OrderDto>>> GetOrdersPerSkill(int taskId,int expertId, CancellationToken cancellationToken);
}
