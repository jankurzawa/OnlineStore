namespace OnlineStore.Data.DAL
{
    public class BasketRepository : IBaseRepository<Basket>
    {

        private readonly StoreContext _storeContext;

        public BasketRepository()
            => _storeContext = new StoreContext();

        public void Add(Basket entity)
            => _storeContext
            .Baskets
            .Add(entity);

        public void Delete(Basket entity)
            => _storeContext
                .Baskets
                .Remove(entity);

        public void Edit(Basket entity)
            => _storeContext
                .Baskets
                .Update(entity);

        public List<Basket> GetAll()
            => _storeContext
                .Baskets
                .Include(basket => basket.BasketProducts)
                .AsNoTracking().ToList();

        public List<Basket> GetAllBy(Func<Basket, bool> condition)
             => _storeContext
                .Baskets.Include(basket => basket.BasketProducts)
                .AsNoTracking()
                .Where(condition)
                .ToList();

        public Basket GetSingle(Func<Basket, bool> condition)
            => _storeContext
                .Baskets.Include(basket => basket.BasketProducts)
                .Include(basket => basket.Customer)
                .Where(condition)
                .FirstOrDefault();

        public void Save()
            => _storeContext
            .SaveChanges();
    }
}
