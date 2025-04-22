using Microsoft.EntityFrameworkCore;
using STS.Domain.Core.Dtos.TaskItemDtos;
using STS.Domain.Core.Entities.Feature;
using STS.Infrastructure.SqlServer.Common;
using STS.Domain.Core.Contracts.Repository;
using STS.Domain.Core.Entities;
using STS.Infrastructure.Cache.Redis;

namespace STS.Infrastructure.Ef.Repositories.Repositories.Feature;
public class TaskItemRepository(AppDbContext dbContext, IRedisCacheServices cacheServices) : ITaskItemRepository
{
    public async Task<Result<TaskItem>> Add(TaskItem model, CancellationToken cancellationToken)
    {
        try
        {
            await dbContext.TaskItems.AddAsync(model, cancellationToken);
            await dbContext.SaveChangesAsync(cancellationToken);

            var result = new Result<TaskItem>() { IsSuccess = true, Data = model };
            return result;
        }
        catch (Exception e)
        {
            return new Result<TaskItem> { IsSuccess = false, Message = e.Message };
        }
    }
    public async Task<Result<bool>> Update(TaskItemDto model, CancellationToken cancellationToken)
    {
        try
        {
            var entity = await dbContext
                .TaskItems
                .FirstAsync(x => x.Id == model.Id, cancellationToken);

            entity.Name = model.Name;
            entity.ImgPath = model.ImgPath is null ? entity.ImgPath : model.ImgPath;
            entity.BasePrice = model.BasePrice;
            entity.SubCategoryId = model.SubCategoryId;

            dbContext.TaskItems.Update(entity);
            await dbContext.SaveChangesAsync(cancellationToken);

            var result = new Result<bool>() { IsSuccess = true, Data = true };
            return result;
        }
        catch (Exception e)
        {
            return new Result<bool> { IsSuccess = false, Message = e.Message };
        }
    }
    public async Task<Result<bool>> Delete(int id, CancellationToken cancellationToken)
    {
        try
        {
            var entity = await dbContext
                .TaskItems.FirstAsync(x => x.Id == id, cancellationToken);

            dbContext.TaskItems.Remove(entity);
            await dbContext.SaveChangesAsync(cancellationToken);

            var result = new Result<bool>() { IsSuccess = true, Data = true };
            return result;
        }
        catch (Exception e)
        {
            return new Result<bool> { IsSuccess = false, Message = e.Message };
        }
    }
    public async Task<Result<TaskItemDto>> GetBy(int id, CancellationToken cancellationToken)
    {
        try
        {
            var entity = await dbContext
                .TaskItems.AsNoTracking()
                .Select(x => new TaskItemDto()
                {
                    Id = x.Id,
                    Name = x.Name,
                    ImgPath = x.ImgPath,
                    BasePrice = x.BasePrice,
                    SubCategoryId = x.SubCategoryId,
                    SubCategoryName = x.SubCategory.Name,
                })
                .FirstAsync(x => x.Id == id, cancellationToken);

            var result = new Result<TaskItemDto>() { IsSuccess = true, Data = entity };
            return result;
        }
        catch (Exception e)
        {
            return new Result<TaskItemDto> { IsSuccess = false, Message = e.Message };
        }
    }
    public async Task<Result<TaskItemDto>> GetBySubCategoryId(int id, CancellationToken cancellationToken)
    {
        try
        {
            var entity = await dbContext
                .TaskItems.AsNoTracking()
                .Select(x => new TaskItemDto()
                {
                    Id = x.Id,
                    Name = x.Name,
                    ImgPath = x.ImgPath,
                    BasePrice = x.BasePrice,
                    SubCategoryId = x.SubCategoryId,
                    SubCategoryName = x.SubCategory.Name,
                })
                .FirstAsync(x => x.SubCategoryId == id, cancellationToken);

            var result = new Result<TaskItemDto>() { IsSuccess = true, Data = entity };
            return result;
        }
        catch (Exception e)
        {
            return new Result<TaskItemDto> { IsSuccess = false, Message = e.Message };
        }
    }

    public async Task<Result<IEnumerable<TaskItemDto>>> GetAllBySubCategoryId(int id, CancellationToken cancellationToken)
    {
        try
        {
            var entities = await dbContext
                .TaskItems
                .Where(x => x.SubCategoryId == id)
                .AsNoTracking()
                .Select(x => new TaskItemDto()
                {
                    Id = x.Id,
                    Name = x.Name,
                    ImgPath = x.ImgPath,
                    BasePrice = x.BasePrice,
                    SubCategoryId = x.SubCategoryId,
                    SubCategoryName = x.SubCategory.Name,
                })
                .ToListAsync(cancellationToken);


            var result = new Result<IEnumerable<TaskItemDto>>() { IsSuccess = true, Data = entities };
            return result;
        }
        catch (Exception e)
        {
            return new Result<IEnumerable<TaskItemDto>> { IsSuccess = false, Message = e.Message };
        }
    }

    public async Task<Result<IEnumerable<TaskItemDto>>> Search(string keyword, CancellationToken cancellationToken)
    {
        try
        {
            var entities = await dbContext
                .TaskItems
                .Where(x => x.Name.Contains(keyword))
                .AsNoTracking()
                .Select(x => new TaskItemDto()
                {
                    Id = x.Id,
                    Name = x.Name,
                    ImgPath = x.ImgPath,
                    BasePrice = x.BasePrice,
                    SubCategoryId = x.SubCategoryId,
                    SubCategoryName = x.SubCategory.Name,
                })
                .ToListAsync(cancellationToken);


            var result = new Result<IEnumerable<TaskItemDto>>() { IsSuccess = true, Data = entities };
            return result;
        }
        catch (Exception e)
        {
            return new Result<IEnumerable<TaskItemDto>> { IsSuccess = false, Message = e.Message };
        }
    }
    public async Task<Result<IEnumerable<TaskItemDto>>> GetAll(CancellationToken cancellationToken)
    {
        try
        {
            var cacheData = cacheServices.Get<Result<IEnumerable<TaskItemDto>>>(CacheKeys.GetAllTaskItems);
            if (cacheData is not null) return cacheData;

            var entities = await dbContext
                .TaskItems.AsNoTracking()
                .Select(x => new TaskItemDto()
                {
                    Id = x.Id,
                    Name = x.Name,
                    ImgPath = x.ImgPath,
                    BasePrice = x.BasePrice,
                    SubCategoryId = x.SubCategoryId,
                    SubCategoryName = x.SubCategory.Name,
                })
                .ToListAsync(cancellationToken);

            var result = new Result<IEnumerable<TaskItemDto>>() { IsSuccess = true, Data = entities };

            cacheServices.SetSliding(CacheKeys.GetAllTaskItems, result, 360);

            return result;
        }
        catch (Exception e)
        {
            return new Result<IEnumerable<TaskItemDto>> { IsSuccess = false, Message = e.Message };
        }
    }
    public async Task<Result<bool>> SoftDelete(int TaskItem, CancellationToken cancellationToken)
    {
        try
        {
            var entity = await dbContext
                .TaskItems
                .FirstAsync(x => x.Id == TaskItem, cancellationToken);

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
    public async Task<Result<bool>> SetImgPath(int TaskItem, string ImgPath, CancellationToken cancellationToken)
    {
        try
        {
            var entity = await dbContext
                .TaskItems
                .FirstOrDefaultAsync(x => x.Id == TaskItem, cancellationToken);

            entity.ImgPath = ImgPath;

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