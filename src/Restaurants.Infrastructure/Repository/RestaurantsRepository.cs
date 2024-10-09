using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Restaurants.Domain.Constants;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Repositories;
using Restaurants.Infrastructure.Persistence;

namespace Restaurants.Infrastructure.Repository;

internal class RestaurantsRepository(RestaurantsDbContext dbContext) : IRestaurantsRepository
{
    public async Task<IEnumerable<Restaurant>> GetAllAsync()
    {
        var restaurants = await dbContext.Restaurants.ToListAsync();
        return restaurants;
    }


    public async Task<(IEnumerable<Restaurant>, int)> GetAllMatchingAsync(string? searchPhrase, int pageSize,
        int pageNumber, string? sortBy, SortDirection sortDirection)
    {
        var searchPhraseLowercase = searchPhrase?.ToLower();

        //pageSize = how many elements displayed on the page
        //pageNumber = how many pages all the elements are divided by
        // pageSize = 5, pageNumber = 3, skip => pageSize * (pageNumber - 1 ) => 5 * (3 - 1) => 10

        var baseQuery = dbContext.Restaurants
            .Where(restaurant => searchPhraseLowercase == null ||
                                 restaurant.Name.ToLower().Contains(searchPhraseLowercase) ||
                                 restaurant.Description.ToLower().Contains(searchPhraseLowercase));

        var totalCount = await baseQuery.CountAsync();

        if (sortBy != null)
        {
            var columnSelector = new Dictionary<string, Expression<Func<Restaurant, object>>>()
            {
                { nameof(Restaurant.Name), r => r.Name },
                { nameof(Restaurant.Description), r => r.Description },
                { nameof(Restaurant.Category), r => r.Category }
            };
            var selectedColumn = columnSelector[sortBy];

            baseQuery = sortDirection == SortDirection.Ascending
                ? baseQuery.OrderBy(selectedColumn) : baseQuery.OrderByDescending(selectedColumn);
        }

        var restaurants = await baseQuery
            .Skip(pageSize * (pageNumber - 1))
            .Take(pageSize)
            .ToListAsync();

        return (restaurants, totalCount);
    }

    public async Task<Restaurant?> GetByIdAsync(int id)
    {
        return await dbContext.Restaurants
            .Include(x => x.Dishes)
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<int> CreateAsync(Restaurant restaurant)
    {
        dbContext.Restaurants.Add(restaurant);
        await dbContext.SaveChangesAsync();
        return restaurant.Id;
    }

    public async Task DeleteAsync(Restaurant restaurant)
    {
        dbContext.Restaurants.Remove(restaurant);
        await dbContext.SaveChangesAsync();
    }

    public async Task SaveChangesAsync()
    {
        await dbContext.SaveChangesAsync();
    }
}