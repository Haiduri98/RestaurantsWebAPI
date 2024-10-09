using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Dishes.Dtos;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Dishes.Dishes.Queries.GetDishByIdForRestaurant;

public class GetDishByIdForRestaurantQueryHandler(
    ILogger<GetDishByIdForRestaurantQuery> logger,
    IRestaurantsRepository restaurantsRepository,
    IDishesRepository dishesRepository,
    IMapper mapper) : IRequestHandler<GetDishByIdForRestaurantQuery, DishDto>
{
    public async Task<DishDto> Handle(GetDishByIdForRestaurantQuery request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Getting dish by ID");

        var restaurant = await restaurantsRepository.GetByIdAsync(request.RestaurantId)
                         ?? throw new NotFoundException(nameof(Restaurant), request.RestaurantId.ToString());

        var dish = restaurant.Dishes.FirstOrDefault(x => x.Id == request.DishId)
                   ?? throw new NotFoundException(nameof(Dish), request.DishId.ToString());

        var mappedDishDto = mapper.Map<DishDto>(dish);
        return mappedDishDto;
    }
}