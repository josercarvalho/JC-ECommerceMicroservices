using MediatR;
using Stock.Application.Products.Queries.ViewModels;
using Stock.Domain.Interfaces;

namespace Stock.Application.Products.Handlers
{
    public class GetProductByIdQueryHandler : IRequestHandler<Queries.GetProductByIdQuery, ProductViewModel>
    {
        private readonly IProductRepository _productRepository;

        public GetProductByIdQueryHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<ProductViewModel> Handle(Queries.GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            var product = await _productRepository.GetByIdAsync(request.Id);
            if (product == null)
            {
                return null; // Or throw a specific exception
            }

            return new ProductViewModel
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                QuantityInStock = product.QuantityInStock
            };
        }
    }
}
