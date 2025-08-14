using FluentValidation;
using Stock.Application.Products.Commands;

namespace Stock.Application.Products.Validators
{
    public class DecreaseStockCommandValidator : AbstractValidator<DecreaseStockCommand>
    {
        public DecreaseStockCommandValidator()
        {
            RuleFor(x => x.ProductId)
                .NotEmpty().WithMessage("Id do produto é obrigatório.");

            RuleFor(x => x.Quantity)
                .GreaterThan(0).WithMessage("Quantidade a ser diminuída deve ser maior que zero.");
        }
    }
}
