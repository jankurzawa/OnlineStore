namespace OnlineStore.Data.Entities.Services
{
    public class OrderProducts
    {
        [Key]
        public Guid OrderProductsId { get; set; }
        public Product Product { get; set; }
        public Guid ProductId { get; }
        public Order Order { get; set; }
        public Guid OrderId { get; }
        public uint Quantity { get; set; }
        public OrderProducts() { }
        public OrderProducts(Order order, Product product, uint quantity)
        {
            Quantity = quantity;
            OrderId = order.OrderId;
            ProductId = product.ProductId;
            OrderProductsId = Guid.NewGuid();
        }
    }
}
