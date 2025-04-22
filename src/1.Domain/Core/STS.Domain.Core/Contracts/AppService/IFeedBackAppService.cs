using STS.Domain.Core.Dtos.FeedBack;
using STS.Domain.Core.Entities;
using STS.Domain.Core.Entities.Feature;

namespace STS.Domain.Core.Contracts.AppService;

public interface IFeedBackAppService
{
    Task<Result<FeedBack>> Add(AddFeedBackDto model, CancellationToken cancellationToken);
    Task<Result<bool>> Update(UpdtaFeedBackDto model, CancellationToken cancellationToken);
    Task<Result<bool>> Delete(int id, CancellationToken cancellationToken);
    Task<Result<FeedBackDto>> GetBy(int id, CancellationToken cancellationToken);
    Task<Result<IEnumerable<FeedBackDto>>> GetAll(CancellationToken cancellationToken);
    Task<Result<bool>> Accept(int feedBackId, CancellationToken cancellationToken);
    Task<Result<bool>> Reject(int feedBackId, CancellationToken cancellationToken);
    Task<Result<bool>> SoftDelete(int feedBackId, CancellationToken cancellationToken);
    Task<Result<IEnumerable<FeedBackDto>>> GetAllBy(int ExpertId, CancellationToken cancellationToken);
}
