using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using STS.Domain.Core.Contracts.Repository;
using STS.Domain.Core.Dtos.ClientDtos;
using STS.Domain.Core.Entities;
using STS.Domain.Core.Entities.User;
using STS.Infrastructure.SqlServer.Common;

namespace STS.Infrastructure.Ef.Repositories.Repositories.User;
public class ClientRepository(AppDbContext dbContext) : IClientRepository
{
    public async Task<Result<ApplicationUser>> Add(AddClientDto model, CancellationToken cancellationToken)
    {
        try
        {
            var entity = new ApplicationUser();

            entity.UserName = model.UserName;
            entity.NormalizedUserName = model.UserName!.ToUpper();
            entity.Email = model.Email;
            entity.NormalizedEmail = model.Email!.ToUpper();
            entity.PasswordHash = model.Password;
            entity.CityId = model.CityId;
            entity.Client = new Client();

            await dbContext.Users.AddAsync(entity, cancellationToken);
            await dbContext.SaveChangesAsync(cancellationToken);

            await AssignedRoleToUser(entity.Id, cancellationToken);

            var result = new Result<ApplicationUser>() { IsSuccess = true, Data = entity };
            return result;

        }
        catch (Exception e)
        {
            return new Result<ApplicationUser> { IsSuccess = false, Message = e.Message };
        }

    }

    public async Task<Result<bool>> Delete(int id, CancellationToken cancellationToken)
    {
        try
        {
            var client = await dbContext.Users
                .FirstAsync(x => x.Client!.Id == id, cancellationToken);

            dbContext.Users.Remove(client);
            await dbContext.SaveChangesAsync(cancellationToken);

            return new Result<bool> { IsSuccess = true, Data = true };
        }
        catch (Exception e)
        {
            return new Result<bool> { IsSuccess = false, Message = e.Message };
        }
    }

    public async Task<Result<IEnumerable<ClientDto>>> GetAll(CancellationToken cancellationToken)
    {
        try
        {
            var client = await dbContext.Users.AsNoTracking()
                .Select(x => new ClientDto()
                {
                    UserName = x.UserName,
                    Email = x.Email,
                    CityId = x.CityId,
                    Balance = x.Balance,
                    CardNumber = x.CardNumber,
                    Address = x.Address,
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    ImageProfile = x.ImgProfilePath,
                    PhoneNumber = x.PhoneNumber,
                    Orders = x.Client!.Orders,
                })
                .ToListAsync(cancellationToken);

            return new Result<IEnumerable<ClientDto>> { IsSuccess = true, Data = client };
        }
        catch (Exception e)
        {

            return new Result<IEnumerable<ClientDto>> { IsSuccess = false, Message = e.Message };
        }
    }

    public async Task<Result<ClientDto>> GetBy(int id, CancellationToken cancellationToken)
    {
        try
        {
            var client = await dbContext.Users.AsNoTracking()
                .Where(x => x.Expert!.Id == id)
                .Select(x => new ClientDto()
                {
                    UserName = x.UserName,
                    Email = x.Email,
                    CityId = x.CityId,
                    Balance = x.Balance,
                    CardNumber = x.CardNumber,
                    Address = x.Address,
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    ImageProfile = x.ImgProfilePath,
                    PhoneNumber = x.PhoneNumber,
                    Orders = x.Client!.Orders,

                })
                .FirstOrDefaultAsync(cancellationToken);
            return new Result<ClientDto> { IsSuccess = true, Data = client };
        }
        catch (Exception e)
        {
            return new Result<ClientDto> { IsSuccess = false, Message = e.Message };
        }
    }

    public async Task<Result<bool>> Update(UpdateClientDto model, CancellationToken cancellationToken)
    {
        try
        {
            var client = await dbContext.Users.AsNoTracking()
                .FirstAsync(x => x.Client!.Id == model.Id, cancellationToken);

            client.PhoneNumber = model.PhoneNumber;
            client.CityId = model.CityId;
            client.FirstName = model.FirstName;
            client.LastName = model.LastName;
            client.Address = model.Address;
            client.CardNumber = model.CardNumber;
            client.Client!.Orders = model.Orders!;
            client.ImgProfilePath = model.ImageProfile;
            client.Balance = model.Balance;


            dbContext.Users.Update(client);
            await dbContext.SaveChangesAsync(cancellationToken);

            return new Result<bool> { IsSuccess = true, Data = true };
        }
        catch (Exception e)
        {
            return new Result<bool> { IsSuccess = false, Message = e.Message };
        }
    }

    public async Task<Result<bool>> ChangePassword(int clientId, string newPassword, CancellationToken cancellationToken)
    {
        try
        {
            var client = await dbContext.Users
                .FirstAsync(x => x.Expert!.Id == clientId, cancellationToken);

            client.PasswordHash = newPassword;

            dbContext.Users.Update(client);
            await dbContext.SaveChangesAsync(cancellationToken);

            return new Result<bool> { IsSuccess = true, Data = true };

        }
        catch (Exception e)
        {

            return new Result<bool> { IsSuccess = false, Message = e.Message };
        }

    }

    private async Task AssignedRoleToUser(int userId, CancellationToken cancellationToken)
    {
        var role = new IdentityUserRole<int>()
        {
            UserId = userId,
            RoleId = 2, // Client = 2
        };

        await dbContext.UserRoles.AddAsync(role, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);
    }
}