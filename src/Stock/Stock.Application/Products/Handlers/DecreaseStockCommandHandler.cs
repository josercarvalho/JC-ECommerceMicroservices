using MediatR;
using Stock.Domain.Interfaces;

namespace Stock.Application.Products.Handlers
{
    public class DecreaseStockCommandHandler : IRequestHandler<Commands.DecreaseStockCommand, Unit>
    {
        private readonly IProductRepository _productRepository;

        public DecreaseStockCommandHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<Unit> Handle(Commands.DecreaseStockCommand request, CancellationToken cancellationToken)
        {
            var product = await _productRepository.GetByIdAsync(request.ProductId);
            if (product == null)
            {
                // TODO: Handle not found exception
                return Unit.Value;
            }

            product.DecreaseStock(request.Quantity);
            await _productRepository.UpdateAsync(product);

            return Unit.Value;
        }
    }
}
