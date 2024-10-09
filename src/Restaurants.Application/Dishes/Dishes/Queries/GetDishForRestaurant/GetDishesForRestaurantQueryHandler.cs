using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Dishes.Dtos;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Dishes.Dishes.Queries.GetDishForRestaurant;

public class GetDishesForRestaurantQueryHandler(
    ILogger<GetDishesForRestaurantQuery> logger,
    IRestaurantsRepository restaurantsRepository,
    IDishesRepository dishesRepository,
    IMapper mapper) : IRequestHandler<GetDishesForRestaurantQuery, IEnumerable<DishDto>>
{
    public async Task<IEnumerable<DishDto>> Handle(GetDishesForRestaurantQuery request,
        CancellationToken cancellationToken)
    {
        logger.LogInformation("Getting all the dishes for a restaurant");
        var restaurant = await restaurantsRepository.GetByIdAsync(request.RestaurantId)
                         ?? throw new NotFoundException(nameof(Restaurant), request.RestaurantId.ToString());

        var results = mapper.Map<IEnumerable<DishDto>>(restaurant.Dishes);
        return results;
    }
}