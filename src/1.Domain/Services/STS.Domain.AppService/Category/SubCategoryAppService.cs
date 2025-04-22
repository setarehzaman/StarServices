using STS.Domain.Core.Contracts.AppService;
using STS.Domain.Core.Contracts.Service;
using STS.Domain.Core.Dtos.Category;
using STS.Domain.Core.Entities;
using STS.Domain.Core.Entities.Category;

namespace STS.Domain.AppService.Category;
public class SubCategoryAppService(ISubCategoryService subCategoryService,
    IBaseDataService baseDataService) : ISubCategoryAppService
{
    public async Task<Result<SubCategory>> Add(SubCategoryDto model, CancellationToken cancellationToken)
    {
        return await subCategoryService.Add(model, cancellationToken);
    }

    public async Task<Result<bool>> Delete(int id, CancellationToken cancellationToken)
    {
        return await subCategoryService.Delete(id, cancellationToken);
    }

    public async Task<Result<IEnumerable<SubCategoryDto>>> GetAll(CancellationToken cancellationToken)
    {
        return await subCategoryService.GetAll(cancellationToken);
    }

    public async Task<Result<IEnumerable<SubCategoryDto>>> GetAllByMainCategoryId(int id, CancellationToken cancellationToken)
    {
        return await subCategoryService.GetAllByMainCategoryId(id, cancellationToken);
    }

    public async Task<Result<SubCategoryDto>> GetBy(int id, CancellationToken cancellationToken)
    {
        return await subCategoryService.GetBy(id, cancellationToken);
    }
    public async Task<Result<bool>> SoftDelete(int subCategoryId, CancellationToken cancellationToken)
    {
        return await subCategoryService.SoftDelete(subCategoryId, cancellationToken); 
    }

    public async Task<Result<bool>> Update(SubCategoryDto model, CancellationToken cancellationToken)
    {
        string folderImagesPath = "/Images/SubCategories";

        if (model.SubCategoryImg is not null)
        {
            model.ImgPath = await baseDataService.UploadImage(model.SubCategoryImg!, folderImagesPath, cancellationToken);
        }

        return await subCategoryService.Update(model, cancellationToken);
    }
}
