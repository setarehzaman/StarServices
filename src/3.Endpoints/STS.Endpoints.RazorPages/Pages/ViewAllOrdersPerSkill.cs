using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using STS.Domain.Core.Contracts.AppService;
using STS.Domain.Core.Dtos.OrderDtos;
using STS.Endpoints.RazorPages.Extensions;

namespace STS.Endpoints.RazorPages.Pages
{
    [Authorize(Roles = "Expert")]
    public class ViewAllOrdersPerSkillModel (IExpertAppService expertAppService) : PageModel
    {
        [BindProperty]
        public IEnumerable<OrderDto> Orders { get; set; }
        public async Task OnGet(int id,CancellationToken cancellationToken)
        {
            var expertId = int.Parse(User.GetExpertId());
            var ordersResult = await expertAppService.GetOrdersPerSkill(id, expertId, cancellationToken);

            if(ordersResult.IsSuccess)
            {
                Orders = ordersResult.Data!;
            }
        }
    }
}
