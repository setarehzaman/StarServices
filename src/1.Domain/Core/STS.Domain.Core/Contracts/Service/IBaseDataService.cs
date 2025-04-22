using Microsoft.AspNetCore.Http;
using STS.Domain.Core.Entities;
using STS.Domain.Core.Entities.BaseEntities;
namespace STS.Domain.Core.Contracts.Service;
public interface IBaseDataService
{
    Task<string> UploadImage(IFormFile FormFile, string folderName, CancellationToken cancellationToken);
    Task<List<string>> UploadImages(List<IFormFile> FormFiles, string folderName, CancellationToken cancellationToken);
    Task<Result<IEnumerable<City>>> GetAllCities(CancellationToken cancellationToken);
    Task<Result<bool>> AddOrderImages(List<string> imageAddresses, int orderId, CancellationToken cancellationToken);
}
