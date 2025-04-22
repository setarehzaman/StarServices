using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using STS.Domain.Core.Contracts.AppService;
using STS.Domain.Core.Dtos.OrderDtos;
using STS.Endpoints.RazorPages.Extensions;

namespace STS.Endpoints.RazorPages.Pages
{
    [Authorize(Roles = "Expert")]
    public class ExpertIndexModel(IExpertAppService expertAppService) : PageModel
    {
        [BindProperty]
        public Dictionary<int, List<OrderDto>> Orders { get; set; }
        public async Task OnGet(CancellationToken cancellationToken)
        {
            if (User.IsInRole("Expert"))
            {
                var expertId = int.Parse(User.GetExpertId());

                var ordersResult = await expertAppService.GetOrdersBasedOnSkills(expertId, cancellationToken);

                var expertOrders = ordersResult.Data.ToList();

                Orders = expertOrders
                    .GroupBy(o => o.TaskId)
                    .ToDictionary(
                        g => g.Key,
                        g => g.OrderByDescending(o => o.CreateAt).Take(3).ToList()
                    );
            }
        }
    }
}
