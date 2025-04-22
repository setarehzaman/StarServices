using STS.Domain.Core.Contracts.AppService;
using STS.Domain.Core.Contracts.Service;
using STS.Domain.Core.Dtos.Category;
using STS.Domain.Core.Entities;
using STS.Domain.Core.Entities.Category;

namespace STS.Domain.AppService.Category;
public class MainCategoryAppService(IMainCategoryService categoryService, 
    IBaseDataService baseDataService) : IMainCategoryAppService
{
    public async Task<Result<MainCategory>> Add(MainCategoryDto model, CancellationToken cancellationToken)
    {
        return await categoryService.Add(model, cancellationToken);
    }

    public async Task<Result<bool>> Delete(int id, CancellationToken cancellationToken)
    {
        return await categoryService.Delete(id, cancellationToken);
    }

    public async Task<Result<IEnumerable<MainCategoryDto>>> GetAll(CancellationToken cancellationToken)
    {
        return await categoryService.GetAll(cancellationToken);
    }

    public async Task<Result<MainCategoryDto>> GetBy(int id, CancellationToken cancellationToken)
    {
        return await categoryService.GetBy(id, cancellationToken);    
    }

    public async Task<Result<bool>> SetImgPath(int mainCategoryId, string ImgPath, CancellationToken cancellationToken)
    {
        return await categoryService.SetImgPath(mainCategoryId, ImgPath, cancellationToken);  
    }

    public async Task<Result<bool>> SoftDelete(int mainCategoryId, CancellationToken cancellationToken)
    {
        return await categoryService.SoftDelete(mainCategoryId, cancellationToken);
    }

    public async Task<Result<bool>> Update(MainCategoryDto model, CancellationToken cancellationToken)
    {
        string folderImagesPath = "/Images/MainCategories";

        if (model.MainCategoryImg is not null)
        {
            model.ImgPath = await baseDataService.UploadImage(model.MainCategoryImg!, folderImagesPath, cancellationToken);
        }

        return await categoryService.Update(model, cancellationToken);
    }
}
