using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using STS.Domain.Core.Contracts.AppService;
using STS.Domain.Core.Dtos.ExpertDtos;
using STS.Domain.Core.Dtos.FeedBack;

namespace STS.Endpoints.RazorPages.Areas.Account.Pages.ProfilePages
{
    public class ExpertBiographyModel(IExpertAppService expertAppService,
        IFeedBackAppService feedBackAppService) : PageModel
    {
        [BindProperty]
        public ExpertDto Expert { get; set; }

        [BindProperty]
        public IEnumerable<FeedBackDto> FeedBacks { get; set; } = new List<FeedBackDto>();  
        public async Task OnGet(int id, CancellationToken cancellationToken)
        {
            var expertResult = await expertAppService.GetBy(id, cancellationToken);
            Expert = expertResult.Data!;

            var feedBackResult = await feedBackAppService.GetAllBy(id, cancellationToken);
            FeedBacks = feedBackResult.Data!;

            if (FeedBacks.Any())
            {
                Expert.Score = (int)FeedBacks.Where(x=>x.IsAccepted).Average(x => x.Rating);
                Expert.FeedbackCount = FeedBacks.Count(x => x.IsAccepted && x.Rating != 0);
            }
        }

    }
}
