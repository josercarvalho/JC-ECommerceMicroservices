using MediatR;
using Sales.Application.Orders.ViewModels;

namespace Sales.Application.Orders.Commands
{
    public class CreateOrderCommand : IRequest<OrderViewModel>
    {
        public Guid CustomerId { get; set; }
        public List<OrderItemCommand> Items { get; set; }
    }

    public class OrderItemCommand
    {
        public Guid ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}
