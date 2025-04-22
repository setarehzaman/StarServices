using STS.Domain.Core.Dtos.TaskItemDtos;
using STS.Domain.Core.Entities;
namespace STS.Infrastructure.Repositories.Dapper.Contracts;
public interface ITaskItemRepository
{
    Task<Result<IEnumerable<TaskItemDto>>> GetAllTaskItems(CancellationToken cancellationToken);
}
