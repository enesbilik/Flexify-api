using Application.Services.Repositories;
using Core.Persistence.Repositories;
using Domain.Entities;
using Persistence.Contexts;

namespace Persistence.Repositories;

public class ConsultantPreferencesRepository : EfRepositoryBase<ConsultantPreferences, Guid, BaseDbContext>,
    IConsultantPreferencesRepository
{
    public ConsultantPreferencesRepository(BaseDbContext context) : base(context)
    {
    }
}