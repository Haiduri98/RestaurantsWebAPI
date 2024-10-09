using MediatR;

namespace Restaurants.Application.Dishes.Dishes.Command.DeleteDish;

public class DeleteDishForRestaurantCommand(int restaurantId) : IRequest
{
    public int RestaurantId { get; } = restaurantId;

}