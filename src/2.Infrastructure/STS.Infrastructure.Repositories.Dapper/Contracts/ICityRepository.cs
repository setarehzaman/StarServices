using STS.Domain.Core.Entities.BaseEntities;
using STS.Domain.Core.Entities;
namespace STS.Infrastructure.Repositories.Dapper.Contracts;
public interface ICityRepository
{
    Task<Result<IEnumerable<City>>> GetAllCities(CancellationToken cancellationToken);
}
