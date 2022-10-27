namespace OnlineStore.View
{
    public class ViewOrder : IView<Order>
    {
        public void ShowList(List<Order> entities)
        {
            int position = 1;
            foreach (var entity in entities)
            {
                Console.Write(position + "." );
                ShowOne(entity);
                position++;
            }
            Console.WriteLine("\n");
        }

        public void ShowOne(Order entity)
        {
            Console.WriteLine($"{entity.Customer.Name} {entity.Customer.Surname} on {entity.OrderDate.ToString()}");
            Console.WriteLine($"Status: {entity.Status.ToString()}");
        }

        public void ShowOneWithProp(Order entity)
        {
            ShowOne(entity);
            if (entity.Status != OrderStatus.Submitted) Console.WriteLine("Payment received: " + entity.OrderPaidAt.ToString());
            ShowAddress(entity);
        }
        private void ShowAddress(Order entity)
        {
            StringBuilder sb = new();
            sb.Append("Ship to:\n");
            sb.Append(entity.Street + " " + entity.HouseNumber + "/" + entity.DoorNumber + "\n");
            sb.Append(entity.PostalCode + " " + entity.City + ", " + entity.Street + "\n");
            Console.WriteLine(sb.ToString());
        }
    }
}
