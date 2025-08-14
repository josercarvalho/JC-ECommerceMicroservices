using MediatR;
using Stock.Application.Products.Queries.ViewModels;

namespace Stock.Application.Products.Queries
{
    public class GetAllProductsQuery : IRequest<IEnumerable<ProductViewModel>>
    {
    }
}
