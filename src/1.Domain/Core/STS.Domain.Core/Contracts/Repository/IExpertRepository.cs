using STS.Domain.Core.Dtos.ExpertDtos;
using STS.Domain.Core.Entities;
using STS.Domain.Core.Entities.User;
namespace STS.Domain.Core.Contracts.Repository;
public interface IExpertRepository
{
    Task<Result<ApplicationUser>> Add(AddExpertDto model, CancellationToken cancellationToken);
    Task<Result<bool>> Update(UpdateExpertDto model, CancellationToken cancellationToken);
    Task<Result<bool>> Delete(int id, CancellationToken cancellationToken);
    Task<Result<ExpertDto>> GetBy(int id, CancellationToken cancellationToken);
    Task<Result<IEnumerable<ExpertDto>>> GetAll(CancellationToken cancellationToken);
    Task<Result<bool>> ChangePassword(int expertId, string newPassword, CancellationToken cancellationToken);
    Task<Result<bool>> UpdateSkills(int expertId, List<int> skillIds, CancellationToken cancellationToken);
    Task<Result<IEnumerable<int>>> GetSkillIdsBy(int expertId, CancellationToken cancellationToken);

}