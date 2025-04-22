using STS.Domain.Core.Contracts.Repository;
using STS.Domain.Core.Contracts.Service;
using STS.Domain.Core.Dtos.ExpertDtos;
using STS.Domain.Core.Entities;

namespace STS.Domain.Service.User;
public class ExpertService(IExpertRepository expertRepository) : IExpertService
{
    public async Task<Result<ExpertDto>> GetBy(int id, CancellationToken cancellationToken)
        => await expertRepository.GetBy(id, cancellationToken);

    public async Task<Result<IEnumerable<int>>> GetSkillIdsBy(int expertId, CancellationToken cancellationToken)
        => await expertRepository.GetSkillIdsBy(expertId, cancellationToken);

    public async Task<Result<bool>> UpdateSkills(int expertId, List<int> skillIds, CancellationToken cancellationToken)
        => await expertRepository.UpdateSkills(expertId, skillIds, cancellationToken);
}
