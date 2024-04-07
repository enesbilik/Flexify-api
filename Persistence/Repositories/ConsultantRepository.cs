using Application.Services.Repositories;
using Core.Persistence.Repositories;
using Domain.Entities;
using Persistence.Contexts;

namespace Persistence.Repositories;

public class ConsultantRepository : EfRepositoryBase<Consultant, Guid, BaseDbContext>,
    IConsultantRepository
{
    public ConsultantRepository(BaseDbContext context) : base(context)
    {
    }
}