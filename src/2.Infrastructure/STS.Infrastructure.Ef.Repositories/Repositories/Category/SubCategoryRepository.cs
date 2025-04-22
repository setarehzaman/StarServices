using Microsoft.EntityFrameworkCore;
using STS.Domain.Core.Contracts.Repository;
using STS.Domain.Core.Dtos.Category;
using STS.Domain.Core.Entities;
using STS.Domain.Core.Entities.Category;
using STS.Infrastructure.Cache.Redis;
using STS.Infrastructure.SqlServer.Common;
namespace STS.Infrastructure.Ef.Repositories.Repositories.Category;
public class SubCategoryRepository(AppDbContext dbContext,IRedisCacheServices cacheServices) : ISubCategoryRepository
{
    public async Task<Result<IEnumerable<SubCategoryDto>>> GetAll(CancellationToken cancellationToken)
    {
        try
        {
            var cacheData = cacheServices.Get<Result<IEnumerable<SubCategoryDto>>>(CacheKeys.GetAllSubCategories);
            if (cacheData is not null) return cacheData;

            var entity = await dbContext.SubCategories
                .AsNoTracking()
                .Select(x => new SubCategoryDto()
                {
                    Id = x.Id,
                    Name = x.Name,
                    MainCategoryName = x.MainCategory.Name
                })
                .ToListAsync(cancellationToken);

            var result = new Result<IEnumerable<SubCategoryDto>>() { IsSuccess = true, Data = entity };

            cacheServices.SetSliding(CacheKeys.GetAllSubCategories,result,360);

            return result;
        }
        catch (Exception e)
        {
            return new Result<IEnumerable<SubCategoryDto>> { IsSuccess = false, Message = e.Message };
        }

    }
    public async Task<Result<IEnumerable<SubCategoryDto>>> GetAllByMainCategoryId(int id , CancellationToken cancellationToken)
    {
        try
        {
            var entity = await dbContext.SubCategories
                .AsNoTracking()
                .Where(x=>x.MainCategoryId == id)
                .Select(x => new SubCategoryDto()
                {
                    Id = x.Id,
                    Name = x.Name,
                    MainCategoryName = x.MainCategory.Name,
                    MainCategoryId = x.MainCategory.Id
                })
                .ToListAsync(cancellationToken);

            var result = new Result<IEnumerable<SubCategoryDto>>() { IsSuccess = true, Data = entity };
            return result;
        }
        catch (Exception e)
        {
            return new Result<IEnumerable<SubCategoryDto>> { IsSuccess = false, Message = e.Message };
        }

    }
    public async Task<Result<SubCategory>> Add(SubCategoryDto model, CancellationToken cancellationToken)
    {
        try
        {
            var entity = new SubCategory();

            entity.Name = model.Name;
            entity.MainCategoryId = model.MainCategoryId;

            await dbContext.SubCategories.AddAsync(entity, cancellationToken);
            await dbContext.SaveChangesAsync(cancellationToken);

            var result = new Result<SubCategory>() { IsSuccess = true, Data = entity };
            return result;
        }
        catch (Exception e)
        {
            return new Result<SubCategory> { IsSuccess = false, Message = e.Message };
        }
    }

    public async Task<Result<bool>> Delete(int id, CancellationToken cancellationToken)
    {
        try
        {
            var entity = await dbContext
                .SubCategories.FirstAsync(x => x.Id == id, cancellationToken);

            dbContext.SubCategories.Remove(entity);
            await dbContext.SaveChangesAsync(cancellationToken);

            var result = new Result<bool>() { IsSuccess = true };
            return result;
        }
        catch (Exception e)
        {
            return new Result<bool> { IsSuccess = false, Message = e.Message };
        }
    }

    public async Task<Result<SubCategoryDto>> GetBy(int id, CancellationToken cancellationToken)
    {
        try
        {
            var entity = await dbContext.SubCategories
                .AsNoTracking()
                .Where(x => x.Id == id)
                .Select(x => new SubCategoryDto()
                {
                    Id = x.Id,
                    Name = x.Name,
                    MainCategoryId = x.MainCategoryId
                })
                .FirstAsync(cancellationToken);

            var result = new Result<SubCategoryDto>() { IsSuccess = true, Data = entity };
            return result;
        }
        catch (Exception e)
        {
            return new Result<SubCategoryDto> { IsSuccess = false, Message = e.Message };
        }
    }

    public async Task<Result<bool>> Update(SubCategoryDto model, CancellationToken cancellationToken)
    {
        try
        {
            var entity = await dbContext
                .SubCategories.FirstOrDefaultAsync(x => x.Id == model.Id, cancellationToken);

            entity!.Name = model.Name;
            entity.MainCategoryId = model.MainCategoryId;

            dbContext.SubCategories.Update(entity);
            await dbContext.SaveChangesAsync(cancellationToken);

            var result = new Result<bool>() { IsSuccess = true };
            return result;
        }
        catch (Exception e)
        {
            return new Result<bool> { IsSuccess = false, Message = e.Message };
        }
    }
    public async Task<Result<bool>> SoftDelete(int subCategoryId, CancellationToken cancellationToken)
    {
        try
        {
            var entity = await dbContext
                .SubCategories
                .FirstAsync(x => x.Id == subCategoryId, cancellationToken);

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
}
