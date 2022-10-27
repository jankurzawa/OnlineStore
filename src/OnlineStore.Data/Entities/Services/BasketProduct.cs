namespace OnlineStore.Data.Entities.Services
{
    public class BasketProduct
    {
        public Guid BasketProductID { get; set; }
        public Basket Basket { get; set; }
        public Guid BasketId { get; set; }
        public Product Product { get; set; }
        public Guid ProductId { get; set; }

        public uint Quantity { get; set; }
        public BasketProduct(Guid basketId, Guid productId, uint quantity)
        {
            BasketProductID = Guid.NewGuid();
            BasketId = basketId;
            ProductId = productId;
            Quantity = quantity;
        }
    }
}
