using FluentValidation;
using Stock.Application.Products.Commands;

namespace Stock.Application.Products.Validators
{
    public class DeleteProductCommandValidator : AbstractValidator<DeleteProductCommand>
    {
        public DeleteProductCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Id do produto é obrigatório.");
        }
    }
}
