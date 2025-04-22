using STS.Domain.Core.Contracts.AppService;
using STS.Domain.Core.Contracts.Service;
using STS.Domain.Core.Dtos.TaskItemDtos;
using STS.Domain.Core.Entities;
using STS.Domain.Core.Entities.Feature;

namespace STS.Domain.AppService.Feature;
public class TaskItemAppService(ITaskItemService taskService,
    IBaseDataService baseDataService) : ITaskItemAppService
{
    public async Task<Result<TaskItem>> Add(TaskItemDto model, CancellationToken cancellationToken)
        => await taskService.Add(model, cancellationToken);

    public async Task<Result<bool>> Delete(int id, CancellationToken cancellationToken)
        => await taskService.Delete(id, cancellationToken);

    public async Task<Result<TaskItemDto>> GetBySubCategoryId(int id, CancellationToken cancellationToken)
        => await taskService.GetBySubCategoryId(id, cancellationToken);

    public async Task<Result<IEnumerable<TaskItemDto>>> GetAll(CancellationToken cancellationToken)
        => await taskService.GetAll(cancellationToken);

    public async Task<Result<IEnumerable<TaskItemDto>>> GetAllBySubCategoryId(int id, CancellationToken cancellationToken)
        => await taskService.GetAllBySubCategoryId(id, cancellationToken);

    public async Task<Result<TaskItemDto>> GetBy(int id, CancellationToken cancellationToken)
        => await taskService.GetBy(id, cancellationToken);

    public async Task<Result<bool>> SetImgPath(int TaskItem, string ImgPath, CancellationToken cancellationToken)
        => await taskService.SetImgPath(TaskItem, ImgPath, cancellationToken);

    public async Task<Result<bool>> SoftDelete(int TaskItem, CancellationToken cancellationToken)
        => await taskService.SoftDelete(TaskItem, cancellationToken);
    public async Task<Result<bool>> Update(TaskItemDto model, CancellationToken cancellationToken)
    {
        string folderImagesPath = "/Images/TaskItems";

        if (model.TaskItemImg is not null)
        {
            model.ImgPath = await baseDataService.UploadImage(model.TaskItemImg!, folderImagesPath, cancellationToken);
        }

        return await taskService.Update(model, cancellationToken);
    }

    public async Task<Result<IEnumerable<TaskItemDto>>> Search(string keyword, CancellationToken cancellationToken)
    {
        return await taskService.Search(keyword, cancellationToken);
    }
}
