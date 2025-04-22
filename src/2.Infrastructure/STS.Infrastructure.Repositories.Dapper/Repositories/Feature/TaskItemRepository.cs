using Dapper;
using Microsoft.Data.SqlClient;
using STS.Domain.Core.Dtos.TaskItemDtos;
using STS.Domain.Core.Entities;
using STS.Domain.Core.Entities.Configs;
using STS.Infrastructure.Cache.Redis;
using STS.Infrastructure.Repositories.Dapper.Contracts;
using STS.Infrastructure.Repositories.Dapper.Queries;
using System.Data;

namespace STS.Infrastructure.Repositories.Dapper.Repositories.Feature;
public class TaskItemRepository(IRedisCacheServices cacheServices, SiteSettings siteSettings) : ITaskItemRepository
{
    private IDbConnection db;
    public async Task<Result<IEnumerable<TaskItemDto>>> GetAllTaskItems(CancellationToken cancellationToken)
    {
        try
        {
            var cacheData = cacheServices.Get<Result<IEnumerable<TaskItemDto>>>(CacheKeys.GetAllTaskItems);
            if (cacheData is not null) return cacheData;

            using (db = new SqlConnection(siteSettings.connectionStrings.SqlConnection))
            {
                var entities = await db.QueryAsync<TaskItemDto>(Query.GetAllTaskItems);
                var result = new Result<IEnumerable<TaskItemDto>>() { IsSuccess = true, Data = entities };

                cacheServices.SetSliding(CacheKeys.GetAllTaskItems, result, 360);
                return result;
            }
        }
        catch (Exception e)
        {
            return new Result<IEnumerable<TaskItemDto>> { IsSuccess = false, Message = e.Message };
        }
    }
}
