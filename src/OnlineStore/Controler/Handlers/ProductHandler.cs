namespace OnlineStore.Controler.Handlers
{
    public class ProductHandler
    {
        private ProductFactory _productFactory;
        private ProductRepository _productRepository;
        private ProductCategoryRepository _productCategoryRepository;
        private ProductCategoryHandler _productCategoryHandler;
        private readonly IView<Product> _productConsoleView;
        private readonly IInputManager _inputManager;
        private readonly MenuDisplay _display;

        public ProductHandler(ProductRepository productRepository, ProductCategoryHandler productCategoryHandler, IInputManager inputManager, IView<Product> view, MenuDisplay display)
        {
            _productConsoleView = view;
            _inputManager = inputManager;
            _display = display;
            _productFactory = new(inputManager, productCategoryHandler);
            _productCategoryHandler = productCategoryHandler;
            _productRepository = productRepository;
            _productCategoryRepository = new ProductCategoryRepository();
        }

        public void GetProductsFromCaterogy()
        {
            ProductCategory category;
            List<Product> products = new List<Product>();
            _productCategoryHandler.GetAllCategories();
            do
            {
                string categoryName = _inputManager.FetchStringValue("Provide category name:");
                category = _productCategoryRepository.GetSingle(category => category.Category == categoryName);
                if (category != null)
                {
                    products = _productRepository.GetAllBy(product => product.ProductCategoryId == category.ProductCategoryId);
                    _productConsoleView.ShowList(products);
                    _display.PrintMessage("");
                    break;
                }
                else { _display.PrintMessage("Wrong option"); }

            } while (true);
        }
        public void GetAllProducts()
            => _productConsoleView.ShowList(_productRepository.GetAll(););
        public bool CheckAvailabilityOfProduct(Product product)
            => (product.IsActive == true && product.Quantity > 0);
        public void ChangeName(Product product)
        {
            product.Name = _inputManager.FetchStringValue("Enter new name of product:");
            _productRepository.Edit(product);
            _productRepository.Save();
        }

        public void AddNewProduct()
        {
            do
            {
                var newProduct = _productFactory.CreateProduct();
                _productConsoleView.ShowOne(newProduct);

                if (_inputManager.FetchStringValue("[y] - confirm") == "y")
                {
                    _productRepository.Add(newProduct);
                    _productRepository.Save();
                    break;
                }
            } while (true);
        }
        public void ChangeActivity(Product product)
        {
            string answer = _inputManager.FetchStringValue("Do you want to set IsActive as false or true?\n[F]alse / [T]rue ");
            switch (answer)
            {
                case "F":
                    product.IsActive = false;
                    _productRepository.Edit(product);
                    _productRepository.Save();
                    break;
                case "T":
                    product.IsActive = true;
                    _productRepository.Edit(product);
                    _productRepository.Save();
                    break;
                default:
                    _display.PrintMessage("Wrong option.");
                    break;
            }
        }
        public void ChangeProductCategory(Product product)
        {
            ProductCategory productCategory = _productCategoryHandler.GetCategory();
            product.ProductCategoryId = productCategory.ProductCategoryId;
            _productRepository.Edit(product);
            _productRepository.Save();
        }
        public void ChangeDiscount(Product product)
        {
            product.Discount = _inputManager.FetchIntValue("Enter new discount of product:");
            _productRepository.Edit(product);
            _productRepository.Save();
        }
        public void ChangePrice(Product product)
        {
            product.OryginalPrice = _inputManager.FetchDoubleValue("Enter new price of product:");
            _productRepository.Edit(product);
            _productRepository.Save();
        }
        public void ChangeQuantity(Product product)
        {
            int numberToAdd = _inputManager.FetchIntValue("Enter the number of products to be added:\n(If you would like to remove som product write e.g. '-5')");
            product.Quantity += (uint)numberToAdd;
            _productRepository.Edit(product);
            _productRepository.Save();
        }
        public void ChangeQuantity(Product product, int quantity)
        {
            product.Quantity += (uint)quantity;
            _productRepository.Edit(product);
            _productRepository.Save();
        }
        public void ShowProductAvailability()
        {
            Product product = GetProduct();
            bool isAvailableProduct = CheckAvailabilityOfProduct(product);
            if (isAvailableProduct && product.Quantity > 0)
            {
                _display.PrintMessage($"{product.Name} is available");
            }
            else { _display.PrintMessage($"{product.Name} is not available"); }
        }
        public Product GetProduct()
        {
            Product product = null;
            do
            {
                string productName = _inputManager.FetchStringValue("Provide product name:");
                product = _productRepository.GetSingle(product => product.Name == productName);
                if (product != null) { break; }
                else { _display.PrintMessage("Wrong product name."); }
            } while (product == null);

            return product;
        }
    }
}
