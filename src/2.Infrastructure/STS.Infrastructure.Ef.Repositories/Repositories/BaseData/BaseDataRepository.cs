using Microsoft.EntityFrameworkCore;
using STS.Domain.Core.Contracts.Repository;
using STS.Domain.Core.Entities;
using STS.Domain.Core.Entities.BaseEntities;
using STS.Infrastructure.Cache.Redis;
using STS.Infrastructure.SqlServer.Common;
namespace STS.Infrastructure.Ef.Repositories.Repositories.BaseData;
public class BaseDataRepository(AppDbContext dbContext,IRedisCacheServices cacheServices) : IBaseDataRepository
{
    public async Task<Result<IEnumerable<City>>> GetAllCities(CancellationToken cancellationToken)
    {
        try
        {
            var cacheData = cacheServices.Get<Result<IEnumerable<City>>>(CacheKeys.GetAllCities);

            if (cacheData is not null) return cacheData;
            else
            {
                var cities = await dbContext.Cities
                    .AsNoTracking()
                    .ToListAsync(cancellationToken);

                var result = new Result<IEnumerable<City>>() { IsSuccess = true, Data = cities };

                cacheServices.SetSliding(CacheKeys.GetAllCities, result, 120);

                return result;
            }
        
        }
        catch (Exception e)
        {
            return new Result<IEnumerable<City>> { IsSuccess = false, Message = e.Message };
        }
    }

    public async Task<Result<bool>> AddOrderImages(List<string> imageAddresses, int orderId, CancellationToken cancellationToken)
    {
        try
        {
            var pictures = new List<Picture>();

            foreach (var imgAddress in imageAddresses)
            {
                pictures.Add(new Picture()
                {
                    OrderId = orderId,
                    Path = imgAddress,
                });
            }

            await dbContext.Pictures.AddRangeAsync(pictures, cancellationToken);
            await dbContext.SaveChangesAsync(cancellationToken);

            var result = new Result<bool>() { IsSuccess = true, Data = true };
            return result;
        }
        catch (Exception e)
        {
            return new Result<bool>() { IsSuccess = false, Message = e.Message };
        }
    }
}
