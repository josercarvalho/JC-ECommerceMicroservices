using MediatR;
using Stock.Domain.Interfaces;

namespace Stock.Application.Products.Handlers
{
    public class UpdateProductCommandHandler : IRequestHandler<Commands.UpdateProductCommand, Unit>
    {
        private readonly IProductRepository _productRepository;

        public UpdateProductCommandHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<Unit> Handle(Commands.UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var product = await _productRepository.GetByIdAsync(request.Id);
            if (product == null)
            {
                // TODO: Handle not found exception
                return Unit.Value;
            }

            product.UpdateDetails(request.Name, request.Description, request.Price);
            await _productRepository.UpdateAsync(product);

            return Unit.Value;
        }
    }
}
