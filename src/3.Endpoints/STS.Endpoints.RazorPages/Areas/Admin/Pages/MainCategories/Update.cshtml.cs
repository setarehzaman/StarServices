using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using STS.Domain.Core.Contracts.AppService;
using STS.Domain.Core.Dtos.Category;

namespace STS.Endpoints.RazorPages.Areas.Admin.Pages.MainCategories
{
    [Authorize(Roles = "Admin")]
    public class UpdateModel(IMainCategoryAppService mainCategoryAppService) : PageModel
    {
        [BindProperty]
        public MainCategoryDto MainCategory { get; set; }

        public async Task OnGet(int id, CancellationToken cancellationToken)
        {
            var result = await mainCategoryAppService.GetBy(id, cancellationToken);
            MainCategory = result.Data!;
        }

        public async Task<IActionResult> OnPost(CancellationToken cancellationToken)
        {
            var result = await mainCategoryAppService.Update(MainCategory, cancellationToken);


            if (result.IsSuccess) return RedirectToPage("Index");

            ModelState.AddModelError(string.Empty, result.Message!);
            return Page();
        }
    }
}
