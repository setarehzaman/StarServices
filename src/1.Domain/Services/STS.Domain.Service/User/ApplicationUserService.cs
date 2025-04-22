using Microsoft.AspNetCore.Identity;
using STS.Domain.Core.Contracts.Repository;
using STS.Domain.Core.Contracts.Service;
using STS.Domain.Core.Dtos.ApplicationUser;
using System.Data;
using Microsoft.EntityFrameworkCore;
using STS.Domain.Core.Entities.User;
using STS.Domain.Core.Entities;

namespace STS.Domain.Service.User;
public class ApplicationUserService(IApplicationUserRepository userRepository,
    UserManager<ApplicationUser> userManager) : IApplicationUserService
{
    public async Task<Result<ApplicationUser>> Add(ApplicationUserDto model, CancellationToken cancellationToken)
        => await userRepository.Add(model, cancellationToken);

    public async Task<Result<bool>> Update(ApplicationUserDto model, CancellationToken cancellationToken)
    {
        return await userRepository.Update(model, cancellationToken);
    }

    public async Task<Result<bool>> SetProfilePath(int userId, string profilePath, CancellationToken cancellationToken)
    {
        return await userRepository.SetProfilePath(userId, profilePath, cancellationToken);
    }

    public async Task<Result<bool>> SoftDelete(int userId, CancellationToken cancellationToken)
    {
        return await userRepository.SoftDelete(userId, cancellationToken);
    }

    public async Task<Result<ProfileDetailDto>> GetImageProfilePath(int userId, CancellationToken cancellationToken)
    {
        return await userRepository.GetImageProfilePath(userId, cancellationToken);
    }

    public async Task<Result<bool>> Login(string userName, string password, CancellationToken cancellationToken)
    {
        return await userRepository.Login(userName, password, cancellationToken);
    }

    public async Task<Result<int>> GetCount(CancellationToken cancellationToken)
    {
        return await userRepository.GetCount(cancellationToken);
    }

    public async Task<Result<List<ApplicationUserDto>>> GetAll(CancellationToken cancellationToken)
    {
        var users = await userRepository.GetAll(cancellationToken);

        var result = new Result<List<ApplicationUserDto>>() { IsSuccess = true };

        result.Data = users.Data!.Select(u => new ApplicationUserDto
        {
            Id = u.Id,
            FirstName = u.FirstName,
            LastName = u.LastName,
            UserName = u.UserName,
            PhoneNumber = u.PhoneNumber,
            Email = u.Email,
            RegisteredAt = u.RegisteredAt,
            City = u.City.Name,
            ImgProfilePath = u.ImgProfilePath,
            Address = u.Address,
            Balance = u.Balance,
            CardNumber = u.CardNumber,
            LockoutEnable = u.LockoutEnabled,
            Role = userManager.GetRolesAsync(u).Result.FirstOrDefault()!
        }).ToList();

        return result;
    }

    public async Task<Result<ApplicationUserDto>> GetBy(int id, CancellationToken cancellationToken)
    {
        var user = await userRepository.GetBy(id, cancellationToken);

        var result = new Result<ApplicationUserDto>() { IsSuccess = true };

        result.Data = new ApplicationUserDto
        {
            Id = user.Data!.Id,
            FirstName = user.Data!.FirstName,
            LastName = user.Data!.LastName,
            UserName = user.Data!.UserName,
            PhoneNumber = user.Data!.PhoneNumber,
            Email = user.Data!.Email,
            RegisteredAt = user.Data!.RegisteredAt,
            City = user.Data!.City.Name,
            ImgProfilePath = user.Data!.ImgProfilePath,
            Address = user.Data!.Address,
            Balance = user.Data!.Balance,
            CardNumber = user.Data!.CardNumber,
            CityId = user.Data.CityId,
            Role = userManager.GetRolesAsync(user.Data!).Result.FirstOrDefault()!
        };

        return result;
    }

    public async Task<Result<bool>> Reject(int userId, CancellationToken cancellationToken)
        => await userRepository.Reject(userId, cancellationToken);

    public async Task<Result<bool>> Accept(int userId, CancellationToken cancellationToken)
        => await userRepository.Accept(userId, cancellationToken);

    public async Task<Result<double>> GetBalance(int id, CancellationToken cancellationToken)
        => await userRepository.GetBalance(id, cancellationToken);

    public async Task<Result<int>> GetUserIdByClientId(int userId, CancellationToken cancellationToken)
        => await userRepository.GetUserIdByClientId(userId, cancellationToken);

    public async Task<Result<int>> GetUserIdByExpertId(int userId, CancellationToken cancellationToken)
        => await userRepository.GetUserIdByExpertId(userId, cancellationToken);

    public async Task<Result<bool>> Deposit(int userId, double balance, CancellationToken cancellationToken)
        => await userRepository.Deposit(userId, balance, cancellationToken);

    public async Task<Result<bool>> Withdraw(int userId, double balance, CancellationToken cancellationToken)
        => await userRepository.Withdraw(userId, balance, cancellationToken);

}
