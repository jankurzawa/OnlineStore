namespace OnlineStore.Data.DAL
{
    public interface IBaseRepository<T> where T : class
    {
        public List<T> GetAll();
        public T GetSingle(Func<T, bool> condition);
        public void Add(T entity);
        public void Edit(T entity);
        public void Delete(T entity);
        public void Save();
    }
}
