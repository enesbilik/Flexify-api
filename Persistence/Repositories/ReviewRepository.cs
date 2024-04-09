using Application.Services.Repositories;
using Core.Persistence.Repositories;
using Domain.Entities;
using Persistence.Contexts;

namespace Persistence.Repositories;

public class ReviewRepository : EfRepositoryBase<Review, Guid, BaseDbContext>, IReviewRepository
{
    public ReviewRepository(BaseDbContext context) : base(context)
    {
    }
}