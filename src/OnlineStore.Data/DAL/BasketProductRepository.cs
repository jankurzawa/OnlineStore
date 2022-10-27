namespace OnlineStore.Data.DAL
{
    public class BasketProductRepository : IBaseRepository<BasketProduct>
    {
        private readonly StoreContext _storeContext;

        public BasketProductRepository()
        {
            _storeContext = new StoreContext();
        }

        public void Add(BasketProduct entity)
        {
            _storeContext
            .BasketProducts
            .Add(entity);
        }

        public void Delete(BasketProduct entity)
        {
            _storeContext
                .BasketProducts
                .Remove(entity);
        }

        public void Edit(BasketProduct entity)
        {
            _storeContext
                .BasketProducts
                .Update(entity);
        }

        public List<BasketProduct> GetAll()
        {
            return _storeContext
                .BasketProducts
                .Include(BasketProducts => BasketProducts.Product)
                .Include(BasketProducts => BasketProducts.Basket)
                .AsNoTracking().ToList();
        }
        public List<BasketProduct> GetAllWithProp(Func<BasketProduct, bool> condition)
        {
            return _storeContext
                .BasketProducts
                .Include(BasketProducts => BasketProducts.Product)
                .Include(BasketProducts => BasketProducts.Basket)
                .AsNoTracking().Where(condition).ToList();
        }

        public BasketProduct GetSingle(Func<BasketProduct, bool> condition)
        {
            return _storeContext
                .BasketProducts
                .Include(BasketProduct => BasketProduct.Product)
                .Include(BasketProduct => BasketProduct.Basket)
                .AsNoTracking()
                .Where(condition).FirstOrDefault();
        }

        public void Save()
        {
            _storeContext
            .SaveChanges();
        }
    }
}
