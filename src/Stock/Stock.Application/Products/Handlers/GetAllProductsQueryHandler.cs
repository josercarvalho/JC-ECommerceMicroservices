using MediatR;
using Stock.Application.Products.Queries.ViewModels;
using Stock.Domain.Interfaces;

namespace Stock.Application.Products.Handlers
{
    public class GetAllProductsQueryHandler : IRequestHandler<Queries.GetAllProductsQuery, IEnumerable<ProductViewModel>>
    {
        private readonly IProductRepository _productRepository;

        public GetAllProductsQueryHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<IEnumerable<ProductViewModel>> Handle(Queries.GetAllProductsQuery request, CancellationToken cancellationToken)
        {
            var products = await _productRepository.GetAllAsync();
            return products.Select(p => new ProductViewModel
            {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description,
                Price = p.Price,
                QuantityInStock = p.QuantityInStock
            });
        }
    }
}
