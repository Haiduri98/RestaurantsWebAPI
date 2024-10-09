using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Restaurants.Application.Users.Command.AssignUserRole;
using Restaurants.Application.Users.Command.UnAssignUserRole;
using Restaurants.Application.Users.Command.UpdateUserDetails;
using Restaurants.Domain.Constants;

namespace Restaurants.API.Controllers;

[Route("api/identity")]
[ApiController]
public class IdentityController(IMediator mediator) : ControllerBase
{
    [HttpPatch("user")]
    [Authorize]
    public async Task<IActionResult> UpdateUserDetails(UpdateUserDetailsCommand command)
    {
        await mediator.Send(command);
        return NoContent();
    }

    [HttpPost("userRole")]
    [Authorize(Roles = UserRoles.Admin)]
    public async Task<IActionResult> AssignUserRole(AssignUserRoleCommand command)
    {
        await mediator.Send(command);
        return NoContent();
    }

    [HttpDelete("userRole")]
    [Authorize(Roles = UserRoles.Admin)]
    public async Task<IActionResult> RemoveUserRole(UnAssignUserRoleCommand command)
    {
        await mediator.Send(command);
        return NoContent();
    }
}

