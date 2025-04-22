using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using STS.Domain.Core.Contracts.AppService;
using STS.Domain.Core.Dtos.OrderDtos;
using STS.Domain.Core.Dtos.SuggestionDtos;
using STS.Endpoints.RazorPages.Extensions;
using System.Globalization;

namespace STS.Endpoints.RazorPages.Pages
{
    [Authorize(Roles = "Expert")]
    public class SubmitSuggestionModel(IOrderAppService orderAppService, ISuggestionAppService suggestionAppService) : PageModel
    {
        public class InputModel
        {
            public int OrderId { get; set; }
            public double SuggestedPrice { get; set; }
            public DateTime ShamsiDoAt { get; set; }
        }

        [BindProperty]
        public OrderDto Order { get; set; }

        [BindProperty]
        public InputModel Input { get; set; }

        [BindProperty]
        public bool ShowForm { get; set; }

        public async Task OnGet(int id, bool fromExpertProfile, CancellationToken cancellationToken)
        {
            var orderResult = await orderAppService.GetBy(id, cancellationToken);

            ShowForm = fromExpertProfile;
            if (!orderResult.IsSuccess)
            {
                ModelState.AddModelError(string.Empty, orderResult.Message!);
            }
            else
            {
                Order = orderResult.Data!;
            }

            Order = orderResult.Data!;
        }
        public async Task<IActionResult> OnPost(CancellationToken cancellationToken)
        {
            var pc = new PersianCalendar();

            var model = new AddSuggestionDto()
            {
                SuggestedPrice = Input.SuggestedPrice,
                OrderId = Input.OrderId,
                ExpertId = int.Parse(User.GetExpertId()),
                DoAt = pc.ToDateTime(Input.ShamsiDoAt.Year, Input.ShamsiDoAt.Month, Input.ShamsiDoAt.Day, 0, 0, 0, 0)
            };



            var result = await suggestionAppService.Add(model, cancellationToken);
            if (result.IsSuccess)
            {
                return RedirectToPage("/ProfilePages/UserSuggestions", new { area = "Account" });
            }
            else
            {

                ModelState.AddModelError(string.Empty, result.Message);
            }

            return Page();
        }

    }
}
