namespace OnlineStore.Data.DAL
{
    public class ProductCategoryRepository : IBaseRepository<ProductCategory>
    {

        private readonly StoreContext _storeContext;

        public ProductCategoryRepository()
            => _storeContext = new StoreContext();
        public void Add(ProductCategory entity)
            => _storeContext
                .ProductCategories
                .Add(entity);
        public void Delete(ProductCategory entity)
            => _storeContext
            .ProductCategories
            .Remove(entity);
        public void Edit(ProductCategory entity)
            => _storeContext
            .ProductCategories
            .Update(entity);
        public List<ProductCategory> GetAll()
            => _storeContext
            .ProductCategories.OrderByDescending(x => x.IsFeatured).ThenByDescending(x => x.Category)
            .AsNoTracking().ToList();
        public List<ProductCategory> GetAllBy(Func<ProductCategory, bool> condition)
            => _storeContext
            .ProductCategories
            .AsNoTracking()
            .Where(condition)
            .ToList();

        public ProductCategory GetSingle(Func<ProductCategory, bool> condition)
            => _storeContext
            .ProductCategories
            .Where(condition)
            .FirstOrDefault();
        public void Save() => _storeContext.SaveChanges();
    }
}
