using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Restaurants.Domain.Exceptions;

namespace Restaurants.Application.Users.Command.UnAssignUserRole;

public class UnAssignUserRoleCommandHandler(
    ILogger<UnAssignUserRoleCommand> logger,
    UserManager<Domain.Entities.User> userManager,
    RoleManager<IdentityRole> roleManager) : IRequestHandler<UnAssignUserRoleCommand>
{
    public async Task Handle(UnAssignUserRoleCommand request, CancellationToken cancellationToken)
    {
        // log the value
        logger.LogInformation("Unassign user role");

        // find the user by UserEmail
        var user = await userManager.FindByEmailAsync(request.UserEmail)
            ?? throw new NotFoundException(nameof(Domain.Entities.User), request.UserEmail);

        // find the role
        var role = await roleManager.FindByNameAsync(request.RoleName)
            ?? throw new NotFoundException(nameof(IdentityRole), request.RoleName);

        // unassign the role from the user using UserManager
        // by that point we have the role name else we had thrown an exception
        await userManager.RemoveFromRoleAsync(user, role.Name!);
    }
}