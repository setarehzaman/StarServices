using STS.Domain.Core.Dtos.Category;
using STS.Domain.Core.Entities;
namespace STS.Infrastructure.Repositories.Dapper.Contracts;
public interface ISubCategoryRepository
{
    Task<Result<IEnumerable<SubCategoryDto>>> GetAllSubCategories(CancellationToken cancellationToken);
}
