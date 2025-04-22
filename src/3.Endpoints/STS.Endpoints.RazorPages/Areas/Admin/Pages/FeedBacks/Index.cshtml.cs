using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using STS.Domain.Core.Contracts.AppService;
using STS.Domain.Core.Dtos.FeedBack;

namespace STS.Endpoints.RazorPages.Areas.Admin.Pages.FeedBacks
{
    [Authorize(Roles = "Admin")]
    public class IndexModel(IFeedBackAppService feedBackAppService) : PageModel
    {
        public IEnumerable<FeedBackDto> FeedBacks { get; set; } = new List<FeedBackDto>();
        public async Task OnGet(CancellationToken cancellationToken)
        {
            var result = await feedBackAppService.GetAll(cancellationToken);
            FeedBacks = result.Data!;
        }

        [HttpGet]
        public async Task<IActionResult> OnGetAccept(int id, CancellationToken cancellationToken)
        {
            var result = await feedBackAppService.Accept(id, cancellationToken);

            if (result.IsSuccess) return RedirectToPage("Index");

            ModelState.AddModelError(string.Empty , result.Message!);
            return Page();

        }
        public async Task<IActionResult> OnGetReject(int id, CancellationToken cancellationToken)
        {
            var result = await feedBackAppService.Reject(id, cancellationToken);

            if (result.IsSuccess) return RedirectToPage("Index");

            ModelState.AddModelError(string.Empty, result.Message!);
            return Page();

        }
    }
}
