using Application.Features.UserApps.Rules;
using Application.Services.Repositories;
using NArchitecture.Core.Persistence.Paging;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Application.Services.UserApps;

public class UserAppManager : IUserAppService
{
    private readonly IUserAppRepository _userAppRepository;
    private readonly UserAppBusinessRules _userAppBusinessRules;

    public UserAppManager(IUserAppRepository userAppRepository, UserAppBusinessRules userAppBusinessRules)
    {
        _userAppRepository = userAppRepository;
        _userAppBusinessRules = userAppBusinessRules;
    }

    public async Task<UserApp?> GetAsync(
        Expression<Func<UserApp, bool>> predicate,
        Func<IQueryable<UserApp>, IIncludableQueryable<UserApp, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        UserApp? userApp = await _userAppRepository.GetAsync(predicate, include, withDeleted, enableTracking, cancellationToken);
        return userApp;
    }

    public async Task<IPaginate<UserApp>?> GetListAsync(
        Expression<Func<UserApp, bool>>? predicate = null,
        Func<IQueryable<UserApp>, IOrderedQueryable<UserApp>>? orderBy = null,
        Func<IQueryable<UserApp>, IIncludableQueryable<UserApp, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        IPaginate<UserApp> userAppList = await _userAppRepository.GetListAsync(
            predicate,
            orderBy,
            include,
            index,
            size,
            withDeleted,
            enableTracking,
            cancellationToken
        );
        return userAppList;
    }

    public async Task<UserApp> AddAsync(UserApp userApp)
    {
        UserApp addedUserApp = await _userAppRepository.AddAsync(userApp);

        return addedUserApp;
    }

    public async Task<UserApp> UpdateAsync(UserApp userApp)
    {
        UserApp updatedUserApp = await _userAppRepository.UpdateAsync(userApp);

        return updatedUserApp;
    }

    public async Task<UserApp> DeleteAsync(UserApp userApp, bool permanent = false)
    {
        UserApp deletedUserApp = await _userAppRepository.DeleteAsync(userApp);

        return deletedUserApp;
    }
}
