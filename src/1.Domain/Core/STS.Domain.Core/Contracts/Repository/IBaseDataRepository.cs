using STS.Domain.Core.Entities;
using STS.Domain.Core.Entities.BaseEntities;

namespace STS.Domain.Core.Contracts.Repository;
public interface IBaseDataRepository
{
    Task<Result<IEnumerable<City>>> GetAllCities(CancellationToken cancellationToken);
    Task<Result<bool>> AddOrderImages(List<string> imageAddresses, int orderId ,  CancellationToken cancellationToken);
}
