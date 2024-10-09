using MediatR;
using Restaurants.Application.Dishes.Dtos;

namespace Restaurants.Application.Dishes.Dishes.Queries.GetDishForRestaurant;

public class GetDishesForRestaurantQuery(int restaurantId) : IRequest<IEnumerable<DishDto>>
{
    public int RestaurantId { get; } = restaurantId;
}