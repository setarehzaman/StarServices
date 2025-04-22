using STS.Domain.Core.Contracts.Repository;
using STS.Domain.Core.Contracts.Service;
using STS.Domain.Core.Dtos.Category;
using STS.Domain.Core.Entities;
using STS.Domain.Core.Entities.Category;


namespace STS.Domain.Service.Category;
public class SubCategoryService(ISubCategoryRepository subCategoryRepository) : ISubCategoryService
{
    public async Task<Result<SubCategory>> Add(SubCategoryDto model, CancellationToken cancellationToken)
    {
        return await subCategoryRepository.Add(model, cancellationToken);
    }

    public async Task<Result<IEnumerable<SubCategoryDto>>> GetAll(CancellationToken cancellationToken)
    {
        return await subCategoryRepository.GetAll(cancellationToken);
    }

    public async Task<Result<IEnumerable<SubCategoryDto>>> GetAllByMainCategoryId(int id, CancellationToken cancellationToken)
    {
        return await subCategoryRepository.GetAllByMainCategoryId(id, cancellationToken);
    }

    public async Task<Result<SubCategoryDto>> GetBy(int id, CancellationToken cancellationToken)
    {
        return await subCategoryRepository.GetBy(id, cancellationToken);
    }

    public async Task<Result<bool>> Update(SubCategoryDto model, CancellationToken cancellationToken)
    {
        return await subCategoryRepository.Update(model, cancellationToken);
    }

    public async Task<Result<bool>> Delete(int id, CancellationToken cancellationToken)
    {
        return await subCategoryRepository.Delete(id, cancellationToken);
    }

    public async Task<Result<bool>> SoftDelete(int subCategoryId, CancellationToken cancellationToken)
    {
        return await subCategoryRepository.SoftDelete(subCategoryId, cancellationToken);
    }
}
