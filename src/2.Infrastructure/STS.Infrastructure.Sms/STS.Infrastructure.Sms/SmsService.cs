using Kavenegar;
using STS.Domain.Core.Entities.Configs;
using STS.Domain.Core.Contracts.Infrastructure;

namespace STS.Infrastructure.Sms;
public class SmsService (SiteSettings siteSettings) : ISmsService
{
    public async Task Send(string mobile, string message)
    {
        KavenegarApi kavenegar = new KavenegarApi(siteSettings.SmsConfiguration.ApiKey);

        // var result = await kavenegar.Send(siteSettings.SmsConfiguration.Sender, mobile, message);
    }
}