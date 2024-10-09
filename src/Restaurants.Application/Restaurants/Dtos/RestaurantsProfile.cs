using AutoMapper;
using Restaurants.Application.Restaurants.Commands.CreateRestaurant;
using Restaurants.Application.Restaurants.Commands.PartialUpdateRestaurant;
using Restaurants.Domain.Entities;

namespace Restaurants.Application.Restaurants.Dtos;

public class RestaurantsProfile : Profile
{
    public RestaurantsProfile()
    {
        CreateMap<CreateRestaurantCommand, Restaurant>()
            .ForMember(d => d.Address, opt => opt.MapFrom(s => new Address()
            {
                City = s.City,
                PostalCode = s.PostalCode,
                Street = s.Street
            }));


        CreateMap<Restaurant, RestaurantDto>()
            .ForMember(d => d.City, opt => opt.MapFrom(src => src.Address == null ? null : src.Address.City))
            .ForMember(d => d.PostalCode,
                opt => opt.MapFrom(src => src.Address == null ? null : src.Address.PostalCode))
            .ForMember(d => d.Street, opt => opt.MapFrom(src => src.Address == null ? null : src.Address.Street))
            .ForMember(d => d.Dishes, opt => opt.MapFrom(src => src.Dishes))
            .ReverseMap();

        CreateMap<PartialUpdateRestaurantCommand, Restaurant>().ReverseMap();

    }
}