using MediatR;
using Restaurants.Application.Restaurants.Dtos;
using Restaurants.Domain.Entities;

namespace Restaurants.Application.Restaurants.Queries.GetRestaurantById;

public class GetRestaurantByIdQuery(int id) : IRequest<RestaurantDto>
{
    public int Id { get;  } = id;
}