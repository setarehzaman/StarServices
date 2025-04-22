using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using STS.Domain.Core.Entities.User;
using STS.Domain.Core.Contracts.Service;
using STS.Domain.Core.Contracts.AppService;
using STS.Domain.Core.Dtos.ApplicationUser;
using STS.Domain.Core.Entities;
using STS.Domain.Core.Enums;
using Microsoft.Extensions.Logging;

namespace STS.Domain.AppService.User;
public class ApplicationUserAppService(IApplicationUserService userService,
    SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager,
    IBaseDataService baseDataService, ILogger<ApplicationUserAppService> logger) : IApplicationUserAppService
{
    public async Task<Result<IdentityResult>> Register(ApplicationUserDto model, CancellationToken cancellationToken)
    {
        string role = string.Empty;
        string folderImagesPath = "/Images/Profiles";

        var user = new ApplicationUser()
        {
            UserName = model.Email,
            Email = model.Email,
            CityId = model.CityId,
            FirstName = model.FirstName,
            LastName = model.LastName,
            Address = model.Address,
            PhoneNumber = model.PhoneNumber,
            RegisteredAt = DateTime.Now,
            //LockoutEnd = DateTime.Now.AddYears(5),
            //LockoutEnabled = true,
        };

        if (model.RoleId == (int)UserRoleEnum.Client)
        {
            role = "Client";
            user.Client = new Client();
        }

        if (model.RoleId == (int)UserRoleEnum.Expert)
        {
            role = "Expert";
            user.Expert = new Expert();
        }

        if (model.RoleId == (int)UserRoleEnum.Admin)
        {
            role = "Admin";
        }

        var createUser = await userManager.CreateAsync(user, model.Password);

        if (createUser.Succeeded)
        {

            await userManager.AddToRoleAsync(user, role);

            if (model.ProfileImage is not null)
            {
                var fileAddress = await baseDataService.UploadImage(model.ProfileImage!, folderImagesPath, cancellationToken);
                await userService.SetProfilePath(user.Id, fileAddress, cancellationToken);
            }

            if (model.RoleId == (int)UserRoleEnum.Expert)
            {
                await userManager.AddClaimAsync(user, new Claim("ExpertId", user.Expert.Id.ToString()));
            }

            if (model.RoleId == (int)UserRoleEnum.Client)
            {
                await userManager.AddClaimAsync(user, new Claim("ClientId", user.Client.Id.ToString()));
            }

        }

        var result = new Result<IdentityResult>() { IsSuccess = true, Data = createUser };
        return result;
    }

    public async Task<Result<SignInResult>> Login(string username, string password)
    {
        var result = await signInManager.PasswordSignInAsync(username, password, true, false);
        if (!result.Succeeded)
        {
            logger.LogInformation("ورود انجام نشد!");
        }
        return new Result<SignInResult>() { IsSuccess = true, Data = result };
    }

    public async Task<Result<ApplicationUser>> Add(ApplicationUserDto model, CancellationToken cancellationToken)
    => await userService.Add(model, cancellationToken);

    public Task<Result<List<ApplicationUserDto>>> GetAll(CancellationToken cancellationToken)
        => userService.GetAll(cancellationToken);

    public Task<Result<ApplicationUserDto>> GetBy(int id, CancellationToken cancellationToken)
        => userService.GetBy(id, cancellationToken);

    public Task<Result<int>> GetCount(CancellationToken cancellationToken)
        => userService.GetCount(cancellationToken);

    public async Task<Result<bool>> Update(ApplicationUserDto model, CancellationToken cancellationToken)
    {
        string folderImagesPath = "/Images/Profiles";

        if (model.ProfileImage is not null)
        {
            model.ImgProfilePath = await baseDataService.UploadImage(model.ProfileImage!, folderImagesPath, cancellationToken);
        }

        return await userService.Update(model, cancellationToken);
    }

    public async Task<Result<bool>> SoftDelete(int userId, CancellationToken cancellationToken)
    {
        return await userService.SoftDelete(userId, cancellationToken);
    }

    public async Task<Result<ProfileDetailDto>> GetImageProfilePath(int userId, CancellationToken cancellationToken)
    {
        return await userService.GetImageProfilePath(userId, cancellationToken);
    }

    public async Task<Result<bool>> Reject(int userId, CancellationToken cancellationToken)
        => await userService.Reject(userId, cancellationToken);

    public async Task<Result<bool>> Accept(int userId, CancellationToken cancellationToken)
        => await userService.Accept(userId, cancellationToken);
}
