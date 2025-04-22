using Dapper;
using Microsoft.Data.SqlClient;
using STS.Domain.Core.Dtos.Category;
using STS.Domain.Core.Entities;
using STS.Domain.Core.Entities.Configs;
using STS.Infrastructure.Cache.Redis;
using STS.Infrastructure.Repositories.Dapper.Contracts;
using STS.Infrastructure.Repositories.Dapper.Queries;
using System.Data;

namespace STS.Infrastructure.Repositories.Dapper.Repositories.Category;

public class MainCategoryRepository(IRedisCacheServices cacheServices, SiteSettings siteSettings) : IMainCategoryRepository
{
    private IDbConnection db;
    public async Task<Result<IEnumerable<MainCategoryDto>>> GetAllMainCategories(CancellationToken cancellationToken)
    {
        try
        {
            var cacheData = cacheServices.Get<Result<IEnumerable<MainCategoryDto>>>(CacheKeys.GetAllMainCategories);
            if (cacheData is not null) return cacheData;

            using (db = new SqlConnection(siteSettings.connectionStrings.SqlConnection))
            {
                var entity = await db.QueryAsync<MainCategoryDto>(Query.GetAllMainCategories);
                var result = new Result<IEnumerable<MainCategoryDto>>() { IsSuccess = true, Data = entity };

                cacheServices.SetSliding(CacheKeys.GetAllMainCategories, result, 500);
                return result;
            }
        }
        catch (Exception e)
        {
            return new Result<IEnumerable<MainCategoryDto>> { IsSuccess = false, Message = e.Message };
        }
    }
}
