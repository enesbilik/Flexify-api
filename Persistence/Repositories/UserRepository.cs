using System;
using Application.Services.Repositories;
using Core.Persistence.Repositories;
using Domain.Entities;
using Persistence.Contexts;

namespace Persistence.Repositories;



public class UserRepository : EfRepositoryBase<AppUser, Guid, BaseDbContext>, IUserRepository
{
    public UserRepository(BaseDbContext context)
        : base(context) { }
}
