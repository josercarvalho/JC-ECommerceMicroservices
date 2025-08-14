using Sales.Domain.Exceptions;

namespace Sales.Domain.Entities
{
    public class Order
    {
        public Guid Id { get; private set; }
        public Guid CustomerId { get; private set; }
        public DateTime OrderDate { get; private set; }
        public string Status { get; private set; } // Ex: "Pending", "Confirmed", "Cancelled"

        public void UpdateStatus(string newStatus)
        {
            // Basic validation for allowed statuses
            if (newStatus != "Pending" && newStatus != "Confirmed" && newStatus != "Cancelled")
            {
                throw new DomainException($"Status inválido: {newStatus}. Status permitidos: Pending, Confirmed, Cancelled.");
            }

            // Add more complex state transition rules here if needed
            // For example, cannot change from Confirmed to Pending
            Status = newStatus;
        }
        public ICollection<OrderItem> OrderItems { get; private set; }

        // Construtor para EF Core
        protected Order() { }

        public Order(Guid customerId)
        {
            Id = Guid.NewGuid();
            CustomerId = customerId;
            OrderDate = DateTime.UtcNow;
            Status = "Pending"; // Status inicial
            OrderItems = new List<OrderItem>();
        }

        public void AddOrderItem(Guid productId, string productName, decimal price, int quantity)
        {
            if (quantity <= 0)
            {
                throw new DomainException("A quantidade do item do pedido deve ser maior que zero.");
            }

            var orderItem = new OrderItem(Id, productId, productName, price, quantity);
            OrderItems.Add(orderItem);
        }

        public void ConfirmOrder()
        {
            if (Status != "Pending")
            {
                throw new DomainException("Somente pedidos pendentes podem ser confirmados.");
            }
            Status = "Confirmed";
        }

        public void CancelOrder()
        {
            if (Status == "Confirmed")
            {
                throw new DomainException("Pedidos confirmados não podem ser cancelados diretamente.");
            }
            Status = "Cancelled";
        }
    }
}
