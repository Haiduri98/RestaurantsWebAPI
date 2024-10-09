using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Domain.Constants;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Interfaces;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Dishes.Dishes.Command.CreateDish;

public class CreateDishCommandHandler(
    ILogger<CreateDishCommand> logger,
    IRestaurantsRepository restaurantsRepository,
    IDishesRepository dishesRepository,
    IMapper mapper,
    IRestaurantAuthorizationService restaurantAuthorizationService
) : IRequestHandler<CreateDishCommand, int>
{
    public async Task<int> Handle(CreateDishCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Creating new dish");

        var restaurantExists = await restaurantsRepository.GetByIdAsync(request.RestaurantId);

        if (restaurantExists is null) throw new NotFoundException(nameof(Restaurant), request.RestaurantId.ToString());

        if (!restaurantAuthorizationService.Authorize(restaurantExists, ResourceOperation.Create))
        {
            throw new ForbidException();
        }

        var dish = mapper.Map<Dish>(request);

        return await dishesRepository.CreateAsync(dish);
    }
}