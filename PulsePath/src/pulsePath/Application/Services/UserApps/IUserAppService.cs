using NArchitecture.Core.Persistence.Paging;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Application.Services.UserApps;

public interface IUserAppService
{
    Task<UserApp?> GetAsync(
        Expression<Func<UserApp, bool>> predicate,
        Func<IQueryable<UserApp>, IIncludableQueryable<UserApp, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );
    Task<IPaginate<UserApp>?> GetListAsync(
        Expression<Func<UserApp, bool>>? predicate = null,
        Func<IQueryable<UserApp>, IOrderedQueryable<UserApp>>? orderBy = null,
        Func<IQueryable<UserApp>, IIncludableQueryable<UserApp, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );
    Task<UserApp> AddAsync(UserApp userApp);
    Task<UserApp> UpdateAsync(UserApp userApp);
    Task<UserApp> DeleteAsync(UserApp userApp, bool permanent = false);
}
