namespace OnlineStore.Data.Entities.Store
{
    public class ProductCategory
    {
        public Guid ProductCategoryId { get; set; }
        public string Category { get; set; }
        public bool IsFeatured { get; set; }

        public ProductCategory()
        {
        }
        public ProductCategory(string name, bool isFeatured)
        {
            ProductCategoryId = Guid.NewGuid();
            Category = name;
            IsFeatured = isFeatured;
        }
    }
}
