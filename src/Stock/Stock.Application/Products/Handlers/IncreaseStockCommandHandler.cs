using MediatR;
using Stock.Domain.Interfaces;

namespace Stock.Application.Products.Handlers
{
    public class IncreaseStockCommandHandler : IRequestHandler<Commands.IncreaseStockCommand, Unit>
    {
        private readonly IProductRepository _productRepository;

        public IncreaseStockCommandHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<Unit> Handle(Commands.IncreaseStockCommand request, CancellationToken cancellationToken)
        {
            var product = await _productRepository.GetByIdAsync(request.ProductId);
            if (product == null)
            {
                // TODO: Handle not found exception
                return Unit.Value;
            }

            product.IncreaseStock(request.Quantity);
            await _productRepository.UpdateAsync(product);

            return Unit.Value;
        }
    }
}
