using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Restaurants.Commands.CreateRestaurant;
using Restaurants.Domain.Constants;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Interfaces;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Restaurants.Commands.PartialUpdateRestaurant;

public class PartialUpdateRestaurantCommandHandler(
    ILogger<CreateRestaurantCommandHandler> logger,
    IMapper mapper,
    IRestaurantsRepository restaurantsRepository,
    IRestaurantAuthorizationService restaurantAuthorizationService) : IRequestHandler<PartialUpdateRestaurantCommand>
{
    public async Task Handle(PartialUpdateRestaurantCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Handling partial update restaurant command");
        var restaurant = await restaurantsRepository.GetByIdAsync(request.Id);

        if (restaurant is null) throw new NotFoundException(nameof(Restaurant), request.Id.ToString());


        if (!restaurantAuthorizationService.Authorize(restaurant, ResourceOperation.Update))
        {
            throw new ForbidException();
        }


        //
        // restaurant.Name = request.Name;
        // restaurant.Description = request.Description;
        // restaurant.HasDelivery = request.HasDelivery;
        // same as the code above
        mapper.Map(request, restaurant);


        // creating a saving method as the following and just mapping the new values
        // by just saving the db will update the new values
        await restaurantsRepository.SaveChangesAsync();
    }
}