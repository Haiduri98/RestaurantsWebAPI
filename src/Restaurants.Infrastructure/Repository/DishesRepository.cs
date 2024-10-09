using Restaurants.Domain.Entities;
using Restaurants.Domain.Repositories;
using Restaurants.Infrastructure.Persistence;

namespace Restaurants.Infrastructure.Repository;

internal class DishesRepository(RestaurantsDbContext dbContext) : IDishesRepository
{
    public async Task<int> CreateAsync(Dish entity)
    {
        dbContext.Dishes.Add(entity);
        await dbContext.SaveChangesAsync();
        return entity.Id;
    }
}