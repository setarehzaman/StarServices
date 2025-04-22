using STS.Domain.Core.Entities;
using STS.Domain.Core.Entities.BaseEntities;

namespace STS.Domain.Core.Contracts.AppService;
public interface IBaseDataAppService
{
    Task<Result<IEnumerable<City>>> GetAllCities(CancellationToken cancellationToken);
}
