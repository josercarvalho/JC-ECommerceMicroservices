using MediatR;
using Sales.Application.Orders.ViewModels;

namespace Sales.Application.Orders.Queries
{
    public class GetOrderByIdQuery : IRequest<OrderViewModel>
    {
        public Guid Id { get; set; }
    }
}
