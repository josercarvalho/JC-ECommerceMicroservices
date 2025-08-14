using System.Net;
using System.Net.Http.Json;
using Xunit;
using Sales.API;
using Sales.Application.Orders.Commands;
using Sales.Application.Orders.ViewModels;

namespace Sales.Tests.Integration
{
    public class OrdersControllerTests : IClassFixture<CustomWebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;

        public OrdersControllerTests(CustomWebApplicationFactory<Program> factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task CreateOrder_ReturnsCreatedOrder()
        {
            // Arrange
            var command = new CreateOrderCommand
            {
                CustomerId = Guid.NewGuid(),
                Items = new List<OrderItemCommand>
                {
                    new OrderItemCommand { ProductId = Guid.NewGuid(), ProductName = "Test Product 1", Price = 10.0m, Quantity = 1 },
                    new OrderItemCommand { ProductId = Guid.NewGuid(), ProductName = "Test Product 2", Price = 20.0m, Quantity = 2 }
                }
            };

            // Act
            var response = await _client.PostAsJsonAsync("/api/orders", command);

            // Assert
            response.EnsureSuccessStatusCode();
            var order = await response.Content.ReadFromJsonAsync<OrderViewModel>();
            Assert.NotNull(order);
            Assert.NotEqual(Guid.Empty, order.Id);
            Assert.Equal(command.CustomerId, order.CustomerId);
            Assert.Equal(command.Items.Count, order.OrderItems.Count);
        }

        [Fact]
        public async Task GetAllOrders_ReturnsOrders()
        {
            // Arrange - ensure at least one order exists
            await _client.PostAsJsonAsync("/api/orders", new CreateOrderCommand
            {
                CustomerId = Guid.NewGuid(),
                Items = new List<OrderItemCommand>
                {
                    new OrderItemCommand { ProductId = Guid.NewGuid(), ProductName = "Product A", Price = 5, Quantity = 1 }
                }
            });

            // Act
            var response = await _client.GetAsync("/api/orders");

            // Assert
            response.EnsureSuccessStatusCode();
            var orders = await response.Content.ReadFromJsonAsync<IEnumerable<OrderViewModel>>();
            Assert.NotNull(orders);
            Assert.True(orders.Any());
        }

        [Fact]
        public async Task GetOrderById_ReturnsNotFound_WhenOrderDoesNotExist()
        {
            // Act
            var response = await _client.GetAsync($"/api/orders/{Guid.NewGuid()}");

            // Assert
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task UpdateOrderStatus_ReturnsNoContent()
        {
            // Arrange
            var createCommand = new CreateOrderCommand
            {
                CustomerId = Guid.NewGuid(),
                Items = new List<OrderItemCommand>
                {
                    new OrderItemCommand { ProductId = Guid.NewGuid(), ProductName = "Product B", Price = 12, Quantity = 3 }
                }
            };
            var createResponse = await _client.PostAsJsonAsync("/api/orders", createCommand);
            var createdOrder = await createResponse.Content.ReadFromJsonAsync<OrderViewModel>();

            var updateCommand = new UpdateOrderStatusCommand { OrderId = createdOrder.Id, NewStatus = "Confirmed" };

            // Act
            var response = await _client.PutAsJsonAsync($"/api/orders/{createdOrder.Id}/status", updateCommand);

            // Assert
            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);

            var getResponse = await _client.GetAsync($"/api/orders/{createdOrder.Id}");
            var updatedOrder = await getResponse.Content.ReadFromJsonAsync<OrderViewModel>();
            Assert.Equal(updateCommand.NewStatus, updatedOrder.Status);
        }
    }
}
