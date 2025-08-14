using FluentValidation;
using Sales.Application.Orders.Commands;

namespace Sales.Application.Orders.Validators
{
    public class UpdateOrderStatusCommandValidator : AbstractValidator<UpdateOrderStatusCommand>
    {
        public UpdateOrderStatusCommandValidator()
        {
            RuleFor(x => x.OrderId)
                .NotEmpty().WithMessage("Id do pedido é obrigatório.");

            RuleFor(x => x.NewStatus)
                .NotEmpty().WithMessage("Novo status é obrigatório.")
                .Must(BeValidStatus).WithMessage("Status inválido. Status permitidos: Pending, Confirmed, Cancelled.");
        }

        private bool BeValidStatus(string status)
        {
            return status == "Pending" || status == "Confirmed" || status == "Cancelled";
        }
    }
}
