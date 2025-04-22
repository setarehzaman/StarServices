using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using STS.Domain.Core.Dtos.ApplicationUser;
using STS.Domain.Core.Contracts.AppService;
using Microsoft.AspNetCore.Authorization;
namespace STS.Endpoints.RazorPages.Areas.Admin.Pages.Users
{
    [Authorize(Roles = "Admin")]
    public class DetailsModel(IApplicationUserAppService userAppService) : PageModel
    {
        [BindProperty]
        public ApplicationUserDto UserModel { get; set; }
        public async Task OnGet(int id, CancellationToken cancellationToken)
        {
            var result = await userAppService.GetBy(id, cancellationToken);
            UserModel = result.Data!;
        }
    }
}