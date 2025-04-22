using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using STS.Domain.Core.Contracts.AppService;
using STS.Domain.Core.Dtos.OrderDtos;
using STS.Domain.Core.Entities.Feature;

namespace STS.Endpoints.RazorPages.Areas.Admin.Pages.Orders
{
    [Authorize(Roles = "Admin")]
    public class OrderDetailsModel(IOrderAppService orderAppService) : PageModel
    {
        [BindProperty]
        public OrderDto OrderModel { get; set; }

        [BindProperty]
        public IEnumerable<OrderStatus> OrderStatuses { get; set; } = new List<OrderStatus>();  

        public async Task OnGet(int id, CancellationToken cancellationToken)
        {
            var orderStatuses = await orderAppService.GetAllOrderStatuses(cancellationToken);
            OrderStatuses = orderStatuses.Data!;

            var order = await orderAppService.GetBy(id, cancellationToken);
            OrderModel = order.Data!;
        }

        public async Task<IActionResult> OnPost(int id, CancellationToken cancellationToken)
        {
            var changeStatusResult = await orderAppService
                .ChangeStatus(id, OrderModel.OrderStatus, cancellationToken);

            TempData["Message"] = changeStatusResult.Message;
            TempData["IsSuccess"] = changeStatusResult.IsSuccess;

            if (!changeStatusResult.IsSuccess)
            {
                var orderStatuses = await orderAppService.GetAllOrderStatuses(cancellationToken);
                OrderStatuses = orderStatuses.Data!;

                var order = await orderAppService.GetBy(id, cancellationToken);
                OrderModel = order.Data!;

                return Page();
            }

            return RedirectToPage("Index");
        }

    }
}
