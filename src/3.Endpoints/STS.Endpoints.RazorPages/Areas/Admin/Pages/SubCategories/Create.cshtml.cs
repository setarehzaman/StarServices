using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using STS.Domain.AppService.Category;
using STS.Domain.Core.Contracts.AppService;
using STS.Domain.Core.Dtos.Category;

namespace STS.Endpoints.RazorPages.Areas.Admin.Pages.SubCategories
{
    [Authorize(Roles = "Admin")]
    public class CreateModel(ISubCategoryAppService subCategoryAppService, IMainCategoryAppService mainCategoryAppService) : PageModel
    {
        [BindProperty]
        public SubCategoryDto InputModel { get; set; }

        [BindProperty] public IEnumerable<MainCategoryDto> MainCategories { get; set; }
        public async Task OnGet(CancellationToken cancellationToken)
        {
            var mainCategoriesResult = await mainCategoryAppService.GetAll(cancellationToken);
            MainCategories = mainCategoriesResult.Data!;
        }
        public async Task<IActionResult> OnPost(CancellationToken cancellationToken)
        {
            var result = await subCategoryAppService.Add(InputModel, cancellationToken);

            if (result.IsSuccess)
            {
                TempData["SuccessMessage"] = "عملیات موردنظر شما با موفقیت انجام شد.";
                return RedirectToPage("Create");
            }
            else
            {
                TempData["ErrorMessage"] = "عملیات مورد نظر شما ناموفق بود.";
                return Page();
            }
        }
    }
}