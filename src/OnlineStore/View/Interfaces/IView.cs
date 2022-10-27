namespace OnlineStore.View.Interfaces
{
    public interface IView<T>
    {
        public void ShowOne(T entity);
        public void ShowList(List<T> entities);
    }
}
