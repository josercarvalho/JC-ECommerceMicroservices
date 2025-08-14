using MediatR;
using Stock.Application.Products.Queries.ViewModels;

namespace Stock.Application.Products.Queries
{
    public class GetProductByIdQuery : IRequest<ProductViewModel>
    {
        public Guid Id { get; set; }
    }
}
