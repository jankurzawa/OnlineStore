namespace OnlineStore.Data.Entities.Services
{
    public class Basket
    {
        [Key]
        public Guid BasketId { get; set; }
        public List<BasketProduct> BasketProducts { get; set; }
        public Guid ProductId { get; set; }
        public Customer Customer { get; set; }
        public Guid CustomerId { get; set; }

        public Basket()
        {
            BasketId = Guid.NewGuid();
            BasketProducts = new();
        }
    }
}
