using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using STS.Domain.Core.Contracts.Repository;
using STS.Domain.Core.Dtos.ApplicationUser;
using STS.Domain.Core.Entities;
using STS.Domain.Core.Entities.User;
using STS.Domain.Core.Enums;
using STS.Infrastructure.SqlServer.Common;

namespace STS.Infrastructure.Ef.Repositories.Repositories.User;
public class ApplicationUserRepository(AppDbContext dbContext) : IApplicationUserRepository
{
    public async Task<Result<bool>> Login(string userName, string password, CancellationToken cancellationToken)
    {
        try
        {
            var login = await dbContext.Users
                .AnyAsync(x => x.UserName == userName && x.PasswordHash == password, cancellationToken);

            var result = new Result<bool>() { IsSuccess = true, Data = login };
            return result;
        }
        catch (Exception e)
        {
            return new Result<bool> { IsSuccess = false, Message = e.Message };
        }
    }

    public async Task<Result<int>> GetCount(CancellationToken cancellationToken)
    {
        try
        {
            var count = await dbContext.Users.CountAsync(cancellationToken);
            var result = new Result<int>() { IsSuccess = true, Data = count };
            return result;
        }
        catch (Exception e)
        {
            return new Result<int> { IsSuccess = false, Message = e.Message };
        }
    }

    public async Task<Result<List<ApplicationUser>>> GetAll(CancellationToken cancellationToken)
    {
        try
        {
            var users = await dbContext.Users
                .AsNoTracking()
                .Include(x => x.City)
                .ToListAsync(cancellationToken);

            var result = new Result<List<ApplicationUser>>() { IsSuccess = true, Data = users };
            return result;
        }
        catch (Exception e)
        {
            return new Result<List<ApplicationUser>> { IsSuccess = false, Message = e.Message };
        }
    }
    public async Task<Result<ApplicationUser>> GetBy(int id, CancellationToken cancellationToken)
    {
        try
        {
            var users = await dbContext.Users
                .AsNoTracking()
                .Include(x => x.City)
                .FirstAsync(x => x.Id == id, cancellationToken);

            var result = new Result<ApplicationUser>() { IsSuccess = true, Data = users };
            return result;
        }
        catch (Exception e)
        {
            return new Result<ApplicationUser> { IsSuccess = false, Message = e.Message };
        }
    }
    public async Task<Result<ApplicationUser>> Add(ApplicationUserDto model, CancellationToken cancellationToken)
    {
        try
        {
            var user = new ApplicationUser();

            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
            user.UserName = model.UserName;
            user.PhoneNumber = model.PhoneNumber;
            user.Email = model.Email;
            user.CityId = model.CityId;
            user.Address = model.Address;
            user.Balance = model.Balance;
            user.Address = model.Address;
            user.CardNumber = model.CardNumber;
            user.ImgProfilePath = model.ImgProfilePath;
            user.RegisteredAt = DateTime.Now;


            await dbContext.Users.AddAsync(user, cancellationToken);
            await dbContext.SaveChangesAsync(cancellationToken);

            var result = new Result<ApplicationUser>() { IsSuccess = true, Data = user };
            return result;
        }
        catch (Exception ex)
        {
            return new Result<ApplicationUser> { IsSuccess = false, Message = ex.Message };
        }

    }
    public async Task<Result<bool>> Update(ApplicationUserDto model, CancellationToken cancellationToken)
    {
        try
        {

            var user = await dbContext.Users
                .FirstAsync(x => x.Id == model.Id, cancellationToken);

            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
            user.UserName = model.UserName ?? user.UserName;
            user.PhoneNumber = model.PhoneNumber;
            user.Email = model.Email ?? user.Email;
            user.CityId = model.CityId;
            user.Address = model.Address;
            user.ImgProfilePath = model.ImgProfilePath ?? user.ImgProfilePath;
            user.CardNumber = model.CardNumber ?? user.CardNumber;
            user.Balance = model.Balance == 0 ? 0 : model.Balance;

            await dbContext.SaveChangesAsync(cancellationToken);
            var result = new Result<bool>() { IsSuccess = true, Data = true };
            return result;
        }
        catch (Exception e)
        {
            return new Result<bool> { IsSuccess = false, Message = e.Message };
        }
    }
    public async Task<Result<bool>> SetProfilePath(int userId, string profilePath, CancellationToken cancellationToken)
    {
        try
        {
            var user = await dbContext
                .Users
                .FirstAsync(x => x.Id == userId, cancellationToken);

            user.ImgProfilePath = profilePath;

            await dbContext.SaveChangesAsync(cancellationToken);
            var result = new Result<bool>() { IsSuccess = true, Data = true };
            return result;
        }
        catch (Exception e)
        {
            return new Result<bool> { IsSuccess = false, Message = e.Message };
        }

    }
    public async Task<Result<bool>> SoftDelete(int userId, CancellationToken cancellationToken)
    {
        try
        {
            var user = await dbContext
                .Users
                .FirstAsync(x => x.Id == userId, cancellationToken);

            user.SoftDelete = true;

            await dbContext.SaveChangesAsync(cancellationToken);
            var result = new Result<bool>() { IsSuccess = true, Data = true };
            return result;

        }
        catch (Exception e)
        {
            return new Result<bool> { IsSuccess = false, Message = e.Message };
        }
    }

    public async Task<Result<ProfileDetailDto>> GetImageProfilePath(int userId, CancellationToken cancellationToken)
    {
        try
        {
            var user = await dbContext
                .Users.Select(x => new ProfileDetailDto
                {
                    Id = x.Id,
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    ProfileImagePath = x.ImgProfilePath
                }).FirstAsync(x => x.Id == userId, cancellationToken);

            return new Result<ProfileDetailDto>() { IsSuccess = true, Data = user };
        }
        catch (Exception e)
        {
            return new Result<ProfileDetailDto>() { IsSuccess = false };
        }
    }
    public async Task<Result<bool>> Reject(int userId, CancellationToken cancellationToken)
    {
        try
        {
            var entity = await dbContext.Users
                .Where(x => x.Id == userId)
                .FirstOrDefaultAsync(cancellationToken);

            entity!.LockoutEnabled = true;

            await dbContext.SaveChangesAsync(cancellationToken);

            var result = new Result<bool>() { IsSuccess = true, Data = true };
            return result;
        }
        catch (Exception e)
        {
            return new Result<bool> { IsSuccess = false, Message = e.Message };
        }
    }
    public async Task<Result<bool>> Accept(int userId, CancellationToken cancellationToken)
    {
        try
        {
            var entity = await dbContext.Users
                .Where(x => x.Id == userId)
                .FirstOrDefaultAsync(cancellationToken);

            entity!.LockoutEnabled = false;

            await dbContext.SaveChangesAsync(cancellationToken);

            var result = new Result<bool>() { IsSuccess = true, Data = true };
            return result;
        }
        catch (Exception e)
        {
            return new Result<bool> { IsSuccess = false, Message = e.Message };
        }
    }

    public async Task<Result<double>> GetBalance(int userId, CancellationToken cancellationToken)
    {
        try
        {
            var user = await dbContext.Users
                .FirstAsync(x => x.Id == userId, cancellationToken);

            return new Result<double>() { IsSuccess = true, Data = user.Balance };

        }
        catch (Exception e)
        {
            return new Result<double>() { IsSuccess = false, Message = e.Message };
        }
    }

    public async Task<Result<int>> GetUserIdByClientId(int id, CancellationToken cancellationToken)
    {
        try
        {
            var user = await dbContext.Users
                .Include(x => x.Client)
                .FirstAsync(x => x.Client!.Id == id, cancellationToken);

            return new Result<int>() { IsSuccess = true, Data = user.Id };
        }
        catch (Exception e)
        {
            return new Result<int>() { IsSuccess = false, Message = e.Message };

        }

    }

    public async Task<Result<int>> GetUserIdByExpertId(int id, CancellationToken cancellationToken)
    {
        try
        {
            var user = await dbContext.Users
                .Include(x => x.Expert)
                .FirstAsync(x => x.Expert!.Id == id, cancellationToken);

            return new Result<int>() { IsSuccess = true, Data = user.Id };
        }
        catch (Exception e)
        {
            return new Result<int>() { IsSuccess = false, Message = e.Message };

        }
    }

    public async Task<Result<bool>> Deposit(int userId, double balance, CancellationToken cancellationToken)
    {
        try
        {
            var user = await dbContext.Users
                .FirstAsync(x => x.Id == userId, cancellationToken);

            user.Balance += balance;

            await dbContext.SaveChangesAsync(cancellationToken);
            return new Result<bool>() { IsSuccess = true, Data = true };


        }
        catch (Exception e)
        {
            return new Result<bool>() { IsSuccess = false, Message = e.Message };

        }
    }

    public async Task<Result<bool>> Withdraw(int userId, double balance, CancellationToken cancellationToken)
    {
        try
        {
            var user = await dbContext.Users
                .FirstAsync(x => x.Id == userId, cancellationToken);

            user.Balance -= balance;

            await dbContext.SaveChangesAsync(cancellationToken);
            return new Result<bool>() { IsSuccess = true, Data = true };


        }
        catch (Exception e)
        {
            return new Result<bool>() { IsSuccess = false, Message = e.Message };

        }


    }
}
