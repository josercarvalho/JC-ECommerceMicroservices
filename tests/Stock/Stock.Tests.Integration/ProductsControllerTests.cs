using System.Net;
using System.Net.Http.Json;
using Xunit;
using Stock.API;
using Stock.Application.Products.Commands;
using Stock.Application.Products.Queries.ViewModels;

namespace Stock.Tests.Integration
{
    public class ProductsControllerTests : IClassFixture<CustomWebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;

        public ProductsControllerTests(CustomWebApplicationFactory<Program> factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task CreateProduct_ReturnsCreatedProduct()
        {
            // Arrange
            var command = new CreateProductCommand
            {
                Name = "Integration Test Product",
                Description = "Description for integration test",
                Price = 99.99m,
                QuantityInStock = 10
            };

            // Act
            var response = await _client.PostAsJsonAsync("/api/products", command);

            // Assert
            response.EnsureSuccessStatusCode();
            var productId = await response.Content.ReadFromJsonAsync<Guid>();
            Assert.NotEqual(Guid.Empty, productId);

            var getResponse = await _client.GetAsync($"/api/products/{productId}");
            getResponse.EnsureSuccessStatusCode();
            var product = await getResponse.Content.ReadFromJsonAsync<ProductViewModel>();
            Assert.NotNull(product);
            Assert.Equal(command.Name, product.Name);
        }

        [Fact]
        public async Task GetAllProducts_ReturnsProducts()
        {
            // Arrange - ensure at least one product exists
            await _client.PostAsJsonAsync("/api/products", new CreateProductCommand { Name = "Product 1", Price = 10, QuantityInStock = 5 });

            // Act
            var response = await _client.GetAsync("/api/products");

            // Assert
            response.EnsureSuccessStatusCode();
            var products = await response.Content.ReadFromJsonAsync<IEnumerable<ProductViewModel>>();
            Assert.NotNull(products);
            Assert.True(products.Any());
        }

        [Fact]
        public async Task GetProductById_ReturnsNotFound_WhenProductDoesNotExist()
        {
            // Act
            var response = await _client.GetAsync($"/api/products/{Guid.NewGuid()}");

            // Assert
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task UpdateProduct_ReturnsNoContent()
        {
            // Arrange
            var createCommand = new CreateProductCommand { Name = "Original Product", Price = 10, QuantityInStock = 5 };
            var createResponse = await _client.PostAsJsonAsync("/api/products", createCommand);
            var productId = await createResponse.Content.ReadFromJsonAsync<Guid>();

            var updateCommand = new UpdateProductCommand { Id = productId, Name = "Updated Product", Description = "New Desc", Price = 15 };

            // Act
            var response = await _client.PutAsJsonAsync($"/api/products/{productId}", updateCommand);

            // Assert
            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);

            var getResponse = await _client.GetAsync($"/api/products/{productId}");
            var updatedProduct = await getResponse.Content.ReadFromJsonAsync<ProductViewModel>();
            Assert.Equal(updateCommand.Name, updatedProduct.Name);
            Assert.Equal(updateCommand.Description, updatedProduct.Description);
            Assert.Equal(updateCommand.Price, updatedProduct.Price);
        }

        [Fact]
        public async Task DeleteProduct_ReturnsNoContent()
        {
            // Arrange
            var createCommand = new CreateProductCommand { Name = "Product to Delete", Price = 10, QuantityInStock = 5 };
            var createResponse = await _client.PostAsJsonAsync("/api/products", createCommand);
            var productId = await createResponse.Content.ReadFromJsonAsync<Guid>();

            // Act
            var response = await _client.DeleteAsync($"/api/products/{productId}");

            // Assert
            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);

            var getResponse = await _client.GetAsync($"/api/products/{productId}");
            Assert.Equal(HttpStatusCode.NotFound, getResponse.StatusCode);
        }
    }
}
