namespace OnlineStore.View
{
    public class ViewProduct : IView<Product>
    {
        public void ShowList(List<Product> entities)
        {
            for (int i = 0; i < entities.Count; i++)
            {
                Console.WriteLine("Item no. {0}:", i + 1);
                ShowOneWithProp(entities[i]);
                Console.Write("\n");
            }
        }

        public void ShowOne(Product entity)
            => Console.WriteLine($"Product name: {entity.Name},  price: {entity.Price}, " +
                $"quantity: {entity.Quantity}, is available: {entity.IsActive}");

        public void ShowOneWithProp(Product entity)
        {
            ShowOne(entity);
            if (entity.Discount > 0) Console.WriteLine("Regular price: {0}", entity.OryginalPrice);
            if (entity.Discount > 0) Console.WriteLine("Discount: {0}", entity.Discount);
        }
    }
}
