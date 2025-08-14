using Stock.Domain.Entities;
using Stock.Domain.Exceptions;
using Xunit;

namespace Stock.Tests.Unit
{
    public class ProductTests
    {
        [Fact]
        public void DecreaseStock_ShouldDecreaseQuantity_WhenQuantityIsValid()
        {
            // Arrange
            var product = new Product("Test Product", "Description", 10.0m, 100);
            var quantityToDecrease = 10;

            // Act
            product.DecreaseStock(quantityToDecrease);

            // Assert
            Assert.Equal(90, product.QuantityInStock);
        }

        [Fact]
        public void DecreaseStock_ShouldThrowDomainException_WhenQuantityIsZeroOrNegative()
        {
            // Arrange
            var product = new Product("Test Product", "Description", 10.0m, 100);

            // Act & Assert
            var exception1 = Assert.Throws<DomainException>(() => product.DecreaseStock(0));
            Assert.Equal("A quantidade a ser diminuída deve ser maior que zero.", exception1.Message);

            var exception2 = Assert.Throws<DomainException>(() => product.DecreaseStock(-5));
            Assert.Equal("A quantidade a ser diminuída deve ser maior que zero.", exception2.Message);
        }

        [Fact]
        public void DecreaseStock_ShouldThrowDomainException_WhenInsufficientStock()
        {
            // Arrange
            var product = new Product("Test Product", "Description", 10.0m, 5);

            // Act & Assert
            var exception = Assert.Throws<DomainException>(() => product.DecreaseStock(10));
            Assert.Contains("Estoque insuficiente", exception.Message);
        }

        [Fact]
        public void IncreaseStock_ShouldIncreaseQuantity_WhenQuantityIsValid()
        {
            // Arrange
            var product = new Product("Test Product", "Description", 10.0m, 100);
            var quantityToIncrease = 10;

            // Act
            product.IncreaseStock(quantityToIncrease);

            // Assert
            Assert.Equal(110, product.QuantityInStock);
        }

        [Fact]
        public void IncreaseStock_ShouldThrowDomainException_WhenQuantityIsZeroOrNegative()
        {
            // Arrange
            var product = new Product("Test Product", "Description", 10.0m, 100);

            // Act & Assert
            var exception1 = Assert.Throws<DomainException>(() => product.IncreaseStock(0));
            Assert.Equal("A quantidade a ser aumentada deve ser maior que zero.", exception1.Message);

            var exception2 = Assert.Throws<DomainException>(() => product.IncreaseStock(-5));
            Assert.Equal("A quantidade a ser aumentada deve ser maior que zero.", exception2.Message);
        }
    }
}
