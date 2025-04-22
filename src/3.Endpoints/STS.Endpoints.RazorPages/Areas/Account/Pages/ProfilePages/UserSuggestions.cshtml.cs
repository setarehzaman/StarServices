using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.RazorPages;
using STS.Domain.Core.Contracts.AppService;
using STS.Domain.Core.Dtos.SuggestionDtos;
using STS.Endpoints.RazorPages.Extensions;

namespace STS.Endpoints.RazorPages.Areas.Account.Pages.ProfilePages
{
    [Authorize(Roles = "Client")]
    public class UserSuggestionsModel (ISuggestionAppService suggestionAppService) : PageModel
    {
        [BindProperty]
        public IEnumerable<SuggestionDto> suggestions { get; set; }
        public async Task OnGet(CancellationToken cancellationToken)
        {
            var expertId = int.Parse(User.GetExpertId());
            var result = await suggestionAppService.GetAllBy(expertId, cancellationToken);

            if(result.IsSuccess)
            {
                suggestions = result.Data!;
            }
            else
            {
                ModelState.AddModelError(string.Empty, result.Message!);
            }
        }
    }
}

