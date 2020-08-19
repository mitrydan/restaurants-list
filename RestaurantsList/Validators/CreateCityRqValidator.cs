using FluentValidation;
using RestaurantsList.Contracts;

namespace RestaurantsList.Validators
{
    public class CreateCityRqValidator : AbstractValidator<CreateCityRq>
    {
        public CreateCityRqValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .MaximumLength(255);
        }
    }
}
