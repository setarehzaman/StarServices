using STS.Domain.Core.Contracts.Repository;
using STS.Domain.Core.Contracts.Service;
using STS.Domain.Core.Dtos.FeedBack;
using STS.Domain.Core.Entities;
using STS.Domain.Core.Entities.Feature;

namespace STS.Domain.Service.Feature;
public class FeedBackService(IFeedBackRepository feedBackRepository) : IFeedBackService
{
    public async Task<Result<FeedBack>> Add(AddFeedBackDto model, CancellationToken cancellationToken)
    {
        return await feedBackRepository.Add(model, cancellationToken);
    }

    public async Task<Result<bool>> Update(UpdtaFeedBackDto model, CancellationToken cancellationToken)
    {
        return await feedBackRepository.Update(model, cancellationToken);
    }

    public async Task<Result<bool>> Delete(int id, CancellationToken cancellationToken)
    {
        return await feedBackRepository.Delete(id, cancellationToken);
    }

    public async Task<Result<FeedBackDto>> GetBy(int id, CancellationToken cancellationToken)
    {
        return await feedBackRepository.GetBy(id, cancellationToken);
    }

    public async Task<Result<IEnumerable<FeedBackDto>>> GetAll(CancellationToken cancellationToken)
    {
        return await feedBackRepository.GetAll(cancellationToken);
    }

    public async Task<Result<bool>> Accept(int feedBackId, CancellationToken cancellationToken)
    {
        return await feedBackRepository.Accept(feedBackId, cancellationToken);
    }

    public async Task<Result<bool>> Reject(int feedBackId, CancellationToken cancellationToken)
    {
        return await feedBackRepository.Reject(feedBackId, cancellationToken);
    }

    public async Task<Result<bool>> SoftDelete(int feedBackId, CancellationToken cancellationToken)
    {
        return await feedBackRepository.SoftDelete(feedBackId, cancellationToken);
    }

    public async Task<Result<IEnumerable<FeedBackDto>>> GetAllBy(int ExpertId, CancellationToken cancellationToken) 
        => await feedBackRepository.GetAllBy(ExpertId, cancellationToken);
}
