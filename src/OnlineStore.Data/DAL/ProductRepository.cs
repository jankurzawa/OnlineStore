namespace OnlineStore.Data.DAL
{
    public class ProductRepository : IBaseRepository<Product>
    {

        private readonly StoreContext _storeContext;

        public ProductRepository()
            => _storeContext = new StoreContext();

        public void Add(Product entity)
            => _storeContext
            .Products
            .Add(entity);

        public void Delete(Product entity)
            => _storeContext
            .Products
            .Remove(entity);

        public void Edit(Product entity)
            => _storeContext
            .Products
            .Update(entity);

        public List<Product> GetAll()
            => _storeContext
            .Products
            .AsNoTracking().ToList();

        public List<Product> GetAllBy(Func<Product, bool> condition)
            => _storeContext
                .Products
                //.Include(products => products.ProductCategoryId)
                .AsNoTracking()
                .Where(condition)
                .ToList();
        public Product GetSingle(Func<Product, bool> condition)
            => _storeContext
            .Products
            //.Include(product => product.ProductCategoryId)
            .Where(condition)
            .FirstOrDefault();

        public void Save() => _storeContext.SaveChanges();
    }
}
