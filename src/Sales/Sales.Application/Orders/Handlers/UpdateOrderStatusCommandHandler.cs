using MediatR;
using Sales.Domain.Interfaces;

namespace Sales.Application.Orders.Handlers
{
    public class UpdateOrderStatusCommandHandler : IRequestHandler<Commands.UpdateOrderStatusCommand, Unit>
    {
        private readonly IOrderRepository _orderRepository;

        public UpdateOrderStatusCommandHandler(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<Unit> Handle(Commands.UpdateOrderStatusCommand request, CancellationToken cancellationToken)
        {
            var order = await _orderRepository.GetByIdAsync(request.OrderId);
            if (order == null)
            {
                // TODO: Handle not found exception
                return Unit.Value;
            }

            order.UpdateStatus(request.NewStatus);
            await _orderRepository.UpdateAsync(order);

            return Unit.Value;
        }
    }
}
