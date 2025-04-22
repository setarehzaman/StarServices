using STS.Domain.Core.Dtos.SuggestionDtos;
using STS.Domain.Core.Entities;
using STS.Domain.Core.Entities.Feature;

namespace STS.Domain.Core.Contracts.AppService;
public interface ISuggestionAppService
{
    Task<Result<Suggestion>> Add(AddSuggestionDto model, CancellationToken cancellationToken);
    Task<Result<bool>> Accept(int suggestionId, CancellationToken cancellationToken);
    Task<Result<bool>> Reject(int suggestionId, CancellationToken cancellationToken);
    Task<Result<IEnumerable<SuggestionDto>>> GetAllSuggestionsPerOrder(int orderId, CancellationToken cancellationToken);
    Task<Result<bool>> SubmitSuggestion(int suggestionId, int orderId, CancellationToken cancellationToken);
    Task<Result<IEnumerable<SuggestionDto>>> GetAllBy(int expertId, CancellationToken cancellationToken);
}
