using MediatR;
using Sales.Application.Orders.ViewModels;
using Sales.Domain.Entities;
using Sales.Domain.Interfaces;

namespace Sales.Application.Orders.Handlers
{
    public class CreateOrderCommandHandler : IRequestHandler<Commands.CreateOrderCommand, OrderViewModel>
    {
        private readonly IOrderRepository _orderRepository;
        // private readonly IRabbitMQPublisher _rabbitMQPublisher; // To be implemented later

        public CreateOrderCommandHandler(IOrderRepository orderRepository /*, IRabbitMQPublisher rabbitMQPublisher*/)
        {
            _orderRepository = orderRepository;
            // _rabbitMQPublisher = rabbitMQPublisher;
        }

        public async Task<OrderViewModel> Handle(Commands.CreateOrderCommand request, CancellationToken cancellationToken)
        {
            // TODO: Validate product availability with Stock service (synchronously or asynchronously)
            // For now, assume products are available

            var order = new Order(request.CustomerId);
            foreach (var item in request.Items)
            {
                order.AddOrderItem(item.ProductId, item.ProductName, item.Price, item.Quantity);
            }

            await _orderRepository.AddAsync(order);

            // TODO: Publish OrderCreatedEvent to RabbitMQ
            // await _rabbitMQPublisher.Publish(new OrderCreatedEvent { OrderId = order.Id, Items = request.Items });

            return new OrderViewModel
            {
                Id = order.Id,
                CustomerId = order.CustomerId,
                OrderDate = order.OrderDate,
                Status = order.Status,
                OrderItems = order.OrderItems.Select(oi => new OrderItemViewModel
                {
                    ProductId = oi.ProductId,
                    ProductName = oi.ProductName,
                    Price = oi.Price,
                    Quantity = oi.Quantity
                }).ToList()
            };
        }
    }
}
