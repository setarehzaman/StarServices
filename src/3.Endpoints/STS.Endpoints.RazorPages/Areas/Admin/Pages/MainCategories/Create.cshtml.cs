using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using STS.Domain.Core.Contracts.AppService;
using STS.Domain.Core.Dtos.ApplicationUser;
using STS.Domain.Core.Dtos.Category;

namespace STS.Endpoints.RazorPages.Areas.Admin.Pages.MainCategories
{
    [Authorize(Roles = "Admin")]
    public class CreateModel(IMainCategoryAppService mainCategoryAppService) : PageModel
    {
        [BindProperty]
        public MainCategoryDto InputModel { get; set; }
        public void OnGet()
        {
        }
        public async Task<IActionResult> OnPost(CancellationToken cancellationToken)
        {
            var result = await mainCategoryAppService.Add(InputModel, cancellationToken);

            if (result.IsSuccess) return RedirectToPage("Index");

            ModelState.AddModelError(string.Empty, result.Message!);
            return Page();
        }
    }
}
