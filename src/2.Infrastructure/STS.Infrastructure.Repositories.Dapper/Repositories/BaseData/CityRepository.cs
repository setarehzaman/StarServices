using Dapper;
using Microsoft.Data.SqlClient;
using STS.Domain.Core.Entities;
using STS.Domain.Core.Entities.BaseEntities;
using STS.Domain.Core.Entities.Configs;
using STS.Infrastructure.Cache.Redis;
using STS.Infrastructure.Repositories.Dapper.Contracts;
using STS.Infrastructure.Repositories.Dapper.Queries;
using System.Data;

namespace STS.Infrastructure.Repositories.Dapper.Repositories.BaseData;

public class CityRepository(IRedisCacheServices cacheServices, SiteSettings siteSettings) : ICityRepository
{
    private IDbConnection db;
    public async Task<Result<IEnumerable<City>>> GetAllCities(CancellationToken cancellationToken)
    {
        try
        {
            var cacheData = cacheServices.Get<Result<IEnumerable<City>>>(CacheKeys.GetAllCities);
            if (cacheData is not null) return cacheData;

            using (db = new SqlConnection(siteSettings.connectionStrings.SqlConnection))
            {
                var cities = await db.QueryAsync<City>(Query.GetAllCities);
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

}
