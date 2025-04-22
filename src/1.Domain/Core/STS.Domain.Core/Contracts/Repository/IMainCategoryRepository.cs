using STS.Domain.Core.Dtos.Category;
using STS.Domain.Core.Entities;
using STS.Domain.Core.Entities.Category;

namespace STS.Domain.Core.Contracts.Repository;
public interface IMainCategoryRepository
{
    Task<Result<MainCategory>> Add(MainCategory model, CancellationToken cancellationToken);
    Task<Result<IEnumerable<MainCategoryDto>>> GetAll(CancellationToken cancellationToken);
    Task<Result<MainCategoryDto>> GetBy(int id, CancellationToken cancellationToken);
    Task<Result<bool>> Update(MainCategoryDto model, CancellationToken cancellationToken);
    Task<Result<bool>> Delete(int id, CancellationToken cancellationToken);
    Task<Result<bool>> SoftDelete(int mainCategoryId, CancellationToken cancellationToken);
    Task<Result<bool>> SetImgPath(int mainCategoryId, string imgPath, CancellationToken cancellationToken);
}
