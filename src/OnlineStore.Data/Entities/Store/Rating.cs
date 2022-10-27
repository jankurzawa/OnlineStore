namespace OnlineStore.Data.Entities.Store
{
    public class Rating
    {
        [Key]
        public Guid RatingId { get; set; }
        public RatingLevel Rate { get; set; }
        public Product Product { get; set; }
        public Guid ProductId { get; set; }
        public Customer Customer { get; set; }
        public Guid CustomerId { get; set; }

        public Rating()
        {
        }
        public Rating(RatingLevel ratingLevel, Product product, Customer customer)
        {
            RatingId = Guid.NewGuid();
            Rate = ratingLevel;
            ProductId = product.ProductId;
            CustomerId = customer.UserId;
        }
    }
}
