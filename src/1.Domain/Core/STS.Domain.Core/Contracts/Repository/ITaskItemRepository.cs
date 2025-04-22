using STS.Domain.Core.Dtos.TaskItemDtos;
using STS.Domain.Core.Entities;
using STS.Domain.Core.Entities.Feature;

namespace STS.Domain.Core.Contracts.Repository;
public interface ITaskItemRepository
{
    Task<Result<TaskItem>> Add(TaskItem model,CancellationToken cancellationToken);
    Task<Result<bool>> Update(TaskItemDto model, CancellationToken cancellationToken);
    Task<Result<bool>> Delete(int id, CancellationToken cancellationToken);
    Task<Result<TaskItemDto>> GetBy(int id,CancellationToken cancellationToken);
    Task<Result<TaskItemDto>> GetBySubCategoryId(int id,CancellationToken cancellationToken);
    Task<Result<IEnumerable<TaskItemDto>>> GetAll(CancellationToken cancellationToken);
    Task<Result<IEnumerable<TaskItemDto>>> GetAllBySubCategoryId(int id, CancellationToken cancellationToken);
    Task<Result<bool>> SoftDelete(int TaskItem, CancellationToken cancellationToken);
    Task<Result<bool>> SetImgPath(int TaskItem, string ImgPath, CancellationToken cancellationToken);
    Task<Result<IEnumerable<TaskItemDto>>> Search(string keyword, CancellationToken cancellationToken);
}