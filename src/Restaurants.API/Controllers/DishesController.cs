using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Restaurants.Application.Dishes.Dishes.Command.CreateDish;
using Restaurants.Application.Dishes.Dishes.Command.DeleteDish;
using Restaurants.Application.Dishes.Dishes.Queries.GetDishByIdForRestaurant;
using Restaurants.Application.Dishes.Dishes.Queries.GetDishForRestaurant;
using Restaurants.Application.Dishes.Dtos;
using Restaurants.Infrastructure.Authorization;

namespace Restaurants.API.Controllers;

[Route("api/restaurants/{restaurantId}/dishes")]
[ApiController]
[Authorize]
public class DishesController(IMediator mediator) : ControllerBase
{

    [HttpPost]

    public async Task<IActionResult> CreateDishAsync([FromRoute] int restaurantId, CreateDishCommand command)
    {
        command.RestaurantId = restaurantId;
        var dishId = await mediator.Send(command);
        return CreatedAtAction(
            "GetDishByIdForRestaurant",
            new { restaurantId, dishId }, // Route values
            null // No content returned with the response
        );
    }

    [HttpGet]
    [Authorize(Policy = PolicyNames.AtLeast20)]
    public async Task<ActionResult<IEnumerable<DishDto>>> GetAllForRestaurantAsync([FromRoute] int restaurantId)
    {
        var dishes = await mediator.Send(new GetDishesForRestaurantQuery(restaurantId));
        return Ok(dishes);
    }

    [HttpGet("{dishId}")]
    public async Task<ActionResult<DishDto>> GetDishByIdForRestaurant([FromRoute] int restaurantId,
        [FromRoute] int dishId)
    {
        var dish = await mediator.Send(new GetDishByIdForRestaurantQuery(restaurantId, dishId));
        return Ok(dish);
    }





    [HttpDelete]
    public async Task<ActionResult> DeleteDishForRestaurantAsync([FromRoute] int restaurantId)
    {
        await mediator.Send(new DeleteDishForRestaurantCommand(restaurantId));
        return NoContent();
    }


}