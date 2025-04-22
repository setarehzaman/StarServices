using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using STS.Domain.AppService.Category;
using STS.Domain.Core.Contracts.AppService;
using STS.Domain.Core.Dtos.OrderDtos;

namespace STS.Endpoints.RazorPages.Areas.Admin.Pages.Orders
{
    [Authorize(Roles = "Admin")]
    public class IndexModel(IOrderAppService orderAppService) : PageModel
    {
        [BindProperty]
        public IEnumerable<OrderDto> Orders { get; set; } = new List<OrderDto>();   
        public async Task OnGet(CancellationToken cancellationToken)
        {
            var result = await orderAppService.GetAll(cancellationToken);
            Orders = result.Data!;
        }

        [HttpGet]
        public async Task<IActionResult> OnGetAccept(int id, CancellationToken cancellationToken)
        {
            var result = await orderAppService.Accept(id, cancellationToken);

            if (result.IsSuccess) return RedirectToPage("Index");

            ModelState.AddModelError(string.Empty, result.Message!);
            return Page();
        }

        [HttpGet]
        public async Task<IActionResult> OnGetReject(int id, CancellationToken cancellationToken)
        {
            var result = await orderAppService.Reject(id, cancellationToken);

            if (result.IsSuccess) return RedirectToPage("Index");

            ModelState.AddModelError(string.Empty, result.Message!);
            return Page();
        }
    }
}
