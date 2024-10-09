using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Restaurants.Domain.Exceptions;

namespace Restaurants.Application.Users.Command.AssignUserRole;

public class AssignUserRoleCommandHandler(
    ILogger<AssignUserRoleCommand> logger,
    RoleManager<IdentityRole> roleManager,
    UserManager<Domain.Entities.User> userManager)
    : IRequestHandler<AssignUserRoleCommand>
{
    public async Task Handle(AssignUserRoleCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Assigning user role: {@Request}", request);

        // getting the user if exists
        var user = await userManager.FindByEmailAsync(request.UserEmail)
                   ?? throw new NotFoundException(nameof(User), request.UserEmail);

        // getting the role if exists
        var role = await roleManager.FindByNameAsync(request.RoleName)
                   ?? throw new NotFoundException(nameof(IdentityRole), request.RoleName);

        // assign a user to a given role
        // we are sure at this point we have the name of the rule
        await userManager.AddToRoleAsync(user, role.Name!);
    }
}