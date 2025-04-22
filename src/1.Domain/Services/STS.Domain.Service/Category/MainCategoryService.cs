using STS.Domain.Core.Contracts.Repository;
using STS.Domain.Core.Contracts.Service;
using STS.Domain.Core.Dtos.Category;
using STS.Domain.Core.Entities;
using STS.Domain.Core.Entities.Category;

namespace STS.Domain.Service.Category;
public class MainCategoryService(IMainCategoryRepository categoryRepository, IBaseDataService baseDataService) : IMainCategoryService
{
    public async Task<Result<MainCategory>> Add(MainCategoryDto model, CancellationToken cancellationToken)
    {
        string folderImagesPath = "/Images/MainCategories";

        var fileAddress = await baseDataService.UploadImage(model.MainCategoryImg!, folderImagesPath, cancellationToken);

        var mainCategory = new MainCategory()
        {
            Id = model.Id,
            ImgPath = fileAddress,
            Name = model.Name,
        };

        return await categoryRepository.Add(mainCategory, cancellationToken);
    }

    public async Task<Result<IEnumerable<MainCategoryDto>>> GetAll(CancellationToken cancellationToken)
    {
        return await categoryRepository.GetAll(cancellationToken);
    }

    public async Task<Result<MainCategoryDto>> GetBy(int id, CancellationToken cancellationToken)
    {
        return await categoryRepository.GetBy(id, cancellationToken);
    }

    public async Task<Result<bool>> Update(MainCategoryDto model, CancellationToken cancellationToken)
    {
        return await categoryRepository.Update(model, cancellationToken);
    }

    public async Task<Result<bool>> Delete(int id, CancellationToken cancellationToken)
    {
        return await categoryRepository.Delete(id, cancellationToken);
    }

    public async Task<Result<bool>> SoftDelete(int mainCategoryId, CancellationToken cancellationToken)
    {
        return await categoryRepository.SoftDelete(mainCategoryId, cancellationToken);
    }

    public async Task<Result<bool>> SetImgPath(int mainCategoryId, string imgPath, CancellationToken cancellationToken)
    {
        return await categoryRepository.SetImgPath(mainCategoryId, imgPath, cancellationToken);
    }
}
