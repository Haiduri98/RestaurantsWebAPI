using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Domain.Constants;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Interfaces;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Dishes.Dishes.Command.DeleteDish;

public class DeleteDishForRestaurantCommandHandler(
    ILogger<DeleteDishForRestaurantCommand> logger,
    IRestaurantsRepository restaurantsRepository,
    IRestaurantAuthorizationService restaurantAuthorizationService) : IRequestHandler<DeleteDishForRestaurantCommand>
{
    public async Task Handle(DeleteDishForRestaurantCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Deleting all the dishes for a restaurant");

        var restaurant = await restaurantsRepository.GetByIdAsync(request.RestaurantId)
                         ?? throw new NotFoundException(nameof(Restaurant), request.RestaurantId.ToString());

        if (!restaurantAuthorizationService.Authorize(restaurant, ResourceOperation.Delete))
        {
            throw new ForbidException();
        }

        restaurant.Dishes.Clear();
        await restaurantsRepository.SaveChangesAsync();
    }
}