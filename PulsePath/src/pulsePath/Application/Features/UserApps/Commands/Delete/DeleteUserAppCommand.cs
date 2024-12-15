using Application.Features.UserApps.Constants;
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

namespace Application.Features.UserApps.Commands.Delete;

public class DeleteUserAppCommand : IRequest<DeletedUserAppResponse>, ISecuredRequest, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public Guid Id { get; set; }

    public string[] Roles => [Admin, Write, UserAppsOperationClaims.Delete];

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string[]? CacheGroupKey => ["GetUserApps"];

    public class DeleteUserAppCommandHandler : IRequestHandler<DeleteUserAppCommand, DeletedUserAppResponse>
    {
        private readonly IMapper _mapper;
        private readonly IUserAppRepository _userAppRepository;
        private readonly UserAppBusinessRules _userAppBusinessRules;

        public DeleteUserAppCommandHandler(IMapper mapper, IUserAppRepository userAppRepository,
                                         UserAppBusinessRules userAppBusinessRules)
        {
            _mapper = mapper;
            _userAppRepository = userAppRepository;
            _userAppBusinessRules = userAppBusinessRules;
        }

        public async Task<DeletedUserAppResponse> Handle(DeleteUserAppCommand request, CancellationToken cancellationToken)
        {
            UserApp? userApp = await _userAppRepository.GetAsync(predicate: ua => ua.Id == request.Id, cancellationToken: cancellationToken);
            await _userAppBusinessRules.UserAppShouldExistWhenSelected(userApp);

            await _userAppRepository.DeleteAsync(userApp!);

            DeletedUserAppResponse response = _mapper.Map<DeletedUserAppResponse>(userApp);
            return response;
        }
    }
}