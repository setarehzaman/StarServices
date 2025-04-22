using Microsoft.AspNetCore.Identity;
using STS.Domain.Core.Dtos.ApplicationUser;
using STS.Domain.Core.Entities;
using STS.Domain.Core.Entities.User;

namespace STS.Domain.Core.Contracts.AppService;
public interface IApplicationUserAppService
{
    Task<Result<ApplicationUser>> Add(ApplicationUserDto model, CancellationToken cancellationToken);
    Task<Result<List<ApplicationUserDto>>> GetAll(CancellationToken cancellationToken);
    Task<Result<ApplicationUserDto>> GetBy(int id, CancellationToken cancellationToken); 
    Task<Result<bool>> Update(ApplicationUserDto model, CancellationToken cancellationToken);
    Task<Result<bool>> SoftDelete(int userId, CancellationToken cancellationToken);
    Task<Result<IdentityResult>> Register(ApplicationUserDto model, CancellationToken cancellationToken);
    Task<Result<SignInResult>> Login(string username, string password);
    Task<Result<int>> GetCount(CancellationToken cancellationToken);
    Task<Result<ProfileDetailDto>> GetImageProfilePath(int userId, CancellationToken cancellationToken);
    Task<Result<bool>> Reject(int userId, CancellationToken cancellationToken);
    Task<Result<bool>> Accept(int userId, CancellationToken cancellationToken);
}
