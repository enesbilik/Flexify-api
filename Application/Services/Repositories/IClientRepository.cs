using Core.Persistence.Repositories;
using Domain.Entities;

namespace Application.Services.Repositories;

public interface IClientRepository : IAsyncRepository<Client, Guid>
{
}