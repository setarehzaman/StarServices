using STS.Domain.Core.Contracts.Repository;
using STS.Domain.Core.Contracts.Service;
using STS.Domain.Core.Dtos.TaskItemDtos;
using STS.Domain.Core.Entities;
using STS.Domain.Core.Entities.Feature;

namespace STS.Domain.Service.Feature;
public class TaskItemService(ITaskItemRepository taskRepository,
    IBaseDataService baseDataService) : ITaskItemService
{
    public async Task<Result<TaskItem>> Add(TaskItemDto model, CancellationToken cancellationToken)
    {
        string folderImagesPath = "/Images/TaskItems";

        var fileAddress = await baseDataService.UploadImage(model.TaskItemImg!, folderImagesPath, cancellationToken);

        var TaskItem = new TaskItem()
        {
            Id = model.Id,
            ImgPath = fileAddress,
            Name = model.Name,
            BasePrice = model.BasePrice,
            SubCategoryId = model.SubCategoryId
        };

        return await taskRepository.Add(TaskItem, cancellationToken);
    }

    public Task<Result<bool>> Update(TaskItemDto model, CancellationToken cancellationToken)
    {
        return taskRepository.Update(model, cancellationToken);
    }

    public async Task<Result<bool>> Delete(int id, CancellationToken cancellationToken)
    {
        return await taskRepository.Delete(id, cancellationToken);
    }

    public async Task<Result<TaskItemDto>> GetBy(int id, CancellationToken cancellationToken)
    {
        return await taskRepository.GetBy(id, cancellationToken);
    }

    public async Task<Result<TaskItemDto>> GetBySubCategoryId(int id, CancellationToken cancellationToken)
    {
        return await taskRepository.GetBySubCategoryId(id, cancellationToken);
    }

    public async Task<Result<IEnumerable<TaskItemDto>>> GetAll(CancellationToken cancellationToken)
    {
        return await taskRepository.GetAll(cancellationToken);
    }

    public async Task<Result<IEnumerable<TaskItemDto>>> GetAllBySubCategoryId(int id, CancellationToken cancellationToken)
    {
        return await taskRepository.GetAllBySubCategoryId(id, cancellationToken);
    }

    public async Task<Result<bool>> SoftDelete(int TaskItem, CancellationToken cancellationToken)
    {
        return await taskRepository.SoftDelete(TaskItem, cancellationToken);
    }

    public async Task<Result<bool>> SetImgPath(int TaskItem, string ImgPath, CancellationToken cancellationToken)
    {
        return await taskRepository.SetImgPath(TaskItem, ImgPath, cancellationToken);
    }

    public async Task<Result<IEnumerable<TaskItemDto>>> Search(string keyword, CancellationToken cancellationToken)
    {
        return await taskRepository.Search(keyword, cancellationToken);
    }
}
