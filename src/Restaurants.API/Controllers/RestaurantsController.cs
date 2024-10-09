using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Restaurants.Application.Restaurants.Commands.CreateRestaurant;
using Restaurants.Application.Restaurants.Commands.DeleteRestaurant;
using Restaurants.Application.Restaurants.Commands.PartialUpdateRestaurant;
using Restaurants.Application.Restaurants.Dtos;
using Restaurants.Application.Restaurants.Queries.GetAllRestaurants;
using Restaurants.Application.Restaurants.Queries.GetRestaurantById;
using Restaurants.Domain.Constants;
using Restaurants.Infrastructure.Authorization;

namespace Restaurants.API.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class RestaurantsController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [AllowAnonymous]
    public async Task<ActionResult<IEnumerable<RestaurantDto>>> GetAll([FromQuery] GetAllRestaurantsQuery query)
    {
        var restaurants = await mediator.Send(query);
        return Ok(restaurants);
    }


    [HttpGet("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [Authorize(Policy = PolicyNames.HasNationality)] //only users with this policy can invoke this endpoint
    public async Task<ActionResult<RestaurantDto?>> GetById([FromRoute] int id)
    {
        // var userId = User.Claims.FirstOrDefault(c => c.Type == "<id claim type>")!.Value;

        var restaurant = await mediator.Send(new GetRestaurantByIdQuery(id));

        return Ok(restaurant);
    }

    [HttpPost]
    [Authorize(Roles = UserRoles.Owner)]
    public async Task<IActionResult> CreateRestaurant([FromBody] CreateRestaurantCommand command)
    {
        int id = await mediator.Send(command);
        return CreatedAtAction(nameof(GetById), new { id }, null);
    }


    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteRestaurantAsync([FromRoute] int id)
    {
        await mediator.Send(new DeleteRestaurantCommand(id));
        return NoContent();
    }


    [HttpPatch("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> PartialUpdateRestaurantAsync([FromRoute] int id,
        [FromBody] PartialUpdateRestaurantCommand command)
    {
        command.Id = id;
        await mediator.Send(command);
        return NoContent();
    }
}