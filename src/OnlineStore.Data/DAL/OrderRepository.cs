namespace OnlineStore.Data.DAL
{
    public class OrderRepository : IBaseRepository<Order>
    {

        private readonly StoreContext _storeContext;

        public OrderRepository()
            => _storeContext = new StoreContext();

        public void Add(Order entity)
            => _storeContext.Orders.Add(entity);

        public void Delete(Order entity)
            => _storeContext.Orders.Remove(entity);

        public void Edit(Order entity)
            => _storeContext.Orders.Update(entity);

        public List<Order> GetAll()
            => _storeContext.Orders
            .Include(order => order.Customer)
            .Include(order => order.Products)
            .AsNoTracking().ToList();

        public Order GetSingle(Func<Order, bool> condition)
            => _storeContext.Orders
            .Include(order => order.Customer)
            .Include(order => order.Products)
            .AsNoTracking()
            .Where(condition)
            .FirstOrDefault();

        public List<Order> GetAllBy(Func<Order, bool> condition)
            => _storeContext.Orders
            .Include(order => order.Customer)
            .Include(order => order.Products)
            .Where(condition)
            .ToList();

        public List<Order> GetAllByStatus(OrderStatus status)
            => _storeContext.Orders
            .Include(order => order.Customer)
            .Include(order => order.Products)
            .Where(order => order.Status.Equals(status))
            .AsNoTracking().ToList();

        public List<Order> GetAllPaydInDates(DateTime start, DateTime end)
            => _storeContext.Orders
            .Include(order => order.Customer)
            .Include(order => order.Products)
            .Where(order => order.OrderPaidAt >= start && order.OrderPaidAt <= end)
            .AsNoTracking().ToList();

        public List<OrderProducts> GetProdactFromOrder(Order order)
            => _storeContext.OrderProducts
            .Include(op => op.Product)
            .Where(op => op.OrderId == order.OrderId)
            .AsNoTracking().ToList();

        public void Save()
            => _storeContext.SaveChanges();
    }
}
