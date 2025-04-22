using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using STS.Domain.Core.Contracts.AppService;
using STS.Domain.Core.Dtos.ApplicationUser;
using STS.Domain.Core.Entities.BaseEntities;

namespace STS.Endpoints.RazorPages.Areas.Admin.Pages.Users
{
    //[Authorize(Roles = "Admin")]
    public class UpdateModel(IApplicationUserAppService applicationUserAppService , IBaseDataAppService baseDataAppService) : PageModel
    {
        [BindProperty]
        public ApplicationUserDto UserModel { get; set; }

        [BindProperty] public IEnumerable<City> Cities { get; set; }


        public async Task OnGet(int id, CancellationToken cancellationToken)
        {
            var resultCities = await baseDataAppService.GetAllCities(cancellationToken);
            Cities = resultCities.Data!;

            var result = await applicationUserAppService.GetBy(id, cancellationToken);
            UserModel = result.Data!;
        }

        public async Task<IActionResult> OnPost(CancellationToken cancellationToken)
        {
            var result = await applicationUserAppService.Update(UserModel, cancellationToken);

            if (result.IsSuccess) return RedirectToPage("Index");

            ModelState.AddModelError(string.Empty, result.Message!);
            return Page();
        }
    }
}
