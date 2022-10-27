namespace OnlineStore.View
{
    public class ViewAdmin : IView<Admin>
    {
        public void ShowList(List<Admin> entities)
        {
            foreach (var entity in entities) ShowOne(entity);
        }

        public void ShowOne(Admin entity)
            => Console.WriteLine("Name and surname: {0} {1}", entity.Name, entity.Surname);
    }
}
