namespace OnlineStore.View
{
    public class ViewCustomer : IView<Customer>
    {
        public void ShowList(List<Customer> entities)
        {
            foreach (var entity in entities) ShowOne(entity);
        }

        public void ShowOne(Customer entity)
            => Console.WriteLine("Name and surname: {0} {1}", entity.Name, entity.Surname);
        public void ShowAddress(Address entity)
        {
            StringBuilder sb = new();
            sb.Append("Address:\n");
            sb.Append(entity.Street + " " + entity.HouseNumber + "/" + entity.DoorNumber + "\n");
            sb.Append(entity.PostalCode + " " + entity.City + ", " + entity.Street + "\n");
            Console.WriteLine(sb.ToString());
        }
    }
}
