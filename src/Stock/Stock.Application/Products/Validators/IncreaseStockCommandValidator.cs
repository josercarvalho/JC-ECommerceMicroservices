using FluentValidation;
using Stock.Application.Products.Commands;

namespace Stock.Application.Products.Validators
{
    public class IncreaseStockCommandValidator : AbstractValidator<IncreaseStockCommand>
    {
        public IncreaseStockCommandValidator()
        {
            RuleFor(x => x.ProductId)
                .NotEmpty().WithMessage("Id do produto é obrigatório.");

            RuleFor(x => x.Quantity)
                .GreaterThan(0).WithMessage("Quantidade a ser aumentada deve ser maior que zero.");
        }
    }
}
