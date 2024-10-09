using MediatR;
using Restaurants.Application.Common;
using Restaurants.Application.Restaurants.Dtos;
using Restaurants.Domain.Constants;
using Restaurants.Domain.Entities;

namespace Restaurants.Application.Restaurants.Queries.GetAllRestaurants;

public class GetAllRestaurantsQuery : IRequest<PagedResult<RestaurantDto>>
{
    public string? searchPhrase { get; set; }
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    public string? SortBy { get; set; }
    public SortDirection sortDirection { get; set; }
}