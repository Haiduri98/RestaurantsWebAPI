using AutoMapper;
using Restaurants.Application.Dishes.Dishes.Command.CreateDish;
using Restaurants.Domain.Entities;

namespace Restaurants.Application.Dishes.Dtos;

public class DishesProfile : Profile
{
    public DishesProfile()
    {
        CreateMap<Dish, DishDto>();
        CreateMap<CreateDishCommand, Dish>();

    }
}