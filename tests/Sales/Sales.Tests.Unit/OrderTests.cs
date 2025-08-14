using Sales.Domain.Entities;
using Sales.Domain.Exceptions;
using Xunit;

namespace Sales.Tests.Unit
{
    public class OrderTests
    {
        [Fact]
        public void AddOrderItem_ShouldAddOrderItemToList_WhenQuantityIsValid()
        {
            // Arrange
            var order = new Order(Guid.NewGuid());
            var productId = Guid.NewGuid();
            var productName = "Test Product";
            var price = 10.0m;
            var quantity = 2;

            // Act
            order.AddOrderItem(productId, productName, price, quantity);

            // Assert
            Assert.Single(order.OrderItems);
            Assert.Equal(productId, order.OrderItems.First().ProductId);
            Assert.Equal(quantity, order.OrderItems.First().Quantity);
        }

        [Fact]
        public void AddOrderItem_ShouldThrowDomainException_WhenQuantityIsZeroOrNegative()
        {
            // Arrange
            var order = new Order(Guid.NewGuid());
            var productId = Guid.NewGuid();
            var productName = "Test Product";
            var price = 10.0m;

            // Act & Assert
            var exception = Assert.Throws<DomainException>(() => order.AddOrderItem(productId, productName, price, 0));
            Assert.Equal("A quantidade do item do pedido deve ser maior que zero.", exception.Message);
        }

        [Fact]
        public void ConfirmOrder_ShouldChangeStatusToConfirmed_WhenStatusIsPending()
        {
            // Arrange
            var order = new Order(Guid.NewGuid());

            // Act
            order.ConfirmOrder();

            // Assert
            Assert.Equal("Confirmed", order.Status);
        }

        [Fact]
        public void ConfirmOrder_ShouldThrowDomainException_WhenStatusIsNotPending()
        {
            // Arrange
            var order = new Order(Guid.NewGuid());
            order.ConfirmOrder(); // First confirm it

            // Act & Assert
            var exception = Assert.Throws<DomainException>(() => order.ConfirmOrder());
            Assert.Equal("Somente pedidos pendentes podem ser confirmados.", exception.Message);
        }

        [Fact]
        public void CancelOrder_ShouldChangeStatusToCancelled_WhenStatusIsNotConfirmed()
        {
            // Arrange
            var order = new Order(Guid.NewGuid());

            // Act
            order.CancelOrder();

            // Assert
            Assert.Equal("Cancelled", order.Status);
        }

        [Fact]
        public void CancelOrder_ShouldThrowDomainException_WhenStatusIsConfirmed()
        {
            // Arrange
            var order = new Order(Guid.NewGuid());
            order.ConfirmOrder(); // Confirm the order

            // Act & Assert
            var exception = Assert.Throws<DomainException>(() => order.CancelOrder());
            Assert.Equal("Pedidos confirmados não podem ser cancelados diretamente.", exception.Message);
        }
    }
}
