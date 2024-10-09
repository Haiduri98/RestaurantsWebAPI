using FluentValidation;

namespace Restaurants.Application.Restaurants.Commands.CreateRestaurant;

public class CreateRestaurantCommandValidator : AbstractValidator<CreateRestaurantCommand>
{
    private readonly List<string> validCategories = ["Italian", "Mexican", "Japanese", "American","Indian"];

    public CreateRestaurantCommandValidator()
    {
        RuleFor(dto => dto.Name).Length(3, 100);
        RuleFor(dto => dto.Description).NotEmpty().WithMessage("Description is required.");
        RuleFor(dto => dto.Category).NotEmpty().WithMessage("Insert a valid category.");
        RuleFor(dto => dto.ContactEmail).EmailAddress().WithMessage("Insert a valid Email address.");
        RuleFor(dto => dto.PostalCode).Matches(@"^\d{2}-\d{3}$")
            .WithMessage("Please provide a valid postal code (xx-xxx)");

        RuleFor(dto => dto.Category)
            .Must(category => validCategories.Contains(category))
            .WithMessage("Category is not valid, please choose from the valid categories.");

        //     .Custom((value, context) =>
        // {
        //     var isValidCategory = validCategories.Contains(value);
        //     if (!isValidCategory)
        //     {
        //         context.AddFailure("Category is not valid, please choose from the valid categories.");
        //     }
        // });
    }
}