﻿using AutoMapper;
using FluentAssertions;
using JetBrains.Annotations;
using Restaurants.Application.Restaurants.Commands.CreateRestaurant;
using Restaurants.Application.Restaurants.Commands.PartialUpdateRestaurant;
using Restaurants.Application.Restaurants.Dtos;
using Restaurants.Domain.Entities;
using Xunit;

namespace Restaurants.Application.Tests.Restaurants.Dtos;

[TestSubject(typeof(RestaurantsProfile))]
public class RestaurantsProfileTest
{
    private readonly IMapper _mapper;

    public RestaurantsProfileTest()
    {
        var configuration = new MapperConfiguration(cfg => { cfg.AddProfile<RestaurantsProfile>(); });
        _mapper = configuration.CreateMapper();
    }

    [Fact]
    public void CreateMap_ForRestaurantToRestaurantDto_MapsCorrectly()
    {
        // arrange


        var restaurant = new Restaurant()
        {
            Id = 1,
            Name = "Test Restaurant",
            Description = "Test Restaurant",
            Category = "Test Category",
            HasDelivery = true,
            ContactEmail = "Test@email.com",
            ContactNumber = "123456789",
            Address = new Address()
            {
                Street = "Test Street",
                City = "Test City",
                PostalCode = "12345",
            }
        };

        // act

        var restaurantDto = _mapper.Map<RestaurantDto>(restaurant);


        // assert
        restaurantDto.Should().NotBeNull();
        restaurantDto.Id.Should().Be(restaurant.Id);
        restaurantDto.Name.Should().Be(restaurant.Name);
        restaurantDto.Description.Should().Be(restaurant.Description);
        restaurantDto.Category.Should().Be(restaurant.Category);
        restaurantDto.HasDelivery.Should().Be(restaurant.HasDelivery);
        restaurantDto.City.Should().Be(restaurant.Address.City);
        restaurantDto.PostalCode.Should().Be(restaurant.Address.PostalCode);
        restaurantDto.Street.Should().Be(restaurant.Address.Street);
    }


    [Fact]
    public void CreateMap_ForCreateRestaurantCommandToRestaurant_MapsCorrectly()
    {
        // arrange


        var command = new CreateRestaurantCommand()
        {
            Name = "Test Restaurant",
            Description = "Test Restaurant",
            Category = "Test Category",
            HasDelivery = true,
            ContactEmail = "Test@email.com",
            ContactNumber = "123456789",
            Street = "Test Street",
            City = "Test City",
            PostalCode = "12345",
        };

        // act

        var restaurant = _mapper.Map<Restaurant>(command);


        // assert
        restaurant.Should().NotBeNull();
        restaurant.Name.Should().Be(command.Name);
        restaurant.Description.Should().Be(command.Description);
        restaurant.Category.Should().Be(command.Category);
        restaurant.HasDelivery.Should().Be(command.HasDelivery);
        restaurant.Address.City.Should().Be(command.City);
        restaurant.Address.PostalCode.Should().Be(command.PostalCode);
        restaurant.Address.Street.Should().Be(command.Street);
    }


    [Fact]
    public void CreateMap_ForUpdateRestaurantCommandToRestaurant_MapsCorrectly()
    {
        // arrange


        var command = new PartialUpdateRestaurantCommand()
        {
            Name = "Test Restaurant",
            Description = "Test Restaurant",
            HasDelivery = true,
        };

        // act

        var restaurant = _mapper.Map<Restaurant>(command);


        // assert
        restaurant.Should().NotBeNull();
        restaurant.Name.Should().Be(command.Name);
        restaurant.Description.Should().Be(command.Description);
        restaurant.HasDelivery.Should().Be(command.HasDelivery);
    }
}