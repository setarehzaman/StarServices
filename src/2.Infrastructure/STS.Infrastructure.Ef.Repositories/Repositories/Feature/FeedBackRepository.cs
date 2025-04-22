using Microsoft.EntityFrameworkCore;
using STS.Domain.Core.Contracts.Repository;
using STS.Domain.Core.Dtos.FeedBack;
using STS.Domain.Core.Entities;
using STS.Domain.Core.Entities.Feature;
using STS.Infrastructure.SqlServer.Common;

namespace STS.Infrastructure.Ef.Repositories.Repositories.Feature;
public class FeedBackRepository(AppDbContext dbContext) : IFeedBackRepository
{
    public async Task<Result<FeedBack>> Add(AddFeedBackDto model, CancellationToken cancellationToken)
    {
        try
        {
            var entity = new FeedBack();

            entity.Rating = model.Rating;
            entity.Comment = model.Comment;
            entity.ClientId = model.ClientId;
            entity.ExpertId = model.ExpertId;

            await dbContext.FeedBacks.AddAsync(entity, cancellationToken);
            await dbContext.SaveChangesAsync(cancellationToken);

            var result = new Result<FeedBack>() { IsSuccess = true, Data = entity };
            return result;
        }
        catch (Exception e)
        {
            return new Result<FeedBack> { IsSuccess = false, Message = e.Message };
        }
    }
    public async Task<Result<bool>> Delete(int id, CancellationToken cancellationToken)
    {
        try
        {
            var entity = await dbContext
                .FeedBacks.FirstAsync(x => x.Id == id, cancellationToken);

            dbContext.FeedBacks.Remove(entity);
            await dbContext.SaveChangesAsync(cancellationToken);

            var result = new Result<bool>() { IsSuccess = true };
            return result;
        }
        catch (Exception e)
        {
            return new Result<bool> { IsSuccess = false, Message = e.Message };
        }
    }
    public async Task<Result<IEnumerable<FeedBackDto>>> GetAll(CancellationToken cancellationToken)
    {
        try
        {
            var entities = await dbContext
                .FeedBacks
                .AsNoTracking()
                .Select(x => new FeedBackDto()
                {
                    Id = x.Id,
                    Comment = x.Comment,
                    Rating = x.Rating,
                    ClientId = x.ClientId,
                    ExpertId = x.ExpertId,
                    IsAccepted = x.IsAccepted,
                    ClientName = $"{x.Client.ApplicationUser.FirstName} {x.Client.ApplicationUser.LastName}",
                    ExpertName = $"{x.Expert.ApplicationUser.FirstName} {x.Expert.ApplicationUser.LastName}"
                })
                .ToListAsync(cancellationToken);

            var result = new Result<IEnumerable<FeedBackDto>>() { IsSuccess = true, Data = entities };
            return result;
        }
        catch (Exception e)
        {
            return new Result<IEnumerable<FeedBackDto>> { IsSuccess = false, Message = e.Message };
        }
    }
    public async Task<Result<FeedBackDto>> GetBy(int id, CancellationToken cancellationToken)
    {
        try
        {
            var entity = await dbContext.FeedBacks.AsNoTracking()
                .Where(x => x.Id == id)
                .Select(x => new FeedBackDto()
                {
                    Comment = x.Comment,
                    Rating = x.Rating,
                    ClientId = x.ClientId,
                    ExpertId = x.ExpertId,
                })
                .FirstAsync(cancellationToken);

            var result = new Result<FeedBackDto>() { IsSuccess = true, Data = entity };
            return result;
        }
        catch (Exception e)
        {
            return new Result<FeedBackDto> { IsSuccess = false, Message = e.Message };
        }
    }
    public async Task<Result<bool>> Update(UpdtaFeedBackDto model, CancellationToken cancellationToken)
    {
        try
        {
            var entity = await dbContext
                .FeedBacks.FirstOrDefaultAsync(x => x.Id == model.Id, cancellationToken);

            entity!.IsAccepted = model.IsAccepted;
            entity.Comment = model.Comment;
            entity.Rating = model.Rating;

            dbContext.FeedBacks.Update(entity);
            await dbContext.SaveChangesAsync(cancellationToken);

            var result = new Result<bool>() { IsSuccess = true };
            return result;
        }
        catch (Exception e)
        {
            return new Result<bool> { IsSuccess = false, Message = e.Message };
        }
    }
    public async Task<Result<bool>> Reject(int feedBackId, CancellationToken cancellationToken)
    {
        try
        {
            var entity = await dbContext.FeedBacks
                .Where(x => x.Id == feedBackId)
                .FirstAsync(cancellationToken);

            entity!.IsAccepted = false;

            await dbContext.SaveChangesAsync(cancellationToken);

            var result = new Result<bool>() { IsSuccess = true };
            return result;
        }
        catch (Exception e)
        {
            return new Result<bool> { IsSuccess = false, Message = e.Message };
        }

    }
    public async Task<Result<bool>> Accept(int feedBackId, CancellationToken cancellationToken)
    {
        try
        {
            var entity = await dbContext.FeedBacks
                .Where(x => x.Id == feedBackId)
                .FirstOrDefaultAsync(cancellationToken);

            entity!.IsAccepted = true;

            await dbContext.SaveChangesAsync(cancellationToken);

            var result = new Result<bool>() { IsSuccess = true };
            return result;
        }
        catch (Exception e)
        {
            return new Result<bool> { IsSuccess = false, Message = e.Message };
        }
    }
    public async Task<Result<bool>> SoftDelete(int feedBackId, CancellationToken cancellationToken)
    {
        try
        {
            var entity = await dbContext
                .FeedBacks
                .FirstAsync(x => x.Id == feedBackId, cancellationToken);

            entity.SoftDelete = false;

            await dbContext.SaveChangesAsync(cancellationToken);

            var result = new Result<bool>() { IsSuccess = true };
            return result;
        }
        catch (Exception e)
        {
            return new Result<bool> { IsSuccess = false, Message = e.Message };
        }
    }
    public async Task<Result<IEnumerable<FeedBackDto>>> GetAllBy(int expertId, CancellationToken cancellationToken)
    {
        try
        {
            var entities = await dbContext
                .FeedBacks
                .AsNoTracking()
                .Where(x => x.ExpertId == expertId && x.IsAccepted)   
                .Select(x => new FeedBackDto()
                {
                    Id = x.Id,
                    Comment = x.Comment,
                    Rating = x.Rating,
                    ClientId = x.ClientId,
                    ExpertId = x.ExpertId,
                    IsAccepted = x.IsAccepted,
                    ClientName = $"{x.Client.ApplicationUser.FirstName} {x.Client.ApplicationUser.LastName}",
                    ClientImgPath = x.Client.ApplicationUser.ImgProfilePath,
                    
                    
                })
                .ToListAsync(cancellationToken);

            var result = new Result<IEnumerable<FeedBackDto>>() { IsSuccess = true, Data = entities };
            return result;
        }
        catch (Exception e)
        {
            return new Result<IEnumerable<FeedBackDto>> { IsSuccess = false, Message = e.Message };
        }
    }
}
