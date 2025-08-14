using Stock.Domain.Exceptions;

namespace Stock.Domain.Entities
{
    public class Product
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public decimal Price { get; private set; }
        public int QuantityInStock { get; private set; }

        // Construtor para EF Core
        protected Product() { }

        public Product(string name, string description, decimal price, int quantityInStock)
        {
            Id = Guid.NewGuid();
            Name = name;
            Description = description;
            Price = price;
            QuantityInStock = quantityInStock;
        }

        public void UpdateDetails(string name, string description, decimal price)
        {
            Name = name;
            Description = description;
            Price = price;
        }

        public void DecreaseStock(int quantity)
        {
            if (quantity <= 0)
            {
                throw new DomainException("A quantidade a ser diminuída deve ser maior que zero.");
            }

            if (QuantityInStock < quantity)
            {
                throw new DomainException($"Estoque insuficiente para o produto '{Name}'. Disponível: {QuantityInStock}, Solicitado: {quantity}.");
            }

            QuantityInStock -= quantity;
        }

        public void IncreaseStock(int quantity)
        {
            if (quantity <= 0)
            {
                throw new DomainException("A quantidade a ser aumentada deve ser maior que zero.");
            }

            QuantityInStock += quantity;
        }
    }
}
