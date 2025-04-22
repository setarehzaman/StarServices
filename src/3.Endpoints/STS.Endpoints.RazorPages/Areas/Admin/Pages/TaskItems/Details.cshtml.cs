using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using STS.Domain.Core.Contracts.AppService;
using STS.Domain.Core.Dtos.ApplicationUser;
using STS.Domain.Core.Dtos.TaskItemDtos;

namespace STS.Endpoints.RazorPages.Areas.Admin.Pages.TaskItems
{
    [Authorize(Roles = "Admin")]
    public class DetailsModel(ITaskItemAppService taskItemAppService) : PageModel
    {
        [BindProperty]
        public TaskItemDto TaskItem { get; set; }
        public async Task OnGet(int id, CancellationToken cancellationToken)
        {
            var result = await taskItemAppService.GetBy(id, cancellationToken);
            TaskItem = result.Data!;
        }
    }
}
