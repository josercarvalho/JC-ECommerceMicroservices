using MediatR;
using Stock.Domain.Interfaces;

namespace Stock.Application.Products.Handlers
{
    public class DeleteProductCommandHandler : IRequestHandler<Commands.DeleteProductCommand, Unit>
    {
        private readonly IProductRepository _productRepository;

        public DeleteProductCommandHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<Unit> Handle(Commands.DeleteProductCommand request, CancellationToken cancellationToken)
        {
            var product = await _productRepository.GetByIdAsync(request.Id);
            if (product == null)
            {
                // TODO: Handle not found exception
                return Unit.Value;
            }

            await _productRepository.DeleteAsync(request.Id);

            return Unit.Value;
        }
    }
}
