using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;
namespace STS.Domain.Core.Enums;
public enum OrderStatusEnum
{
    [Display(Name = "منتظر پیشنهاد متخصصان")]
    AwaitingSuggestions = 1,

    [Display(Name = "منتظر انتخاب متخصص")]
    SelectingExpert = 2,

    [Display(Name = "منتظر آمدن متخصص به محل شما")]
    ExpertEnRoute = 3,

    [Display(Name = "در دست انجام")]
    JobInProgress = 4,

    [Display(Name = "اتمام کار")]
    JobCompleted = 5,

    [Display(Name = "پرداخت شده")]
    Paid = 6,

    [Display(Name = "منقضی شده")]
    Expired = 7
}