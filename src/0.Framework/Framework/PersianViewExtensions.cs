using STS.Domain.Core.Enums;

namespace Framework;
public static class PersianViewExtensions
{
    public static string ToPersianStatus(this bool value)
    {
        return value ? "فعال" : "غیرفعال";
    }
    public static string ToPersianStatusReverse(this bool value)
    {
        return value ? "غیرفعال" : "فعال";
    }

    public static string ToPersianRole(this string roleName)
    {
        return roleName switch
        {
            "Admin" => "مدیر",
            "Client" => "کاربر",
            "Expert" => "متخصص",
        };
    }
}


