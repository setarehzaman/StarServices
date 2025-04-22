using STS.Domain.Core.Contracts.AppService;
using STS.Domain.Core.Contracts.Service;
using STS.Domain.Core.Dtos.FeedBack;
using STS.Domain.Core.Entities;
using STS.Domain.Core.Entities.Feature;

namespace STS.Domain.AppService.Feature;
public class FeedBackAppService(IFeedBackService feedBackService) : IFeedBackAppService
{
    public async Task<Result<bool>> Accept(int feedBackId, CancellationToken cancellationToken) 
        => await feedBackService.Accept(feedBackId, cancellationToken);

    public async Task<Result<FeedBack>> Add(AddFeedBackDto model, CancellationToken cancellationToken)
    {
        return await feedBackService.Add(model, cancellationToken);   
    }

    public Task<Result<bool>> Delete(int id, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task<Result<IEnumerable<FeedBackDto>>> GetAll(CancellationToken cancellationToken)
        => await feedBackService.GetAll(cancellationToken);

    public async Task<Result<IEnumerable<FeedBackDto>>> GetAllBy(int ExpertId, CancellationToken cancellationToken) 
        => await feedBackService.GetAllBy(ExpertId, cancellationToken);

    public async Task<Result<FeedBackDto>> GetBy(int id, CancellationToken cancellationToken)
    {
        return await feedBackService.GetBy(id, cancellationToken);
    }

    public async Task<Result<bool>> Reject(int feedBackId, CancellationToken cancellationToken)
        => await feedBackService.Reject(feedBackId, cancellationToken);

    public Task<Result<bool>> SoftDelete(int feedBackId, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task<Result<bool>> Update(UpdtaFeedBackDto model, CancellationToken cancellationToken)
    {
        return await feedBackService.Update(model, cancellationToken);
    }
}
