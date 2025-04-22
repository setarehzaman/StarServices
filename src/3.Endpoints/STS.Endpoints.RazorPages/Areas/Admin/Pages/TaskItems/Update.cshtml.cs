using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using STS.Domain.AppService.Category;
using STS.Domain.Core.Contracts.AppService;
using STS.Domain.Core.Dtos.Category;
using STS.Domain.Core.Dtos.TaskItemDtos;
using STS.Domain.Core.Entities;

namespace STS.Endpoints.RazorPages.Areas.Admin.Pages.TaskItems
{
    [Authorize(Roles = "Admin")]
    public class UpdateModel(ITaskItemAppService taskItemAppService,ISubCategoryAppService subCategoryAppService) : PageModel
    {
        [BindProperty]
        public TaskItemDto TaskItem { get; set; }

        [BindProperty] public IEnumerable<SubCategoryDto> SubCategories { get; set; }

        public async Task OnGet(int id, CancellationToken cancellationToken)
        {
            var subCategoriesResult = await subCategoryAppService.GetAll(cancellationToken);
            SubCategories = subCategoriesResult.Data!;

            var result = await taskItemAppService.GetBy(id, cancellationToken);
            TaskItem = result.Data!;
        }

        public async Task<IActionResult> OnPost(CancellationToken cancellationToken)
        {
            var result = await taskItemAppService.Update(TaskItem, cancellationToken);

            if (result.IsSuccess) return RedirectToPage("Index");

            ModelState.AddModelError(string.Empty, result.Message!);
            return Page();
        }
    }
}
