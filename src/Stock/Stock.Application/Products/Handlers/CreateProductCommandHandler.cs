using MediatR;
using Stock.Domain.Entities;
using Stock.Domain.Interfaces;

namespace Stock.Application.Products.Handlers
{
    public class CreateProductCommandHandler : IRequestHandler<Commands.CreateProductCommand, Guid>
    {
        private readonly IProductRepository _productRepository;

        public CreateProductCommandHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<Guid> Handle(Commands.CreateProductCommand request, CancellationToken cancellationToken)
        {
            var product = new Product(request.Name, request.Description, request.Price, request.QuantityInStock);
            await _productRepository.AddAsync(product);
            return product.Id;
        }
    }
}
