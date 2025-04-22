
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using STS.Domain.AppService.Category;
using STS.Domain.Core.Contracts.AppService;
using STS.Domain.Core.Dtos.TaskItemDtos;

namespace STS.Endpoints.RazorPages.Areas.Admin.Pages.TaskItems
{
    [Authorize(Roles = "Admin")]
    public class IndexModel(ITaskItemAppService taskItemAppService) : PageModel
    {
        public IEnumerable<TaskItemDto> TaskItems { get; set; } = new List<TaskItemDto>();
        public async Task OnGet(CancellationToken cancellationToken)
        {
            var result = await taskItemAppService.GetAll(cancellationToken);
            TaskItems = result.Data!;
        }
        public async Task<IActionResult> OnGetDelete(int id, CancellationToken cancellationToken)
        {
            var result = await taskItemAppService.SoftDelete(id, cancellationToken);

            if (result.IsSuccess) return RedirectToPage("Index");

            ModelState.AddModelError(string.Empty, result.Message!);
            return Page();
        }
    }
}
