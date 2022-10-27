namespace OnlineStore.Controler.Handlers
{
    public class ProductCategoryHandler
    {
        private ProductCategoryRepository _productCategoryRepository;
        private readonly IView<ProductCategory> _categoryConsoleView;
        private IInputManager _inputManager;
        private MenuDisplay _display;


        public ProductCategoryHandler(IInputManager inputManager, IView<ProductCategory> view, MenuDisplay display)
        {
            _productCategoryRepository = new ProductCategoryRepository();
            _categoryConsoleView = view;
            _inputManager = inputManager;
            _display = display;
        }

        public void GetAllCategories()
            => _categoryConsoleView.ShowList(_productCategoryRepository.GetAll());

        public ProductCategory CreateCategory()
        {
            string categoryName;
            do
            {
                categoryName = _inputManager.FetchStringValue("Provide new category name");
                if (string.IsNullOrWhiteSpace(categoryName) || categoryName.Length < 2)
                {
                    Console.WriteLine("Incorrect name or surname. Try again");
                }
                else { break; }
            } while (true);

            ProductCategory category = new ProductCategory(categoryName, false);

            return category;
        }
        public void AddCategory()
        {
            do
            {
                ProductCategory newCategory = CreateCategory();
                if (_inputManager.FetchStringValue("[y] - confirm") == "y")
                {
                    _productCategoryRepository.Add(newCategory);
                    _productCategoryRepository.Save();
                    break;
                }
            } while (true);
        }
        public void ChangeCategoryName(ProductCategory productCategory)
        {
            productCategory.Category = _inputManager.FetchStringValue("Enter new category name:");
            _productCategoryRepository.Edit(productCategory);
            _productCategoryRepository.Save();
        }
        public ProductCategory GetCategory()
        {
            GetAllCategories();
            ProductCategory productCategory;
            do
            {
                try
                {
                    var productCategoryName = _inputManager.FetchStringValue($"Enter the name of category:");
                    productCategory = _productCategoryRepository.GetSingle(product => product.Category == productCategoryName);
                    if (productCategory != null) { break; }
                    else { _display.PrintMessage("Wrong category name!"); }

                }
                catch (Exception ex) { _display.PrintMessage(ex.Message); }
            } while (true);

            return productCategory;
        }
        public void MarkCategoryAsFeatured()
        {
            ProductCategory productCategory = GetCategory();
            var isFeatured = _inputManager.FetchStringValue($"The category is featured? [T]rue / [F]alse");
            switch (isFeatured)
            {
                case "T":
                    productCategory.IsFeatured = true;
                    break;
                case "F":
                    productCategory.IsFeatured = false;
                    break;
                default:
                    Console.WriteLine("Wrong option!");
                    break;
            }
            _productCategoryRepository.Edit(productCategory);
            _productCategoryRepository.Save();

        }
        public void DeleteCategory(ProductCategory productCategory)
        {
            _productCategoryRepository.Delete(productCategory);
            _productCategoryRepository.Save();
        }
    }
}
