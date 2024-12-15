using NArchitecture.Core.Application.Responses;

namespace Application.Features.UserApps.Commands.Delete;

public class DeletedUserAppResponse : IResponse
{
    public Guid Id { get; set; }
}