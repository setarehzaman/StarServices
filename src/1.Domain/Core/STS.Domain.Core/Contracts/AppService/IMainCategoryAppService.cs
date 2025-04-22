using STS.Domain.Core.Dtos.Category;
using STS.Domain.Core.Entities;
using STS.Domain.Core.Entities.Category;

namespace STS.Domain.Core.Contracts.AppService;
public interface IMainCategoryAppService
{
    Task<Result<MainCategory>> Add(MainCategoryDto model, CancellationToken cancellationToken);
    Task<Result<IEnumerable<MainCategoryDto>>> GetAll(CancellationToken cancellationToken);
    Task<Result<MainCategoryDto>> GetBy(int id, CancellationToken cancellationToken);
    Task<Result<bool>> Update(MainCategoryDto model, CancellationToken cancellationToken);
    Task<Result<bool>> Delete(int id, CancellationToken cancellationToken);
    Task<Result<bool>> SoftDelete(int mainCategoryId, CancellationToken cancellationToken);
    Task<Result<bool>> SetImgPath(int mainCategoryId, string ImgPath, CancellationToken cancellationToken);
}
