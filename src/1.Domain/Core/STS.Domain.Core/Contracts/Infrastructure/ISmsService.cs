namespace STS.Domain.Core.Contracts.Infrastructure;
public interface ISmsService
{
    Task Send(string mobile, string message);
}