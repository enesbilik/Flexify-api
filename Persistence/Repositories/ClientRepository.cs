using Application.Services.Repositories;
using Core.Persistence.Repositories;
using Domain.Entities;
using Persistence.Contexts;

namespace Persistence.Repositories;

public class ClientRepository : EfRepositoryBase<Client, Guid, BaseDbContext>, IClientRepository
{
    public ClientRepository(BaseDbContext context) : base(context)
    {
    }
}