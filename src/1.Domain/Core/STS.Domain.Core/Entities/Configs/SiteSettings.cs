
namespace STS.Domain.Core.Entities.Configs;

public class SiteSettings
{
    public ConnectionStrings connectionStrings { get; set; }
    public double FeeAmount { get; set; }
    public RedisConfiguration RedisConfiguration { get; set; }
    public SmsConfiguration SmsConfiguration { get; set; }

}
