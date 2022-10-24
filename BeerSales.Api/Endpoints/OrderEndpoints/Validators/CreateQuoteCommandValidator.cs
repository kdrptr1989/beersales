using BeerSales.Core.Beer.Commands;
using BeerSales.Core.Order.Commands.CreateQuote;
using FluentValidation;

namespace BeerSales.Api.Endpoints.BeerEndpoints.Validators
{
    public class CreateQuoteCommandValidator : AbstractValidator<CreateQuoteCommand>
    {
        public CreateQuoteCommandValidator()
        {
            RuleFor(createQuoteCommand => createQuoteCommand).NotEmpty();
            RuleFor(createQuoteCommand => createQuoteCommand.OrdersList)
                .NotEmpty()
                .WithMessage("Order cannot be empty!");
                
        }
    }
}
