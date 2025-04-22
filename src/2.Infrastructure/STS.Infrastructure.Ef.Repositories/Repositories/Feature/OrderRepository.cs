using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using STS.Domain.Core.Contracts.Repository;
using STS.Domain.Core.Dtos.OrderDtos;
using STS.Domain.Core.Dtos.SuggestionDtos;
using STS.Domain.Core.Entities;
using STS.Domain.Core.Entities.Feature;
using STS.Domain.Core.Entities.User;
using STS.Domain.Core.Enums;
using STS.Infrastructure.SqlServer.Common;

namespace STS.Infrastructure.Ef.Repositories.Repositories.Feature;
public class OrderRepository(AppDbContext dbContext) : IOrderRepository
{
    public async Task<Result<Order>> Add(AddOrderDto model, CancellationToken cancellationToken)
    {
        try
        {
            var entity = new Order()
            {
                OrderedPrice = model.OrderedPrice,
                TaskId = model.TaskId,
                ClientId = model.ClientId,
                DoAt = model.DoAt,
                CreatedAt = DateTime.Now,
                OrderStatus = OrderStatusEnum.AwaitingSuggestions,
                Description = model.Description
            };

            await dbContext.Orders.AddAsync(entity, cancellationToken);
            await dbContext.SaveChangesAsync(cancellationToken);

            var result = new Result<Order>() { IsSuccess = true, Data = entity };
            return result;
        }
        catch (Exception e)
        {
            return new Result<Order> { IsSuccess = false, Message = e.Message };
        }
    }
    public async Task<Result<bool>> Delete(int id, CancellationToken cancellationToken)
    {
        try
        {
            var entity = await dbContext
                .Orders.FirstAsync(x => x.Id == id, cancellationToken);

            dbContext.Orders.Remove(entity);
            await dbContext.SaveChangesAsync(cancellationToken);

            var result = new Result<bool>() { IsSuccess = true, Data = true };
            return result;
        }
        catch (Exception e)
        {
            return new Result<bool> { IsSuccess = false, Message = e.Message };
        }
    }
    public async Task<Result<IEnumerable<OrderDto>>> GetAllByClientId(int clientId, CancellationToken cancellationToken)
    {
        try
        {
            var entities = await dbContext
                .Orders
                .Where(x => x.ClientId == clientId)
                .AsNoTracking()
                .Select(x => new OrderDto()
                {
                    Id = x.Id,
                    IsAccepted = x.IsAccepted,
                    DoAt = x.DoAt,
                    OrderedPrice = x.OrderedPrice,
                    TaskId = x.TaskId,
                    ClientId = x.ClientId,
                    OrderStatus = x.OrderStatus,
                    ClientName = $"{x.Client.ApplicationUser.FirstName} {x.Client.ApplicationUser.LastName}",
                    TaskName = x.Task.Name,
                    SuggestionsCount = x.Suggestions.Count,
                    ImagesPath = x.Pictures.Any() ? x.Pictures.Select(x => x.Path).ToList() : new List<string>(),
                    CreateAt = x.CreatedAt,
                    Suggestions = x.Suggestions.Select(x => new SuggestionDto()
                    {
                        Id = x.Id,
                        ExpertId = x.ExpertId,
                        DoAt = x.DoAt,
                        ExpertName = $"{x.Expert.ApplicationUser.FirstName} {x.Expert.ApplicationUser.LastName}",
                        IsAccepted = x.IsAccepted,
                        OrderId = x.OrderId,
                        SuggestedPrice = x.SuggestedPrice,
                        ImageProfilePath = x.Expert.ApplicationUser.ImgProfilePath
                    }).ToList()

                })
                .ToListAsync(cancellationToken);

            var result = new Result<IEnumerable<OrderDto>>() { IsSuccess = true, Data = entities };
            return result;
        }
        catch (Exception e)
        {
            return new Result<IEnumerable<OrderDto>> { IsSuccess = false, Message = e.Message };
        }
    }
    public async Task<Result<IEnumerable<OrderDto>>> GetAll(CancellationToken cancellationToken)
    {
        try
        {
            var entities = await dbContext
                .Orders
                .AsNoTracking()
                .Select(x => new OrderDto()
                {
                    Id = x.Id,
                    IsAccepted = x.IsAccepted,
                    DoAt = x.DoAt,
                    OrderedPrice = x.OrderedPrice,
                    TaskId = x.TaskId,
                    ClientId = x.ClientId,
                    OrderStatus = x.OrderStatus,
                    ClientName = $"{x.Client.ApplicationUser.FirstName} {x.Client.ApplicationUser.LastName}",
                    TaskName = x.Task.Name
                })
                .ToListAsync(cancellationToken);

            var result = new Result<IEnumerable<OrderDto>>() { IsSuccess = true, Data = entities };
            return result;
        }
        catch (Exception e)
        {
            return new Result<IEnumerable<OrderDto>> { IsSuccess = false, Message = e.Message };
        }
    }
    public async Task<Result<bool>> ChangeStatus(int orderId, OrderStatusEnum status, CancellationToken cancellationToken)
    {
        try
        {
            var entity = await dbContext
                .Orders.FirstAsync(x => x.Id == orderId, cancellationToken);

            entity.OrderStatus = status;

            dbContext.Orders.Update(entity);
            await dbContext.SaveChangesAsync(cancellationToken);

            var result = new Result<bool>() { IsSuccess = true, Data = true };
            return result;
        }
        catch (Exception e)
        {
            return new Result<bool> { IsSuccess = false, Message = e.Message };
        }
    }
    public async Task<Result<OrderDto>> GetBy(int id, CancellationToken cancellationToken)
    {
        try
        {
            var entity = await dbContext.Orders.AsNoTracking()
                .Where(x => x.Id == id)
                .Select(x => new OrderDto()
                {
                    Id = x.Id,
                    IsAccepted = x.IsAccepted,
                    DoAt = x.DoAt,
                    OrderedPrice = x.OrderedPrice,
                    Description = x.Description,
                    TaskId = x.TaskId,
                    ClientId = x.ClientId,
                    OrderStatus = x.OrderStatus,
                    ClientName = $"{x.Client.ApplicationUser.FirstName} {x.Client.ApplicationUser.LastName}",
                    TaskName = x.Task.Name,
                    SuggestionsCount = x.Suggestions.Count,
                    ImagesPath = x.Pictures.Any() ? x.Pictures.Select(x => x.Path).ToList() : new List<string>(),
                    Suggestions = x.Suggestions.Select(x => new SuggestionDto()
                    {
                        Id = x.Id,
                        ExpertId = x.ExpertId,
                        DoAt = x.DoAt,
                        ExpertName = $"{x.Expert.ApplicationUser.FirstName} {x.Expert.ApplicationUser.LastName}",
                        IsAccepted = x.IsAccepted,
                        OrderId = x.OrderId,
                        SuggestedPrice = x.SuggestedPrice,
                        ImageProfilePath = x.Expert.ApplicationUser.ImgProfilePath
                    }).ToList()
                })
                .FirstAsync(cancellationToken);


            var result = new Result<OrderDto>() { IsSuccess = true, Data = entity };
            return result;
        }
        catch (Exception e)
        {
            return new Result<OrderDto> { IsSuccess = false, Message = e.Message };
        }
    }
    public async Task<Result<bool>> Update(UpdateOrderDto model, CancellationToken cancellationToken)
    {
        try
        {
            var entity = await dbContext
                .Orders.FirstOrDefaultAsync(x => x.Id == model.Id, cancellationToken);

            entity!.OrderedPrice = model.OrderedPrice;
            entity.DoAt = model.DoAt;
            entity.OrderStatus = model.OrderStatus;

            dbContext.Orders.Update(entity);
            await dbContext.SaveChangesAsync(cancellationToken);

            var result = new Result<bool>() { IsSuccess = true, Data = true };
            return result;
        }
        catch (Exception e)
        {
            return new Result<bool> { IsSuccess = false, Message = e.Message };
        }
    }
    public async Task<Result<bool>> Reject(int orderId, CancellationToken cancellationToken)
    {
        try
        {
            var entity = await dbContext
                .Orders
                .Where(x => x.Id == orderId)
                .FirstOrDefaultAsync(cancellationToken);

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
    public async Task<Result<bool>> Accept(int orderId, CancellationToken cancellationToken)
    {
        try
        {
            var entity = await dbContext
                .Orders
                .Where(x => x.Id == orderId)
                .FirstOrDefaultAsync(cancellationToken);

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
    public async Task<Result<bool>> HasAnySuggestion(int orderId, CancellationToken cancellationToken)
    {
        try
        {
            var anySuggestion = await dbContext.Suggestions
                .AnyAsync(x => x.OrderId == orderId, cancellationToken);

            var result = new Result<bool>() { IsSuccess = true, Data = anySuggestion };
            return result;
        }
        catch (Exception e)
        {
            return new Result<bool> { IsSuccess = false, Message = e.Message };
        }
    }
    public async Task<Result<IEnumerable<OrderStatus>>> GetAllOrderStatuses(CancellationToken cancellationToken)
    {
        try
        {
            var statuses = await dbContext.OrderStatus
                .ToListAsync(cancellationToken);

            var result = new Result<IEnumerable<OrderStatus>>() { IsSuccess = true, Data = statuses };
            return result;
        }
        catch (Exception e)
        {
            return new Result<IEnumerable<OrderStatus>> { IsSuccess = false, Message = e.Message };
        }
    }
    public async Task<Result<IEnumerable<OrderDto>>> GetAllBy(List<int> taskIds, int expertId, CancellationToken cancellationToken)
    {
        try
        {
            var entities = await dbContext
                .Orders
                .AsNoTracking()
                .Where(x => taskIds.Contains(x.TaskId) && !x.IsAccepted)
                .Select(x => new OrderDto()
                {
                    Id = x.Id,
                    IsAccepted = x.IsAccepted,
                    DoAt = x.DoAt,
                    OrderedPrice = x.OrderedPrice,
                    TaskId = x.TaskId,
                    ClientId = x.ClientId,
                    OrderStatus = x.OrderStatus,
                    Description = x.Description,
                    ClientName = $"{x.Client.ApplicationUser.FirstName} {x.Client.ApplicationUser.LastName}",
                    TaskName = x.Task.Name,
                    ImagesPath = x.Pictures.Any() ? x.Pictures.Select(x => x.Path).ToList() : new List<string>(),
                    MainCategoryImgAddress = x.Task.SubCategory.MainCategory.ImgPath,
                    AlreadyHasSuggestion = x.Suggestions.Any(x => x.ExpertId == expertId)
                })
                .ToListAsync(cancellationToken);

            var result = new Result<IEnumerable<OrderDto>>() { IsSuccess = true, Data = entities };
            return result;
        }
        catch (Exception e)
        {
            return new Result<IEnumerable<OrderDto>> { IsSuccess = false, Message = e.Message };
        }
    }
    public async Task<Result<IEnumerable<GetExpiredOrderDto>>> GetExpiredOrders(CancellationToken cancellationToken)
    {
        try
        {
            var rawData = await dbContext.Orders
             .AsNoTracking()
             .Where(x => x.CreatedAt < DateTime.Now.AddDays(-10) &&
                         x.OrderStatus == OrderStatusEnum.AwaitingSuggestions)
             .Select(x => new
             {
                 x.Id,
                 x.Task.Name,
                 x.Client.ApplicationUser.PhoneNumber,
                 x.Client.ApplicationUser.FirstName,
                 x.Client.ApplicationUser.LastName
             }).ToListAsync(cancellationToken);

            var orderDetails = rawData.Select(x => new GetExpiredOrderDto
            {
                OrderId = x.Id,
                TaskName = x.Name,
                PhoneNumber = string.IsNullOrEmpty(x.PhoneNumber) ? null : x.PhoneNumber,
                FullName = GetFullName(x.FirstName, x.LastName)
            }).ToList();

            return new Result<IEnumerable<GetExpiredOrderDto>> { IsSuccess = true, Data = orderDetails };

        }
        catch (Exception e)
        {
            return new Result<IEnumerable<GetExpiredOrderDto>> { IsSuccess = false, Message = e.Message };
        }
    }

    public async Task<Result<bool>> ChageStatusToExpired(IEnumerable<int> ids, CancellationToken cancellationToken)
    {
        try
        {
            await dbContext.Orders
            .Where(x => ids.Contains(x.Id))
            .ExecuteUpdateAsync(s => s
            .SetProperty(r => r.OrderStatus, OrderStatusEnum.Expired));

            var result = new Result<bool>() { IsSuccess = true, Data = true };
            return result;
        }
        catch (Exception e)
        {
            return new Result<bool> { IsSuccess = false, Message = e.Message };

        }

    }

    private string? GetFullName(string? firstName, string? lastName)
    {
        string result = string.Empty;

        if (firstName == null && lastName == null) return null;

        if (firstName != null) result = firstName;

        if (lastName != null) result = $"{result} {lastName} عزیز";

        return result;
    }
}