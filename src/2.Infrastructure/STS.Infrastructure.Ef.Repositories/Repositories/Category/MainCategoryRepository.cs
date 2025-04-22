using Microsoft.EntityFrameworkCore;
using STS.Domain.Core.Contracts.Repository;
using STS.Domain.Core.Dtos.Category;
using STS.Domain.Core.Entities;
using STS.Domain.Core.Entities.Category;
using STS.Infrastructure.Cache.Redis;
using STS.Infrastructure.SqlServer.Common;

namespace STS.Infrastructure.Ef.Repositories.Repositories.Category;
public class MainCategoryRepository(AppDbContext dbContext, IRedisCacheServices cacheServices) : IMainCategoryRepository
{
    public async Task<Result<IEnumerable<MainCategoryDto>>> GetAll(CancellationToken cancellationToken)
    {
        try
        {
            var cacheData = cacheServices.Get<Result<IEnumerable<MainCategoryDto>>>(CacheKeys.GetAllMainCategories);
            if (cacheData is not null) return cacheData;

            var entity = await dbContext.MainCategories
                .AsNoTracking()
                .Select(x => new MainCategoryDto()
                {
                    Id = x.Id,
                    Name = x.Name,
                    ImgPath = x.ImgPath,
                })
                .ToListAsync(cancellationToken);

            var result = new Result<IEnumerable<MainCategoryDto>>() { IsSuccess = true, Data = entity };

            cacheServices.SetSliding(CacheKeys.GetAllMainCategories, result, 500);

            return result;

        }
        catch (Exception e)
        {
            return new Result<IEnumerable<MainCategoryDto>> { IsSuccess = false, Message = e.Message };
        }
    }
    public async Task<Result<MainCategory>> Add(MainCategory model, CancellationToken cancellationToken)
    {
        try
        {
            await dbContext.MainCategories.AddAsync(model, cancellationToken);
            await dbContext.SaveChangesAsync(cancellationToken);

            var result = new Result<MainCategory>() { IsSuccess = true, Data = model };
            return result;
        }
        catch (Exception e)
        {
            return new Result<MainCategory> { IsSuccess = false, Message = e.Message };
        }
    }
    public async Task<Result<bool>> Delete(int id, CancellationToken cancellationToken)
    {
        try
        {
            var entity = await dbContext
                .MainCategories.FirstAsync(x => x.Id == id, cancellationToken);

            dbContext.MainCategories.Remove(entity);
            await dbContext.SaveChangesAsync(cancellationToken);

            var result = new Result<bool>() { IsSuccess = true };
            return result;
        }
        catch (Exception e)
        {
            return new Result<bool> { IsSuccess = false, Message = e.Message };
        }

    }
    public async Task<Result<MainCategoryDto>> GetBy(int id, CancellationToken cancellationToken)
    {
        try
        {
            var entity = await dbContext.MainCategories
                .AsNoTracking()
                .Where(x => x.Id == id)
                .Select(x => new MainCategoryDto()
                {
                    Id = x.Id,
                    Name = x.Name,
                    ImgPath = x.ImgPath,

                })
                .FirstAsync(cancellationToken);

            var result = new Result<MainCategoryDto>() { IsSuccess = true, Data = entity };
            return result;
        }
        catch (Exception e)
        {
            return new Result<MainCategoryDto> { IsSuccess = false, Message = e.Message };
        }

    }
    public async Task<Result<bool>> Update(MainCategoryDto model, CancellationToken cancellationToken)
    {
        try
        {
            var entity = await dbContext
                .MainCategories.FirstAsync(x => x.Id == model.Id, cancellationToken);

            entity!.Name = model.Name;
            entity.ImgPath = model.ImgPath is null ? entity.ImgPath : model.ImgPath;

            dbContext.MainCategories.Update(entity);
            await dbContext.SaveChangesAsync(cancellationToken);

            var result = new Result<bool>() { IsSuccess = true };
            return result;
        }
        catch (Exception e)
        {
            return new Result<bool> { IsSuccess = false, Message = e.Message };
        }
    }
    public async Task<Result<bool>> SoftDelete(int mainCategoryId, CancellationToken cancellationToken)
    {
        try
        {
            var entity = await dbContext
                .MainCategories
                .FirstAsync(x => x.Id == mainCategoryId, cancellationToken);

            entity.SoftDelete = true;

            await dbContext.SaveChangesAsync(cancellationToken);

            var result = new Result<bool>() { IsSuccess = true };
            return result;
        }
        catch (Exception e)
        {
            return new Result<bool> { IsSuccess = false, Message = e.Message };
        }
    }
    public async Task<Result<bool>> SetImgPath(int mainCategoryId, string imgPath, CancellationToken cancellationToken)
    {
        try
        {
            var entity = await dbContext
                .MainCategories
                .FirstAsync(x => x.Id == mainCategoryId, cancellationToken);

            entity.ImgPath = imgPath;

            await dbContext.SaveChangesAsync(cancellationToken);
            return new Result<bool> { IsSuccess = true };
        }
        catch (Exception e)
        {
            return new Result<bool> { IsSuccess = false, Message = e.Message };
        }
    }
}
