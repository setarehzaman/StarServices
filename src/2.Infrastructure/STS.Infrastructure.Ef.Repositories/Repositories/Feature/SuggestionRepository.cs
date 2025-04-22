using Microsoft.EntityFrameworkCore;
using STS.Domain.Core.Contracts.Repository;
using STS.Domain.Core.Dtos.SuggestionDtos;
using STS.Domain.Core.Entities;
using STS.Domain.Core.Entities.Feature;
using STS.Infrastructure.SqlServer.Common;

namespace STS.Infrastructure.Ef.Repositories.Repositories.Feature;
public class SuggestionRepository(AppDbContext dbContext) : ISuggestionRepository
{
    public async Task<Result<Suggestion>> Add(AddSuggestionDto model, CancellationToken cancellationToken)
    {
        try
        {
            var entity = new Suggestion();

            entity.DoAt = model.DoAt;
            entity.CreateAt = DateTime.Now;
            entity.SuggestedPrice = model.SuggestedPrice;
            entity.OrderId = model.OrderId;
            entity.ExpertId = model.ExpertId;

            await dbContext.Suggestions.AddAsync(entity, cancellationToken);
            await dbContext.SaveChangesAsync(cancellationToken);

            var result = new Result<Suggestion>() { IsSuccess = true, Data = entity };
            return result;
        }
        catch (Exception e)
        {
            return new Result<Suggestion> { IsSuccess = false, Message = e.Message };
        }
    }
    public async Task<Result<bool>> Delete(int id, CancellationToken cancellationToken)
    {
        try
        {
            var entity = await dbContext
                .Suggestions.FirstAsync(x => x.Id == id, cancellationToken);

            dbContext.Suggestions.Remove(entity);
            await dbContext.SaveChangesAsync(cancellationToken);

            var result = new Result<bool>() { IsSuccess = true, Data = true };
            return result;
        }
        catch (Exception e)
        {
            return new Result<bool> { IsSuccess = false, Message = e.Message };
        }
    }
    public async Task<Result<IEnumerable<SuggestionDto>>> GetAll(CancellationToken cancellationToken)
    {
        try
        {
            var entity = await dbContext.Suggestions.AsNoTracking()
                .Select(x => new SuggestionDto()
                {
                    DoAt = x.DoAt,
                    SuggestedPrice = x.SuggestedPrice,
                    OrderId = x.OrderId,
                    ExpertId = x.ExpertId,
                    IsAccepted = x.IsAccepted,
                    ExpertName = $"{x.Expert.ApplicationUser.FirstName} {x.Expert.ApplicationUser.LastName}"


                })
                .ToListAsync(cancellationToken);

            var result = new Result<IEnumerable<SuggestionDto>>() { IsSuccess = true, Data = entity };
            return result;
        }
        catch (Exception e)
        {
            return new Result<IEnumerable<SuggestionDto>> { IsSuccess = false, Message = e.Message };
        }
    }
    public async Task<Result<SuggestionDto>> GetBy(int id, CancellationToken cancellationToken)
    {
        try
        {
            var entity = await dbContext.Suggestions.AsNoTracking()
                .Where(x => x.Id == id)
                .Select(x => new SuggestionDto()
                {
                    DoAt = x.DoAt,
                    SuggestedPrice = x.SuggestedPrice,
                    OrderId = x.OrderId,
                    ExpertId = x.ExpertId,
                    IsAccepted = x.IsAccepted,
                    ExpertName = $"{x.Expert.ApplicationUser.FirstName} {x.Expert.ApplicationUser.LastName}"

                })
                .FirstAsync(cancellationToken);

            var result = new Result<SuggestionDto>() { IsSuccess = true, Data = entity };
            return result;
        }
        catch (Exception e)
        {
            return new Result<SuggestionDto> { IsSuccess = false, Message = e.Message };
        }
    }
    public async Task<Result<bool>> Update(UpdateSuggestionDto model, CancellationToken cancellationToken)
    {
        try
        {
            var entity = await dbContext
                .Suggestions.FirstOrDefaultAsync(x => x.Id == model.Id, cancellationToken);

            entity!.IsAccepted = model.IsAccepted;
            entity.SuggestedPrice = model.SuggestedPrice;
            entity.DoAt = model.DoAt;

            dbContext.Suggestions.Update(entity);
            await dbContext.SaveChangesAsync(cancellationToken);

            var result = new Result<bool>() { IsSuccess = true, Data = true };
            return result;

        }
        catch (Exception e)
        {
            return new Result<bool> { IsSuccess = false, Message = e.Message };
        }
    }
    public async Task<Result<bool>> Reject(int suggestionId, CancellationToken cancellationToken)
    {
        try
        {
            var entity = await dbContext.Suggestions
                .Where(x => x.Id == suggestionId)
                .FirstAsync(cancellationToken);

            entity!.IsAccepted = false;

            await dbContext.SaveChangesAsync(cancellationToken);

            var result = new Result<bool>() { IsSuccess = true, Data = true };
            return result;
        }
        catch (Exception e)
        {
            return new Result<bool> { IsSuccess = false, Message = e.Message };
        }

    }
    public async Task<Result<bool>> Accept(int suggestionId, CancellationToken cancellationToken)
    {
        try
        {
            var entity = await dbContext.Suggestions
                .Where(x => x.Id == suggestionId)
                .FirstAsync(cancellationToken);

            entity!.IsAccepted = true;

            await dbContext.SaveChangesAsync(cancellationToken);

            var result = new Result<bool>() { IsSuccess = true, Data = true };
            return result;
        }
        catch (Exception e)
        {
            return new Result<bool> { IsSuccess = false, Message = e.Message };
        }
    }
    public async Task<Result<IEnumerable<SuggestionDto>>> GetAllSuggestionsPerOrder(int orderId, CancellationToken cancellationToken)
    {
        try
        {
            var suggestions = await dbContext.Suggestions
                .AsNoTracking()
                .Where(x => x.OrderId == orderId)
                .Select(x => new SuggestionDto()
                {
                    Id = x.Id,
                    OrderId = x.OrderId,
                    ExpertId = x.ExpertId,
                    DoAt = x.DoAt,
                    SuggestedPrice = x.SuggestedPrice,
                    ExpertName = $"{x.Expert.ApplicationUser.FirstName} {x.Expert.ApplicationUser.LastName}",
                    IsAccepted = x.IsAccepted,
                })
                .ToListAsync(cancellationToken);
            var result = new Result<IEnumerable<SuggestionDto>>() { IsSuccess = true, Data = suggestions };
            return result;

        }
        catch (Exception e)
        {
            return new Result<IEnumerable<SuggestionDto>> { IsSuccess = false, Message = e.Message };
        }
    }

    public async Task<Result<double>> GetAcceptedSuggestionPrice(int orderId, CancellationToken cancellationToken)
    {
        try
        {
            var suggestion = await dbContext.Suggestions
                .Where(x => x.OrderId == orderId && x.IsAccepted)
                .FirstAsync(cancellationToken);

            return new Result<double>() { IsSuccess = true, Data = suggestion.SuggestedPrice };
        }
        catch (Exception e)
        {
            return new Result<double>() { IsSuccess = false };
        }

    }

    public async Task<Result<IEnumerable<SuggestionDto>>> GetAllBy(int expertId, CancellationToken cancellationToken)
    {
        try
        {
            var entity = await dbContext.Suggestions.AsNoTracking()
                .Where(x => x.ExpertId == expertId)
                .Select(x => new SuggestionDto()
                {
                    DoAt = x.DoAt,
                    SuggestedPrice = x.SuggestedPrice,
                    OrderId = x.OrderId,
                    ExpertId = x.ExpertId,
                    IsAccepted = x.IsAccepted,
                    ExpertName = $"{x.Expert.ApplicationUser.FirstName} {x.Expert.ApplicationUser.LastName}",
                    TaskName = x.Order.Task.Name,
                    OrderStatus = x.Order.OrderStatus
                })
                .ToListAsync(cancellationToken);

            var result = new Result<IEnumerable<SuggestionDto>>() { IsSuccess = true, Data = entity };
            return result;
        }
        catch (Exception e)
        {
            return new Result<IEnumerable<SuggestionDto>> { IsSuccess = false, Message = e.Message };
        }
    }
}
