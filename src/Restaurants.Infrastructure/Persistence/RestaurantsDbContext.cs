using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Restaurants.Domain.Entities;

namespace Restaurants.Infrastructure.Persistence;

/*
 * Some notes:
 * We add the entity framework core sqlserver extension to the infrastructure project
 * We add entityframeworkcore.tools
 * We create the connection string using the OnConfiguring method.
 * We add the tables we want in the db using the DbSet
 * The migration is created within the infrastructure project.
 * The presentation module/layer has reference/dependency on the infrastructure module/layer
 * initializing the db connection.
 * the connection string should not be hardcoded!!!
 * define the connection string in the appsettings.development.json
 */


internal class RestaurantsDbContext(DbContextOptions<RestaurantsDbContext> options) :
    IdentityDbContext<User>(options)
{
    internal DbSet<Restaurant> Restaurants { get; set; }
    internal DbSet<Dish> Dishes { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        // since address does not have its own identity, it is associated with the restaurant entity
        modelBuilder.Entity<Restaurant>().OwnsOne(r => r.Address);


        // One to Many
        modelBuilder.Entity<Restaurant>()
            .HasMany(r => r.Dishes)
            .WithOne()
            .HasForeignKey(d => d.RestaurantId);

        modelBuilder.Entity<User>()
            .HasMany(o => o.OwnedRestaurants)
            .WithOne(r => r.Owner)
            .HasForeignKey(r => r.OwnerId);
    }
}