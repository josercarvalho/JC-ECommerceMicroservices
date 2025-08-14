using MediatR;

namespace Stock.Application.Products.Commands
{
    public class DecreaseStockCommand : IRequest<Unit>
    {
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
