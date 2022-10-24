
using BeerSales.Core.Stock.Commands;
using FluentValidation;

namespace BeerSales.Api.Endpoints.BeerEndpoints.Validators
{
    public class UpdateStockCommandValidator : AbstractValidator<UpdateStockCommand>
    {
        public UpdateStockCommandValidator()
        {
            RuleFor(createQuoteCommand => createQuoteCommand).NotEmpty();
            RuleFor(createQuoteCommand => createQuoteCommand.UpdateStockDto)
                .NotEmpty()
                .WithMessage("New Stock item cannot be empty!");

            RuleFor(createQuoteCommand => createQuoteCommand.UpdateStockDto.Quantity)
               .GreaterThanOrEqualTo(0)
               .WithMessage("Quantity should be a positive value!");
        }
    }
}
