using STS.Domain.Core.Dtos.ClientDtos;
using STS.Domain.Core.Entities;
using STS.Domain.Core.Entities.User;

namespace STS.Domain.Core.Contracts.Repository;
public interface IClientRepository
{
    Task<Result<ApplicationUser>> Add(AddClientDto model, CancellationToken cancellationToken);
    Task<Result<bool>> Update(UpdateClientDto model, CancellationToken cancellationToken);
    Task<Result<bool>> Delete(int id, CancellationToken cancellationToken);
    Task<Result<ClientDto>> GetBy(int id, CancellationToken cancellationToken);
    Task<Result<IEnumerable<ClientDto>>> GetAll(CancellationToken cancellationToken);
    Task<Result<bool>> ChangePassword(int clientId, string newPassword, CancellationToken cancellationToken);
}
