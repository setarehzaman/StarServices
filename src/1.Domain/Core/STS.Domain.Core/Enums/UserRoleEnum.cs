using System.ComponentModel.DataAnnotations;

namespace STS.Domain.Core.Enums
{
    public enum UserRoleEnum
    {
        [Display(Name = "ادمین")]
        Admin = 1,

        [Display(Name = "کاربر")]
        Client = 2,

        [Display(Name = "متخصص")]
        Expert = 3
    }
}
