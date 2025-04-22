using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using STS.Domain.Core.Contracts.AppService;
using STS.Domain.Core.Dtos.FeedBack;
using STS.Domain.Core.Dtos.OrderDtos;
using STS.Domain.Core.Entities;
using STS.Domain.Core.Enums;
using STS.Endpoints.RazorPages.Extensions;

namespace STS.Endpoints.RazorPages.Areas.Account.Pages.ProfilePages
{
    [Authorize(Roles = "Client")]
    public class UserOrdersModel(IOrderAppService orderAppService, ISuggestionAppService suggestionAppService,
        IClientAppService clientAppService, IFeedBackAppService feedBackAppService) : PageModel
    {
        [BindProperty]
        public IEnumerable<OrderDto> InProgressorders { get; set; }

        [BindProperty]
        public IEnumerable<OrderDto> FinishedOrdes { get; set; }

        [BindProperty]
        public int Rating { get; set; }

        [BindProperty]
        public string? Comment { get; set; }

        [BindProperty]
        public int ExpertId { get; set; }   
        public async Task OnGet(CancellationToken cancellationToken)
        {
            if (User.IsInRole("Client"))
            {
                var clientId = int.Parse(User.GetClientId());
                var ordersResult = await orderAppService.GetAllByClientId(clientId, cancellationToken);
                if (ordersResult.Data is not null && ordersResult.Data.Any())
                {
                    InProgressorders = ordersResult.Data
                        .Where(x => x.OrderStatus != OrderStatusEnum.Paid)
                        .OrderByDescending(x => x.CreateAt);

                    FinishedOrdes = ordersResult.Data
                        .Where(x => x.OrderStatus == OrderStatusEnum.Paid);

                }
            }
        }

        [HttpGet]
        public async Task<IActionResult> OnGetSubmitSuggestion(int suggestionId, int orderId, CancellationToken cancellationToken)
        {
            var result = await suggestionAppService.SubmitSuggestion(suggestionId, orderId, cancellationToken);

            if (result.IsSuccess) return RedirectToPage("UserOrders");

            ModelState.AddModelError(string.Empty, result.Message!);
            return Page();
        }

        [HttpGet]
        public async Task<IActionResult> OnGetPayForSuggestion(int expertId, int orderId, CancellationToken cancellationToken)
        {
            var clientId = int.Parse(User.GetClientId());
            var userId = int.Parse(User.GetUserId());

            var result = await clientAppService.PayForSuggestion(orderId, expertId, clientId, userId, cancellationToken);

            if (result.IsSuccess) return RedirectToPage("UserOrders");

            ModelState.AddModelError(string.Empty, result.Message!);
            return Page();
        }

        public async Task<IActionResult> OnPost(CancellationToken cancellationToken)
        {
            var feedBack = new AddFeedBackDto()
            {
                Rating = Rating,
                Comment = Comment,
                ExpertId = ExpertId,
                ClientId = int.Parse(User.GetClientId())
            };

            var result = await feedBackAppService.Add(feedBack, cancellationToken);


            if (result.IsSuccess) return RedirectToPage("UserOrders");

            ModelState.AddModelError(string.Empty, result.Message!);
            return Page();
        }
    }
}
