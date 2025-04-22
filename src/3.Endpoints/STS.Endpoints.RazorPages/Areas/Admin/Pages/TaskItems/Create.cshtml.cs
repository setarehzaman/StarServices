using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using STS.Domain.Core.Contracts.AppService;
using STS.Domain.Core.Dtos.Category;
using STS.Domain.Core.Dtos.TaskItemDtos;
using STS.Domain.Core.Entities.Category;

namespace STS.Endpoints.RazorPages.Areas.Admin.Pages.TaskItems
{
    [Authorize(Roles = "Admin")]
    public class CreateModel(ITaskItemAppService taskItemAppService , ISubCategoryAppService subCategoryAppService) : PageModel
    {
        [BindProperty]
        public TaskItemDto InputModel { get; set; }

        [BindProperty] public IEnumerable<SubCategoryDto> SubCategories { get; set; }
        public async Task OnGet(CancellationToken cancellationToken)
        {
            var subCategoriesResult = await subCategoryAppService.GetAll(cancellationToken);
            SubCategories = subCategoriesResult.Data!;
        }
        public async Task<IActionResult> OnPost(CancellationToken cancellationToken)
        {
            var result = await taskItemAppService.Add(InputModel, cancellationToken);

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
