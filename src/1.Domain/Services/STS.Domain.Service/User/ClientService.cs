using STS.Domain.Core.Contracts.Repository;
using STS.Domain.Core.Contracts.Service;
namespace STS.Domain.Service.User;
public class ClientService(IClientRepository clientRepository) : IClientService
{
}
