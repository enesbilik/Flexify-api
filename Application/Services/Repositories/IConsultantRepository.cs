using Core.Persistence.Repositories;
using Domain.Entities;

namespace Application.Services.Repositories;

public interface IConsultantRepository : IAsyncRepository<Consultant, Guid>
{
}