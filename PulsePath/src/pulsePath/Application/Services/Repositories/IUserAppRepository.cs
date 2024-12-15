using Domain.Entities;
using NArchitecture.Core.Persistence.Repositories;

namespace Application.Services.Repositories;

public interface IUserAppRepository : IAsyncRepository<UserApp, Guid>, IRepository<UserApp, Guid>
{
}