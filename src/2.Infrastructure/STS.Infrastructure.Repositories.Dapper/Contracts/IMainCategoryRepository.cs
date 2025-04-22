using STS.Domain.Core.Dtos.Category;
using STS.Domain.Core.Entities;
namespace STS.Infrastructure.Repositories.Dapper.Contracts;
public interface IMainCategoryRepository
{
    Task<Result<IEnumerable<MainCategoryDto>>> GetAllMainCategories(CancellationToken cancellationToken);
}
