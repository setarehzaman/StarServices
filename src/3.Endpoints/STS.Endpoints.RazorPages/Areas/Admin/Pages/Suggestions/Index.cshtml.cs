using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using STS.Domain.Core.Contracts.AppService;
using STS.Domain.Core.Dtos.SuggestionDtos;
using STS.Domain.Core.Entities;

namespace STS.Endpoints.RazorPages.Areas.Admin.Pages.Suggestions
{
    public class IndexModel(ISuggestionAppService suggestionAppService) : PageModel
    {
        [BindProperty]
        public IEnumerable<SuggestionDto> Suggestions { get; set; } = new List<SuggestionDto>();
        public async Task OnGet(int id, CancellationToken cancellationToken)
        {
            var result = await suggestionAppService.GetAllSuggestionsPerOrder(id, cancellationToken);
            Suggestions = result.Data!;
        }

        [HttpGet]
        public async Task<IActionResult> OnGetAccept(int id, int OrderId, CancellationToken cancellationToken)
        {
            var result = await suggestionAppService.Accept(id, cancellationToken);

            if (result.IsSuccess) return RedirectToPage("Index", new { id = OrderId });

            ModelState.AddModelError(string.Empty, result.Message!);
            return Page();
        }

        [HttpGet]
        public async Task<IActionResult> OnGetReject(int id, int OrderId, CancellationToken cancellationToken)
        {
            var result = await suggestionAppService.Reject(id, cancellationToken);

            if (result.IsSuccess) return RedirectToPage("Index", new { id = OrderId });

            ModelState.AddModelError(string.Empty, result.Message!);
            return Page();
        }
    }
}
