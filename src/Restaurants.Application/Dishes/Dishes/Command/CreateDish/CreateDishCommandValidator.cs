using FluentValidation;

namespace Restaurants.Application.Dishes.Dishes.Command.CreateDish;

public class CreateDishCommandValidator : AbstractValidator<CreateDishCommand>
{
    public CreateDishCommandValidator()
    {
        RuleFor(command => command.Price).GreaterThanOrEqualTo(0).WithMessage("Price must be greater than 0");
        RuleFor(command => command.KiloCalories).GreaterThanOrEqualTo(0).WithMessage("Calorie must be greater than 0");
    }
}