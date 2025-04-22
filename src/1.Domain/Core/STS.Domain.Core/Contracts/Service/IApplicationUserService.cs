using STS.Domain.Core.Dtos.ApplicationUser;
using STS.Domain.Core.Entities;
using STS.Domain.Core.Entities.User;

namespace STS.Domain.Core.Contracts.Service;
public interface IApplicationUserService
{
    Task<Result<bool>> Login(string userName, string password, CancellationToken cancellationToken);
    Task<Result<int>> GetCount(CancellationToken cancellationToken);
    Task<Result<List<ApplicationUserDto>>> GetAll(CancellationToken cancellationToken);
    Task<Result<ApplicationUserDto>> GetBy(int id, CancellationToken cancellationToken);
    Task<Result<ApplicationUser>> Add(ApplicationUserDto model, CancellationToken cancellationToken);
    Task<Result<bool>> Update(ApplicationUserDto model, CancellationToken cancellationToken);
    Task<Result<bool>> SetProfilePath(int userId, string profilePath, CancellationToken cancellationToken);
    Task<Result<bool>> SoftDelete(int userId, CancellationToken cancellationToken);
    Task<Result<ProfileDetailDto>> GetImageProfilePath(int userId, CancellationToken cancellationToken);
    Task<Result<bool>> Reject(int userId, CancellationToken cancellationToken);
    Task<Result<bool>> Accept(int userId, CancellationToken cancellationToken);
    Task<Result<double>> GetBalance(int id, CancellationToken cancellationToken);
    Task<Result<int>> GetUserIdByClientId(int userId, CancellationToken cancellationToken);
    Task<Result<int>> GetUserIdByExpertId(int userId, CancellationToken cancellationToken);
    Task<Result<bool>> Deposit(int userId, double balance, CancellationToken cancellationToken);
    Task<Result<bool>> Withdraw(int userId, double balance, CancellationToken cancellationToken);
}
