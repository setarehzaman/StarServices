using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using STS.Domain.Core.Contracts.AppService;
using STS.Domain.Core.Dtos.FeedBack;
using STS.Endpoints.RazorPages.Extensions;

namespace STS.Endpoints.RazorPages.Areas.Account.Pages.ProfilePages
{
    [Authorize(Roles = "Expert")]
    public class ExpertFeedBacksModel (IFeedBackAppService feedBackAppService): PageModel
    {
        [BindProperty]
        public IEnumerable<FeedBackDto> FeedBacks { get; set; }
        public async Task  OnGet(CancellationToken cancellationToken)
        {
            var expertId = int.Parse(User.GetExpertId());
            var result = await feedBackAppService.GetAllBy(expertId, cancellationToken);

            if (result.IsSuccess)
            {
                FeedBacks = result.Data!;
            }
            else
            {
                ModelState.AddModelError(string.Empty, result.Message!);
            }
        }
    }
}
