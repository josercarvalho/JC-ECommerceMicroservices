using FluentValidation;
using Stock.Application.Products.Commands;

namespace Stock.Application.Products.Validators
{
    public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
    {
        public UpdateProductCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Id do produto é obrigatório.");

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Nome do produto é obrigatório.")
                .MaximumLength(250).WithMessage("Nome do produto não pode exceder 250 caracteres.");

            RuleFor(x => x.Description)
                .MaximumLength(1000).WithMessage("Descrição do produto não pode exceder 1000 caracteres.");

            RuleFor(x => x.Price)
                .GreaterThan(0).WithMessage("Preço do produto deve ser maior que zero.");
        }
    }
}
