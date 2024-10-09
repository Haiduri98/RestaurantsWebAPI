﻿using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using FluentAssertions;
using JetBrains.Annotations;
using Microsoft.Extensions.Logging;
using Moq;
using Restaurants.Application.Restaurants.Commands.CreateRestaurant;
using Restaurants.Application.User;
using Restaurants.Application.Users;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Repositories;
using Xunit;

namespace Restaurants.Application.Tests.Restaurants.Commands.CreateRestaurant;

[TestSubject(typeof(CreateRestaurantCommandHandler))]
public class CreateRestaurantCommandHandlerTest
{
    [Fact]
    public async Task Handle_ForValidCommand_ReturnsCreatedRestaurantId()
    {
        // arrange
        var loggerMock = new Mock<ILogger<CreateRestaurantCommandHandler>>();
        var mapperMock = new Mock<IMapper>();

        var command = new CreateRestaurantCommand();
        var restaurant = new Restaurant();

        mapperMock.Setup(m => m.Map<Restaurant>(command)).Returns(restaurant);


        var restaurantRepositoryMock = new Mock<IRestaurantsRepository>();

        restaurantRepositoryMock.Setup(repo => repo.CreateAsync(It.IsAny<Restaurant>())).ReturnsAsync(1);


        var userContextMock = new Mock<IUserContext>();
        var currentUser = new CurrentUser("owner-id", "test@test.com", [], null, null);

        userContextMock.Setup(u => u.GetCurrentUser()).Returns(currentUser);

        var commandHandler =
            new CreateRestaurantCommandHandler(loggerMock.Object, mapperMock.Object, restaurantRepositoryMock.Object,
                userContextMock.Object);

        // act
        var result = await commandHandler.Handle(command, CancellationToken.None);

        // assert
        result.Should().Be(1);
        restaurant.OwnerId.Should().Be("owner-id");
        restaurantRepositoryMock.Verify(repo => repo.CreateAsync(restaurant), Times.Once);
    }
}