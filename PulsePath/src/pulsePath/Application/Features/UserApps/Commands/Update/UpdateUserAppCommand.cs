using Application.Features.UserApps.Constants;
using Application.Features.UserApps.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using NArchitecture.Core.Application.Pipelines.Authorization;
using NArchitecture.Core.Application.Pipelines.Caching;
using NArchitecture.Core.Application.Pipelines.Logging;
using NArchitecture.Core.Application.Pipelines.Transaction;
using MediatR;
using static Application.Features.UserApps.Constants.UserAppsOperationClaims;

namespace Application.Features.UserApps.Commands.Update;

public class UpdateUserAppCommand : IRequest<UpdatedUserAppResponse>, ISecuredRequest, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public Guid Id { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required string Email { get; set; }
    public required string PasswordHash { get; set; }
    public required DateTime CreatedAt { get; set; }
    public required DateTime UpdatedAt { get; set; }

    public string[] Roles => [Admin, Write, UserAppsOperationClaims.Update];

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string[]? CacheGroupKey => ["GetUserApps"];

    public class UpdateUserAppCommandHandler : IRequestHandler<UpdateUserAppCommand, UpdatedUserAppResponse>
    {
        private readonly IMapper _mapper;
        private readonly IUserAppRepository _userAppRepository;
        private readonly UserAppBusinessRules _userAppBusinessRules;

        public UpdateUserAppCommandHandler(IMapper mapper, IUserAppRepository userAppRepository,
                                         UserAppBusinessRules userAppBusinessRules)
        {
            _mapper = mapper;
            _userAppRepository = userAppRepository;
            _userAppBusinessRules = userAppBusinessRules;
        }

        public async Task<UpdatedUserAppResponse> Handle(UpdateUserAppCommand request, CancellationToken cancellationToken)
        {
            UserApp? userApp = await _userAppRepository.GetAsync(predicate: ua => ua.Id == request.Id, cancellationToken: cancellationToken);
            await _userAppBusinessRules.UserAppShouldExistWhenSelected(userApp);
            userApp = _mapper.Map(request, userApp);

            await _userAppRepository.UpdateAsync(userApp!);

            UpdatedUserAppResponse response = _mapper.Map<UpdatedUserAppResponse>(userApp);
            return response;
        }
    }
}