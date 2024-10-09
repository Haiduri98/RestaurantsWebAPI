using FluentValidation;

namespace Restaurants.Application.Restaurants.Commands.PartialUpdateRestaurant;

public class PartialUpdateRestaurantCommandValidator : AbstractValidator<PartialUpdateRestaurantCommand>
{
    public PartialUpdateRestaurantCommandValidator()
    {
        RuleFor(c => c.Name).Length(3, 100);

    }
}