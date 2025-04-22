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
public class SubCategoryRepository(IRedisCacheServices cacheServices, SiteSettings siteSettings) : ISubCategoryRepository
{
    private IDbConnection db;
    public async Task<Result<IEnumerable<SubCategoryDto>>> GetAllSubCategories(CancellationToken cancellationToken)
    {
        try
        {
            var cacheData = cacheServices.Get<Result<IEnumerable<SubCategoryDto>>>(CacheKeys.GetAllSubCategories);
            if (cacheData is not null) return cacheData;

            using (db = new SqlConnection(siteSettings.connectionStrings.SqlConnection))
            {
                var entity = await db.QueryAsync<SubCategoryDto>(Query.GetAllSubCategories);
                var result = new Result<IEnumerable<SubCategoryDto>>() { IsSuccess = true, Data = entity };

                cacheServices.SetSliding(CacheKeys.GetAllSubCategories, result, 360);
                return result;
            }
        }
        catch (Exception e)
        {
            return new Result<IEnumerable<SubCategoryDto>> { IsSuccess = false, Message = e.Message };
        }
    }

}
