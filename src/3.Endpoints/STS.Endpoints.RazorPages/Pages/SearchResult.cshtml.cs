using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using STS.Domain.Core.Contracts.AppService;
using STS.Domain.Core.Dtos.TaskItemDtos;
using static Microsoft.Extensions.Logging.EventSource.LoggingEventSource;

namespace STS.Endpoints.RazorPages.Pages
{
    public class SearchResultModel (ITaskItemAppService  taskItemAppService) : PageModel
    {
        [BindProperty]
        public IEnumerable<TaskItemDto> TaskItems { get; set; }

        [BindProperty]
        public string Keyword { get; set; }

        public void OnGet(string keyword)
        {

        }

        public async Task<IActionResult> OnPost(string keyword,CancellationToken cancellationToken)
        {
            Keyword = keyword;

            var result = await taskItemAppService.Search(keyword, cancellationToken);

            if(result.IsSuccess)
            {
                TaskItems = result.Data!;
            }

            return Page();
        }
    }
}
