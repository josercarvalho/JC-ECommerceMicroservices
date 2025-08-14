using MediatR;

namespace Sales.Application.Orders.Commands
{
    public class UpdateOrderStatusCommand : IRequest<Unit>
    {
        public Guid OrderId { get; set; }
        public string NewStatus { get; set; }
    }
}
