using STS.Domain.Core.Contracts.Repository;
using STS.Domain.Core.Contracts.Service;
using STS.Domain.Core.Dtos.SuggestionDtos;
using STS.Domain.Core.Entities;
using STS.Domain.Core.Entities.Feature;

namespace STS.Domain.Service.Feature;
public class SuggestionService(ISuggestionRepository suggestionRepository) : ISuggestionService
{
    public async Task<Result<Suggestion>> Add(AddSuggestionDto model, CancellationToken cancellationToken)
    {
        return await suggestionRepository.Add(model, cancellationToken);
    }

    public async Task<Result<bool>> Update(UpdateSuggestionDto model, CancellationToken cancellationToken)
    {
        return await suggestionRepository.Update(model, cancellationToken);
    }

    public async Task<Result<bool>> Delete(int id, CancellationToken cancellationToken)
    {
        return await suggestionRepository.Delete(id, cancellationToken);
    }

    public async Task<Result<SuggestionDto>> GetBy(int id, CancellationToken cancellationToken)
    {
        return await suggestionRepository.GetBy(id, cancellationToken);
    }

    public async Task<Result<IEnumerable<SuggestionDto>>> GetAll(CancellationToken cancellationToken)
    {
        return await suggestionRepository.GetAll(cancellationToken);
    }

    public async Task<Result<bool>> Accept(int suggestionId, CancellationToken cancellationToken)
    {
        return await suggestionRepository.Accept(suggestionId, cancellationToken);
    }

    public async Task<Result<bool>> Reject(int suggestionId, CancellationToken cancellationToken)
    {
        return await suggestionRepository.Reject(suggestionId, cancellationToken);
    }

    public async Task<Result<IEnumerable<SuggestionDto>>> GetAllSuggestionsPerOrder(int orderId, CancellationToken cancellationToken)
    {
        return await suggestionRepository.GetAllSuggestionsPerOrder(orderId, cancellationToken);
    }

    public async Task<Result<double>> GetAcceptedSuggestionPrice(int orderId, CancellationToken cancellationToken)
    {
        return await suggestionRepository.GetAcceptedSuggestionPrice(orderId, cancellationToken);
    }

    public async Task<Result<IEnumerable<SuggestionDto>>> GetAllBy(int expertId, CancellationToken cancellationToken)
    {
        return await suggestionRepository.GetAllBy(expertId, cancellationToken);
    }
}
