namespace OnlineStore.Data.DAL
{
    public class CredentialRepository : IBaseRepository<CredentialsContainer>
    {

        private readonly StoreContext _storeContext;

        public CredentialRepository()
            => _storeContext = new StoreContext();

        public void Add(CredentialsContainer entity)
            => _storeContext
            .CredentialsContainers
            .Add(entity);
        public void Delete(CredentialsContainer entity)
            => _storeContext
            .CredentialsContainers
            .Remove(entity);

        public void Edit(CredentialsContainer entity)
            => _storeContext
            .CredentialsContainers
            .Update(entity);
        public CredentialsContainer GetSingle(Func<CredentialsContainer, bool> condition)
            => _storeContext
                .CredentialsContainers
                .Where(condition)
                .FirstOrDefault();
        public List<CredentialsContainer> GetAllBy(Func<CredentialsContainer, bool> condition)
            => _storeContext
                .CredentialsContainers
                .AsNoTracking()
                .Where(condition)
                .ToList();
        public List<CredentialsContainer> GetAll()
            => _storeContext
                .CredentialsContainers
                .Include(credentialContainer => credentialContainer.User)
                .AsNoTracking().ToList();
        public void Save()
            => _storeContext
                .SaveChanges();
    }
}
