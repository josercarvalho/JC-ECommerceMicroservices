using MediatR;
using Sales.Application.Orders.ViewModels;

namespace Sales.Application.Orders.Queries
{
    public class GetAllOrdersQuery : IRequest<IEnumerable<OrderViewModel>>
    {
    }
}
