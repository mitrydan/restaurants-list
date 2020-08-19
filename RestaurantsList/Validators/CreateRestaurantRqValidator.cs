using FluentValidation;
using RestaurantsList.Contracts;

namespace RestaurantsList.Validators
{
    public class CreateRestaurantRqValidator : AbstractValidator<CreateRestaurantRq>
    {
        public CreateRestaurantRqValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .MaximumLength(255);
        }
    }
}
