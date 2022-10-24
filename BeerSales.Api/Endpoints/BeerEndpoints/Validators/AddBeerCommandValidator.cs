using BeerSales.Core.Beer.Commands;
using FluentValidation;

namespace BeerSales.Api.Endpoints.BeerEndpoints.Validators
{
    public class AddBeerCommandValidator : AbstractValidator<AddBeerCommand>
    {
        public AddBeerCommandValidator()
        {
            RuleFor(addBeerCommand => addBeerCommand).NotEmpty();
            RuleFor(addBeerCommand => addBeerCommand.Beer.Name)
                .MinimumLength(3)
                .WithMessage("Beer name should be minimum 3 character.");
                
            RuleFor(addBeerCommand => addBeerCommand.Beer.AlcoholContent)
                .GreaterThanOrEqualTo(0)
                .WithMessage("Alcohol content should be positive value.");

            RuleFor(addBeerCommand => addBeerCommand.Beer.Price)
                .GreaterThanOrEqualTo(0)
                .WithMessage("Price should be positive value.");

            RuleFor(addBeerCommand => addBeerCommand.Beer.Currency)
                .MinimumLength(3)
                .MaximumLength(3)
                .WithMessage("Currency should be exactly 3 character e.g. EUR");
        }
    }
}
