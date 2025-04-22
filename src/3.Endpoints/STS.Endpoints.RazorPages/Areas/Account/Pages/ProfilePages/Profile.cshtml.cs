using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using STS.Domain.Core.Contracts.AppService;
using STS.Domain.Core.Dtos.ApplicationUser;
using STS.Endpoints.RazorPages.Extensions;

namespace STS.Endpoints.RazorPages.Areas.Account.Pages.ProfilePages
{
    [Authorize(Roles = "Client,Expert")]
    public class ProfileModel(IApplicationUserAppService applicationUserAppService) : PageModel
    {
        [BindProperty]
        public ApplicationUserDto UserModel { get; set; }
        public async Task OnGet(CancellationToken cancellationToken)
        {
            var userId = User.GetUserId();

            var result = await applicationUserAppService.GetBy(int.Parse(userId), cancellationToken);
            UserModel = result.Data;
        }

    }
}
