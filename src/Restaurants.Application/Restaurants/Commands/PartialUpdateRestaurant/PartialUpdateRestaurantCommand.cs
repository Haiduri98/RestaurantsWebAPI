﻿using MediatR;

namespace Restaurants.Application.Restaurants.Commands.PartialUpdateRestaurant;

public class PartialUpdateRestaurantCommand : IRequest
{
    public int Id { get; set; }
    public string Name { get; set; } = default!;
    public string Description { get; set; } = default!;
    public bool HasDelivery { get; set; }

}
