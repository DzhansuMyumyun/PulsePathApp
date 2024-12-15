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

namespace Application.Features.UserApps.Commands.Create;

public class CreateUserAppCommand : IRequest<CreatedUserAppResponse>, ISecuredRequest, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required string Email { get; set; }
    public required string PasswordHash { get; set; }
    public required DateTime CreatedAt { get; set; }
    public required DateTime UpdatedAt { get; set; }

    public string[] Roles => [Admin, Write, UserAppsOperationClaims.Create];

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string[]? CacheGroupKey => ["GetUserApps"];

    public class CreateUserAppCommandHandler : IRequestHandler<CreateUserAppCommand, CreatedUserAppResponse>
    {
        private readonly IMapper _mapper;
        private readonly IUserAppRepository _userAppRepository;
        private readonly UserAppBusinessRules _userAppBusinessRules;

        public CreateUserAppCommandHandler(IMapper mapper, IUserAppRepository userAppRepository,
                                         UserAppBusinessRules userAppBusinessRules)
        {
            _mapper = mapper;
            _userAppRepository = userAppRepository;
            _userAppBusinessRules = userAppBusinessRules;
        }

        public async Task<CreatedUserAppResponse> Handle(CreateUserAppCommand request, CancellationToken cancellationToken)
        {
            UserApp userApp = _mapper.Map<UserApp>(request);

            await _userAppRepository.AddAsync(userApp);

            CreatedUserAppResponse response = _mapper.Map<CreatedUserAppResponse>(userApp);
            return response;
        }
    }
}