using STS.Domain.Core.Dtos.SuggestionDtos;
using STS.Domain.Core.Entities;
using STS.Domain.Core.Entities.Feature;

namespace STS.Domain.Core.Contracts.Repository;
public interface ISuggestionRepository
{
    Task<Result<Suggestion>> Add(AddSuggestionDto model, CancellationToken cancellationToken);
    Task<Result<bool>> Update(UpdateSuggestionDto model, CancellationToken cancellationToken);
    Task<Result<bool>> Delete(int id, CancellationToken cancellationToken);
    Task<Result<SuggestionDto>> GetBy(int id, CancellationToken cancellationToken);
    Task<Result<IEnumerable<SuggestionDto>>> GetAll(CancellationToken cancellationToken);
    Task<Result<bool>> Accept(int suggestionId, CancellationToken cancellationToken);
    Task<Result<bool>> Reject(int suggestionId, CancellationToken cancellationToken);
    Task<Result<IEnumerable<SuggestionDto>>> GetAllSuggestionsPerOrder(int orderId, CancellationToken cancellationToken);
    Task<Result<double>> GetAcceptedSuggestionPrice(int orderId, CancellationToken cancellationToken);
    Task<Result<IEnumerable<SuggestionDto>>> GetAllBy(int expertId, CancellationToken cancellationToken);
}