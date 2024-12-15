using Application.Features.UserApps.Commands.Create;
using Application.Features.UserApps.Commands.Delete;
using Application.Features.UserApps.Commands.Update;
using Application.Features.UserApps.Queries.GetById;
using Application.Features.UserApps.Queries.GetList;
using NArchitecture.Core.Application.Requests;
using NArchitecture.Core.Application.Responses;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserAppsController : BaseController
{
    [HttpPost]
    public async Task<ActionResult<CreatedUserAppResponse>> Add([FromBody] CreateUserAppCommand command)
    {
        CreatedUserAppResponse response = await Mediator.Send(command);

        return CreatedAtAction(nameof(GetById), new { response.Id }, response);
    }

    [HttpPut]
    public async Task<ActionResult<UpdatedUserAppResponse>> Update([FromBody] UpdateUserAppCommand command)
    {
        UpdatedUserAppResponse response = await Mediator.Send(command);

        return Ok(response);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<DeletedUserAppResponse>> Delete([FromRoute] Guid id)
    {
        DeleteUserAppCommand command = new() { Id = id };

        DeletedUserAppResponse response = await Mediator.Send(command);

        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<GetByIdUserAppResponse>> GetById([FromRoute] Guid id)
    {
        GetByIdUserAppQuery query = new() { Id = id };

        GetByIdUserAppResponse response = await Mediator.Send(query);

        return Ok(response);
    }

    [HttpGet]
    public async Task<ActionResult<GetListResponse<GetListUserAppListItemDto>>> GetList([FromQuery] PageRequest pageRequest)
    {
        GetListUserAppQuery query = new() { PageRequest = pageRequest };

        GetListResponse<GetListUserAppListItemDto> response = await Mediator.Send(query);

        return Ok(response);
    }
}