namespace OnlineStore.View.Interfaces
{
    public class ViewProductCategory : IView<ProductCategory>
    {
        public void ShowList(List<ProductCategory> entities)
            => entities.ForEach(ShowOne);

        public void ShowOne(ProductCategory entity)
            => Console.WriteLine($"Category: {entity.Category}, Is Features: {entity.IsFeatured}");
    }
}
