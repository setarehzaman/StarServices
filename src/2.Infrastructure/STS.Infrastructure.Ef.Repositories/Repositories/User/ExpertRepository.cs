using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using STS.Domain.Core.Contracts.Repository;
using STS.Domain.Core.Dtos.ExpertDtos;
using STS.Domain.Core.Entities;
using STS.Domain.Core.Entities.User;
using STS.Domain.Core.Enums;
using STS.Infrastructure.SqlServer.Common;
namespace STS.Infrastructure.Ef.Repositories.Repositories.User;
public class ExpertRepository(AppDbContext dbContext) : IExpertRepository
{
    public async Task<Result<ApplicationUser>> Add(AddExpertDto model, CancellationToken cancellationToken)
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
            entity.Expert = new Expert();

            await dbContext.Users.AddAsync(entity, cancellationToken);
            await dbContext.SaveChangesAsync(cancellationToken);

            await AssignedRoleToUser(entity.Id, cancellationToken);

            return new Result<ApplicationUser> { IsSuccess = true, Data = entity };

        }
        catch (Exception e)
        {
            return new Result<ApplicationUser> { IsSuccess = false, Message = e.Message };
        }

    }
    public async Task<Result<bool>> Update(UpdateExpertDto model, CancellationToken cancellationToken)
    {
        try
        {
            var expert = await dbContext.Users
                .FirstAsync(x => x.Expert!.Id == model.Id, cancellationToken);

            expert.PhoneNumber = model.PhoneNumber;
            expert.CityId = model.CityId;
            expert.FirstName = model.FirstName;
            expert.LastName = model.LastName;
            expert.Address = model.Address;
            expert.CardNumber = model.CardNumber;
            expert.ImgProfilePath = model.ImageProfile;
            expert.Expert!.Biography = model.Biography;
            expert.Expert!.Skills = model.Skills;
            expert.Balance = model.Balance;

            dbContext.Users.Update(expert);
            await dbContext.SaveChangesAsync(cancellationToken);

            return new Result<bool> { IsSuccess = true, Data = true };
        }
        catch (Exception e)
        {
            return new Result<bool> { IsSuccess = false, Message = e.Message };
        }

    }
    public async Task<Result<bool>> Delete(int id, CancellationToken cancellationToken)
    {
        try
        {
            var expert = await dbContext.Users
                .FirstAsync(x => x.Expert!.Id == id, cancellationToken);

            dbContext.Users.Remove(expert);
            await dbContext.SaveChangesAsync(cancellationToken);

            return new Result<bool> { IsSuccess = true, Data = true };

        }
        catch (Exception e)
        {
            return new Result<bool> { IsSuccess = false, Message = e.Message };
        }
    }
    public async Task<Result<ExpertDto>> GetBy(int id, CancellationToken cancellationToken)
    {
        try
        {
            var expert = await dbContext.Users.AsNoTracking()
                .Include(x => x.Expert).ThenInclude(x => x.Skills).ThenInclude(x => x.Task)
                .Where(x => x.Expert!.Id == id)
                .Select(x => new ExpertDto()
                {
                    UserName = x.UserName,
                    Email = x.Email,
                    CityId = x.CityId,
                    Balance = x.Balance,
                    CardNumber = x.CardNumber,
                    Address = x.Address,
                    Biography = x.Expert!.Biography,
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    ImageProfile = x.ImgProfilePath,
                    PhoneNumber = x.PhoneNumber,
                    Skills = x.Expert!.Skills,
                    Score = x.Expert!.Score,
                    Suggestions = x.Expert!.Suggestions,
                })
                .FirstOrDefaultAsync(cancellationToken);

            return new Result<ExpertDto> { IsSuccess = true, Data = expert };
        }
        catch (Exception e)
        {
            return new Result<ExpertDto> { IsSuccess = false, Message = e.Message };
        }
    }
    public async Task<Result<IEnumerable<ExpertDto>>> GetAll(CancellationToken cancellationToken)
    {
        try
        {
            var expert = await dbContext.Users.AsNoTracking()
                .Select(x => new ExpertDto()
                {
                    UserName = x.UserName,
                    Email = x.Email,
                    CityId = x.CityId,
                    Balance = x.Balance,
                    CardNumber = x.CardNumber,
                    Address = x.Address,
                    Biography = x.Expert!.Biography,
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    ImageProfile = x.ImgProfilePath,
                    PhoneNumber = x.PhoneNumber,
                    Skills = x.Expert!.Skills,
                    Score = x.Expert!.Score,
                    Suggestions = x.Expert!.Suggestions,
                })
                .ToListAsync(cancellationToken);

            return new Result<IEnumerable<ExpertDto>> { IsSuccess = true, Data = expert };
        }
        catch (Exception e)
        {
            return new Result<IEnumerable<ExpertDto>> { IsSuccess = false, Message = e.Message };
        }
    }
    public async Task<Result<bool>> ChangePassword(int expertId, string newPassword, CancellationToken cancellationToken)
    {
        try
        {
            var expert = await dbContext.Users
                .FirstAsync(x => x.Expert!.Id == expertId, cancellationToken);

            expert.PasswordHash = newPassword;

            dbContext.Users.Update(expert);
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
            RoleId = (int)UserRoleEnum.Expert
        };

        await dbContext.UserRoles.AddAsync(role, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);
    }
    public async Task<Result<bool>> UpdateSkills(int expertId, List<int> skillIds, CancellationToken cancellationToken)
    {
        try
        {
            var expert = await dbContext.Experts
                .Include(e => e.Skills)
                .FirstAsync(e => e.Id == expertId, cancellationToken);


            var skils = skillIds.Select(x => new ExpertSkills
            {
                ExpertId = expertId,
                TaskId = x,
            }).ToList();

            expert.Skills.Clear();

            expert.Skills.AddRange(skils);

            await dbContext.SaveChangesAsync(cancellationToken);

            return new Result<bool> { IsSuccess = true, Data = true };
        }
        catch (Exception e)
        {

            return new Result<bool> { IsSuccess = false, Message = e.Message };
        }
    }
    public async Task<Result<IEnumerable<int>>> GetSkillIdsBy(int expertId, CancellationToken cancellationToken)
    {
        try
        {
            var expert = await dbContext.Experts
                .Include(x=>x.Skills)
                .FirstAsync(x => x.Id == expertId, cancellationToken);

            var ids = expert.Skills.Select(x => x.TaskId);

            return new Result<IEnumerable<int>> { IsSuccess = true, Data = ids };

        }
        catch (Exception e)
        {
            return new Result<IEnumerable<int>> { IsSuccess = false, Message = e.Message };
        }


    }
}
