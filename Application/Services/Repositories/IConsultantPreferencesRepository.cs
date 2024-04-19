using Core.Persistence.Repositories;
using Domain.Entities;

namespace Application.Services.Repositories;

public interface IConsultantPreferencesRepository : IAsyncRepository<ConsultantPreferences, Guid>
{
}