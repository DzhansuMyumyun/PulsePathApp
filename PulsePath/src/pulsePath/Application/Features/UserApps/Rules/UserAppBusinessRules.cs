using Application.Features.UserApps.Constants;
using Application.Services.Repositories;
using NArchitecture.Core.Application.Rules;
using NArchitecture.Core.CrossCuttingConcerns.Exception.Types;
using NArchitecture.Core.Localization.Abstraction;
using Domain.Entities;

namespace Application.Features.UserApps.Rules;

public class UserAppBusinessRules : BaseBusinessRules
{
    private readonly IUserAppRepository _userAppRepository;
    private readonly ILocalizationService _localizationService;

    public UserAppBusinessRules(IUserAppRepository userAppRepository, ILocalizationService localizationService)
    {
        _userAppRepository = userAppRepository;
        _localizationService = localizationService;
    }

    private async Task throwBusinessException(string messageKey)
    {
        string message = await _localizationService.GetLocalizedAsync(messageKey, UserAppsBusinessMessages.SectionName);
        throw new BusinessException(message);
    }

    public async Task UserAppShouldExistWhenSelected(UserApp? userApp)
    {
        if (userApp == null)
            await throwBusinessException(UserAppsBusinessMessages.UserAppNotExists);
    }

    public async Task UserAppIdShouldExistWhenSelected(Guid id, CancellationToken cancellationToken)
    {
        UserApp? userApp = await _userAppRepository.GetAsync(
            predicate: ua => ua.Id == id,
            enableTracking: false,
            cancellationToken: cancellationToken
        );
        await UserAppShouldExistWhenSelected(userApp);
    }
}