using Microsoft.AspNetCore.Http;
using STS.Domain.Core.Contracts.Repository;
using STS.Domain.Core.Contracts.Service;
using System.Net.Http.Headers;
using STS.Domain.Core.Entities.BaseEntities;
using STS.Domain.Core.Entities;

namespace STS.Domain.Service.BaseData;
public class BaseDataService(IBaseDataRepository baseDataRepository) : IBaseDataService
{
    public async Task<string> UploadImage(IFormFile FormFile, string folderName, CancellationToken cancellation)
    {
        string filePath;
        string fileName;
        if (FormFile != null)
        {
            fileName = Guid.NewGuid().ToString() +
                       ContentDispositionHeaderValue.Parse(FormFile.ContentDisposition).FileName.Trim('"');
            filePath = Path.Combine($"wwwroot{folderName}", fileName);
            try
            {
                using (var stream = File.Create(filePath))
                {
                    await FormFile.CopyToAsync(stream, cancellation);
                }
            }
            catch
            {
                throw new Exception("Upload files operation failed");
            }
            return $"{folderName}/{fileName}";
        }
        else
            fileName = "";

        return fileName;
    }

    public async Task<List<string>> UploadImages(List<IFormFile> FormFiles, string folderName, CancellationToken cancellationToken)
    {
        List<string> fileAddresses = new List<string>();
        string filePath;
        string fileName;

        if (FormFiles is not null)
        {
            foreach (var formFile in FormFiles)
            {
                if (formFile != null)
                {
                    fileName = Guid.NewGuid().ToString() +
                               ContentDispositionHeaderValue.Parse(formFile.ContentDisposition).FileName.Trim('"');
                    filePath = Path.Combine($"wwwroot{folderName}", fileName);
                    try
                    {
                        using (var stream = File.Create(filePath))
                        {
                            await formFile.CopyToAsync(stream, cancellationToken);
                        }
                    }
                    catch
                    {
                        throw new Exception("Upload files operation failed");
                    }
                    fileAddresses.Add($"{folderName}/{fileName}");
                }
                else
                    fileName = "";
            }
        }
        return fileAddresses;
    }

    public async Task<Result<IEnumerable<City>>> GetAllCities(CancellationToken cancellationToken)
        => await baseDataRepository.GetAllCities(cancellationToken);

    public async Task<Result<bool>> AddOrderImages(List<string> imageAddresses, int orderId, CancellationToken cancellationToken)
    {
        return await baseDataRepository.AddOrderImages(imageAddresses, orderId, cancellationToken);
    }
}
