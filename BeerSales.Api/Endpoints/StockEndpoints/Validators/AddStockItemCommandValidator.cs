
using BeerSales.Core.Stock.Commands;
using FluentValidation;

namespace BeerSales.Api.Endpoints.BeerEndpoints.Validators
{
    public class AddStockItemCommandValidator : AbstractValidator<AddStockItemCommand>
    {
        public AddStockItemCommandValidator()
        {
            RuleFor(createQuoteCommand => createQuoteCommand).NotEmpty();
            RuleFor(createQuoteCommand => createQuoteCommand.AddStockItem)
                .NotEmpty()
                .WithMessage("New Stock item cannot be empty!");

            RuleFor(createQuoteCommand => createQuoteCommand.AddStockItem.Quantity)
               .GreaterThanOrEqualTo(0)
               .WithMessage("Quantity should be a positive value!");
        }
    }
}
