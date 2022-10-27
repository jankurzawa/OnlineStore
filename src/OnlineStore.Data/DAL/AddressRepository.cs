namespace OnlineStore.Data.DAL
{
    public class AddressRepository : IBaseRepository<Address>
    {

        private readonly StoreContext _storeContext;

        public AddressRepository()
            => _storeContext = new StoreContext();

        public void Add(Address entity)
            => _storeContext.Addresses.Add(entity);

        public void Delete(Address entity)
            => _storeContext.Addresses.Remove(entity);

        public void Edit(Address entity)
            => _storeContext.Addresses.Update(entity);

        public List<Address> GetAll()
            => _storeContext.Addresses.AsNoTracking().ToList();

        public List<Address> GetAllBy(Func<Address, bool> condition)
            => _storeContext.Addresses.AsNoTracking().Where(condition).ToList();

        public Address GetSingle(Func<Address, bool> condition)
            => _storeContext.Addresses.Where(condition).FirstOrDefault();

        public void Save()
            => _storeContext.SaveChanges();
    }
}
