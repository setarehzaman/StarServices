using STS.Domain.Core.Dtos.ExpertDtos;
using STS.Domain.Core.Entities;

namespace STS.Domain.Core.Contracts.Service;
public interface IExpertService
{
    Task<Result<ExpertDto>> GetBy(int id, CancellationToken cancellationToken);
    Task<Result<bool>> UpdateSkills(int expertId, List<int> skillIds, CancellationToken cancellationToken);
    Task<Result<IEnumerable<int>>> GetSkillIdsBy(int expertId, CancellationToken cancellationToken);
}
