using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using STS.Domain.Core.Contracts.AppService;
using STS.Domain.Core.Dtos.ApplicationUser;
using STS.Domain.Core.Entities.BaseEntities;

namespace STS.Endpoints.RazorPages.Areas.Admin.Pages.Users
{
    [Authorize(Roles = "Admin")]
    public class CreateModel(IApplicationUserAppService userAppService, IBaseDataAppService baseDataAppService)
        : PageModel
    {
        [BindProperty] public ApplicationUserDto InputModel { get; set; }

        [BindProperty] public IEnumerable<City> Cities { get; set; }

        public async Task OnGet(CancellationToken cancellationToken)
        {
            var result = await baseDataAppService.GetAllCities(cancellationToken);
            Cities = result.Data!;
        }

        public async Task<IActionResult> OnPost(CancellationToken cancellationToken)
        {
            var result = await userAppService.Register(InputModel, cancellationToken);

            if (result.IsSuccess) return RedirectToPage("Index");

            ModelState.AddModelError(string.Empty, result.Message!);
            return Page();
        }
    }
}
