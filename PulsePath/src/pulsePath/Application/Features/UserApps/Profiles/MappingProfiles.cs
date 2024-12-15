using Application.Features.UserApps.Commands.Create;
using Application.Features.UserApps.Commands.Delete;
using Application.Features.UserApps.Commands.Update;
using Application.Features.UserApps.Queries.GetById;
using Application.Features.UserApps.Queries.GetList;
using AutoMapper;
using NArchitecture.Core.Application.Responses;
using Domain.Entities;
using NArchitecture.Core.Persistence.Paging;

namespace Application.Features.UserApps.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<CreateUserAppCommand, UserApp>();
        CreateMap<UserApp, CreatedUserAppResponse>();

        CreateMap<UpdateUserAppCommand, UserApp>();
        CreateMap<UserApp, UpdatedUserAppResponse>();

        CreateMap<DeleteUserAppCommand, UserApp>();
        CreateMap<UserApp, DeletedUserAppResponse>();

        CreateMap<UserApp, GetByIdUserAppResponse>();

        CreateMap<UserApp, GetListUserAppListItemDto>();
        CreateMap<IPaginate<UserApp>, GetListResponse<GetListUserAppListItemDto>>();
    }
}