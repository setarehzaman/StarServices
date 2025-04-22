using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using STS.Domain.Core.Contracts.AppService;
using STS.Domain.Core.Dtos.OrderDtos;
using STS.Domain.Core.Dtos.TaskItemDtos;
using STS.Endpoints.RazorPages.Extensions;
using System.Globalization;

namespace STS.Endpoints.RazorPages.Pages
{
    [Authorize(Roles = "Client")]
    public class SubmitOrderModel(IOrderAppService orderAppService,
        ITaskItemAppService taskItemAppService) : PageModel
    {
        public class InputModel
        {
            public int TaskId { get; set; }
            public double OrderedPrice { get; set; }
            public string? Description { get; set; }
            public DateTime ShamsiDoAt { get; set; }
            public List<IFormFile>? Images { get; set; }
        }

        [BindProperty] 
        public InputModel Input { get; set; }

        [BindProperty] 
        public TaskItemDto TaskItem { get; set; }

        public async Task OnGet(int id, CancellationToken cancellationToken)
        {
            var task = await taskItemAppService.GetBy(id, cancellationToken);
            if (task.IsSuccess) 
            {   
                TaskItem = task.Data!;
            }
        }

        public async Task<IActionResult> OnPost(CancellationToken cancellationToken)
        {
            var task = await taskItemAppService.GetBy(Input.TaskId, cancellationToken);
            if (task.IsSuccess)
            {
                TaskItem = task.Data!;
            }

            if (Input.OrderedPrice < TaskItem.BasePrice)
            {
                ModelState.AddModelError(string.Empty, "قیمت پیشنهادی شما از قیمت پایه ی این سرویس پایین تر است");
                return Page();
            }

            var pc = new PersianCalendar();

            var model = new AddOrderDto()
            {
                Description = Input.Description,
                OrderedPrice = Input.OrderedPrice,
                TaskId = Input.TaskId,
                ClientId = int.Parse(User.GetClientId()),
                DoAt = pc.ToDateTime(Input.ShamsiDoAt.Year, Input.ShamsiDoAt.Month, Input.ShamsiDoAt.Day, 0, 0, 0, 0)
            };

           var result =  await orderAppService.Add(model, Input.Images, cancellationToken);

           if (result.IsSuccess)
           {
               return RedirectToPage("/ProfilePages/UserOrders", new { area = "Account" });
           }
           else
           {

                ModelState.AddModelError(string.Empty, result.Message);
           }

            return Page();
        }
    }
}
