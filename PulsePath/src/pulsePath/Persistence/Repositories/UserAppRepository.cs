using Application.Services.Repositories;
using Domain.Entities;
using NArchitecture.Core.Persistence.Repositories;
using Persistence.Contexts;

namespace Persistence.Repositories;

public class UserAppRepository : EfRepositoryBase<UserApp, Guid, BaseDbContext>, IUserAppRepository
{
    public UserAppRepository(BaseDbContext context) : base(context)
    {
    }
}