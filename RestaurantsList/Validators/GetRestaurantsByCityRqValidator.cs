using FluentValidation;
using RestaurantsList.Requests;

namespace RestaurantsList.Validators
{
    public class GetRestaurantsByCityRqValidator : AbstractValidator<GetRestaurantsByCityRq>
    {
        public GetRestaurantsByCityRqValidator()
        { }
    }
}
