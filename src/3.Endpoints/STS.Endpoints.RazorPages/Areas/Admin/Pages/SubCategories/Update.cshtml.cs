using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using STS.Domain.AppService.Category;
using STS.Domain.Core.Contracts.AppService;
using STS.Domain.Core.Dtos.Category;
using STS.Domain.Core.Entities;

namespace STS.Endpoints.RazorPages.Areas.Admin.Pages.SubCategories
{
    [Authorize(Roles = "Admin")]
    public class UpdateModel (ISubCategoryAppService subCategoryAppService , IMainCategoryAppService mainCategoryAppService): PageModel
    {
        [BindProperty]
        public SubCategoryDto SubCategory { get; set; }

        [BindProperty] public IEnumerable<MainCategoryDto> MainCategories { get; set; }

        public async Task OnGet(int id, CancellationToken cancellationToken)
        {
            var mainCategoriesResult = await mainCategoryAppService.GetAll(cancellationToken);
            MainCategories = mainCategoriesResult.Data!;

            var result = await subCategoryAppService.GetBy(id, cancellationToken);
            SubCategory = result.Data!;
        }

        public async Task<IActionResult> OnPost(CancellationToken cancellationToken)
        {
            var result = await subCategoryAppService.Update(SubCategory, cancellationToken);

            if (result.IsSuccess) return RedirectToPage("Index");

            ModelState.AddModelError(string.Empty, result.Message!);
            return Page();
        }
    }
}
