namespace OnlineStore.Data.Entities.Store
{
    public class Product
    {
        [Key]
        public Guid ProductId { get; set; }
        public bool IsActive { get; set; }
        public ProductCategory CategoryName { get; set; }
        public Guid ProductCategoryId { get; set; }
        public string Name { get; set; }
        public double OryginalPrice { get; set; }
        public double Price { get => OryginalPrice - (OryginalPrice * Discount); }
        public int Discount { get; set; }
        public uint Quantity { get; set; }
        public List<BasketProduct>? BasketProducts { get; set; }
        public List<OrderProducts> Orders { get; set; }
        public List<Rating>? Ratings { get; set; }

        public Product()
        {
        }

        public Product(string name, double price, uint quantity)
        {
            ProductId = Guid.NewGuid();
            Name = name;
            OryginalPrice = price;
            Quantity = quantity;
            BasketProducts = new();
            Orders = new();
            Ratings = new();
        }
    }
}
