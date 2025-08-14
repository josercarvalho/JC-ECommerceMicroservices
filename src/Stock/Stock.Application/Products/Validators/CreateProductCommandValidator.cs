using FluentValidation;
using Stock.Application.Products.Commands;

namespace Stock.Application.Products.Validators
{
    public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
    {
        public CreateProductCommandValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Nome do produto é obrigatório.")
                .MaximumLength(250).WithMessage("Nome do produto não pode exceder 250 caracteres.");

            RuleFor(x => x.Description)
                .MaximumLength(1000).WithMessage("Descrição do produto não pode exceder 1000 caracteres.");

            RuleFor(x => x.Price)
                .GreaterThan(0).WithMessage("Preço do produto deve ser maior que zero.");

            RuleFor(x => x.QuantityInStock)
                .GreaterThanOrEqualTo(0).WithMessage("Quantidade em estoque não pode ser negativa.");
        }
    }
}
