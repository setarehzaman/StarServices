using STS.Domain.Core.Contracts.AppService;
using STS.Domain.Core.Contracts.Service;
using STS.Domain.Core.Entities;
using STS.Domain.Core.Entities.BaseEntities;

namespace STS.Domain.AppService.BaseData;
public class BaseDataAppService(IBaseDataService baseDataService) : IBaseDataAppService
{
    public async Task<Result<IEnumerable<City>>> GetAllCities(CancellationToken cancellationToken)
    {
       return await baseDataService.GetAllCities(cancellationToken);
    }
}
