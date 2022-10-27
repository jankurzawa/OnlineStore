namespace OnlineStore.Data.DAL
{
    public class UserRepository : IBaseRepository<User>
    {

        private readonly StoreContext _storeContext;

        public UserRepository()
            => _storeContext = new StoreContext();

        public void Add(User entity)
            => _storeContext
            .Users
            .Add(entity);

        public void Delete(User entity)
            => _storeContext
                .Users
                .Remove(entity);

        public void Edit(User entity)
            => _storeContext
            .Users
            .Update(entity);

        public List<User> GetAll()
            => _storeContext
                .Users.Include(singleUser => singleUser.Credentials)
                .AsNoTracking()
                .ToList();

        public List<User> GetAllBy(Func<User, bool> condition)
            => _storeContext.Users
                .Include(singleUser => singleUser.Credentials)
                .AsNoTracking()
                .Where(condition)
                .ToList();

        public User GetSingle(Func<User, bool> condition)
            => _storeContext.Users
                .Include(singleUser => singleUser.Credentials)
                .Where(condition).FirstOrDefault();
        public Customer GetSingleCustomer(Func<Customer, bool> condition)
            => _storeContext.Customer
                .Include(singleUser => singleUser.Credentials)
                .Include(customer => customer.Orders)
                .Include(customer => customer.Address)
                .Where(condition).FirstOrDefault();


        public void Save()
            => _storeContext
            .SaveChanges();
    }
}
