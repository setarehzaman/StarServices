using STS.Domain.Core.Dtos.Category;
using STS.Domain.Core.Entities;
using STS.Domain.Core.Entities.Category;

namespace STS.Domain.Core.Contracts.Repository;
public interface ISubCategoryRepository
{
    Task<Result<SubCategory>> Add(SubCategoryDto model, CancellationToken cancellationToken);
    Task<Result<IEnumerable<SubCategoryDto>>> GetAll(CancellationToken cancellationToken);
    Task<Result<IEnumerable<SubCategoryDto>>> GetAllByMainCategoryId(int id, CancellationToken cancellationToken);
    Task<Result<SubCategoryDto>> GetBy(int id, CancellationToken cancellationToken);
    Task<Result<bool>> Update(SubCategoryDto model, CancellationToken cancellationToken);
    Task<Result<bool>> Delete(int id, CancellationToken cancellationToken);
    Task<Result<bool>> SoftDelete(int subCategoryId, CancellationToken cancellationToken);
}
