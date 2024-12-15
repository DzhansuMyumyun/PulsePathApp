using Application.Features.UserApps.Constants;
using Application.Features.UserApps.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using NArchitecture.Core.Application.Pipelines.Authorization;
using MediatR;
using static Application.Features.UserApps.Constants.UserAppsOperationClaims;

namespace Application.Features.UserApps.Queries.GetById;

public class GetByIdUserAppQuery : IRequest<GetByIdUserAppResponse>, ISecuredRequest
{
    public Guid Id { get; set; }

    public string[] Roles => [Admin, Read];

    public class GetByIdUserAppQueryHandler : IRequestHandler<GetByIdUserAppQuery, GetByIdUserAppResponse>
    {
        private readonly IMapper _mapper;
        private readonly IUserAppRepository _userAppRepository;
        private readonly UserAppBusinessRules _userAppBusinessRules;

        public GetByIdUserAppQueryHandler(IMapper mapper, IUserAppRepository userAppRepository, UserAppBusinessRules userAppBusinessRules)
        {
            _mapper = mapper;
            _userAppRepository = userAppRepository;
            _userAppBusinessRules = userAppBusinessRules;
        }

        public async Task<GetByIdUserAppResponse> Handle(GetByIdUserAppQuery request, CancellationToken cancellationToken)
        {
            UserApp? userApp = await _userAppRepository.GetAsync(predicate: ua => ua.Id == request.Id, cancellationToken: cancellationToken);
            await _userAppBusinessRules.UserAppShouldExistWhenSelected(userApp);

            GetByIdUserAppResponse response = _mapper.Map<GetByIdUserAppResponse>(userApp);
            return response;
        }
    }
}