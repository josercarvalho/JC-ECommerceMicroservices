using FluentValidation;
using Sales.Application.Orders.Commands;

namespace Sales.Application.Orders.Validators
{
    public class CreateOrderCommandValidator : AbstractValidator<CreateOrderCommand>
    {
        public CreateOrderCommandValidator()
        {
            RuleFor(x => x.CustomerId)
                .NotEmpty().WithMessage("Id do cliente é obrigatório.");

            RuleFor(x => x.Items)
                .NotEmpty().WithMessage("O pedido deve conter pelo menos um item.")
                .Must(items => items.Any()).WithMessage("O pedido deve conter pelo menos um item.");

            RuleForEach(x => x.Items).SetValidator(new OrderItemCommandValidator());
        }
    }

    public class OrderItemCommandValidator : AbstractValidator<OrderItemCommand>
    {
        public OrderItemCommandValidator()
        {
            RuleFor(x => x.ProductId)
                .NotEmpty().WithMessage("Id do produto é obrigatório.");

            RuleFor(x => x.ProductName)
                .NotEmpty().WithMessage("Nome do produto é obrigatório.")
                .MaximumLength(250).WithMessage("Nome do produto não pode exceder 250 caracteres.");

            RuleFor(x => x.Price)
                .GreaterThan(0).WithMessage("Preço do item deve ser maior que zero.");

            RuleFor(x => x.Quantity)
                .GreaterThan(0).WithMessage("Quantidade do item deve ser maior que zero.");
        }
    }
}
