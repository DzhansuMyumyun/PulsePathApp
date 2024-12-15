using Application.Features.UserApps.Constants;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using NArchitecture.Core.Application.Pipelines.Authorization;
using NArchitecture.Core.Application.Pipelines.Caching;
using NArchitecture.Core.Application.Requests;
using NArchitecture.Core.Application.Responses;
using NArchitecture.Core.Persistence.Paging;
using MediatR;
using static Application.Features.UserApps.Constants.UserAppsOperationClaims;

namespace Application.Features.UserApps.Queries.GetList;

public class GetListUserAppQuery : IRequest<GetListResponse<GetListUserAppListItemDto>>, ISecuredRequest, ICachableRequest
{
    public PageRequest PageRequest { get; set; }

    public string[] Roles => [Admin, Read];

    public bool BypassCache { get; }
    public string? CacheKey => $"GetListUserApps({PageRequest.PageIndex},{PageRequest.PageSize})";
    public string? CacheGroupKey => "GetUserApps";
    public TimeSpan? SlidingExpiration { get; }

    public class GetListUserAppQueryHandler : IRequestHandler<GetListUserAppQuery, GetListResponse<GetListUserAppListItemDto>>
    {
        private readonly IUserAppRepository _userAppRepository;
        private readonly IMapper _mapper;

        public GetListUserAppQueryHandler(IUserAppRepository userAppRepository, IMapper mapper)
        {
            _userAppRepository = userAppRepository;
            _mapper = mapper;
        }

        public async Task<GetListResponse<GetListUserAppListItemDto>> Handle(GetListUserAppQuery request, CancellationToken cancellationToken)
        {
            IPaginate<UserApp> userApps = await _userAppRepository.GetListAsync(
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize, 
                cancellationToken: cancellationToken
            );

            GetListResponse<GetListUserAppListItemDto> response = _mapper.Map<GetListResponse<GetListUserAppListItemDto>>(userApps);
            return response;
        }
    }
}