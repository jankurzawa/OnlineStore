namespace OnlineStore.Controler.Handlers
{
    internal class AdminHandler : BaseUserHandler<Admin>
    {
        private readonly ProductCategoryHandler _productCategoryHandler;
        private readonly ProductHandler _productHandler;
        private readonly OrderHandler _orderHandler;
        private readonly RateHandler _rateHandler;
        private readonly IView<Order> _viewOrder;
        public AdminHandler(ProductCategoryHandler productCategoryHandler, ProductHandler productHandler, OrderHandler orderHandler,
            RateHandler rateHandler, IInputManager inputManager, IView<Admin> view, MenuDisplay display)
            : base(inputManager, view, display)
        {
            _availableCommands = GetAvailableCommands();
            _productCategoryHandler = productCategoryHandler;
            _productHandler = productHandler;
            _orderHandler = orderHandler;
            _rateHandler = rateHandler;
            _viewOrder = new ViewOrder();
        }
        protected override string[] GetAvailableCommands()
            => new string[] { "1. Create new product category", "2. Show all categories", "3. Edit category", "4. Create new product", "5. Show products",
                "6. Edit product property", "7. Feature product category", "8. Display list of orders with statuses", "9. Review Statistics", "[quit]" };
        public override void RunFeatureBasedOn(string userInput)
        {
            switch (userInput)
            {
                case "1":
                    CreateNewProductCategory();
                    break;
                case "2":
                    DisplayAllProductCategories();
                    break;
                case "3":
                    ChangeCategory();
                    break;
                case "4":
                    CreateNewProduct();
                    break;
                case "5":
                    _productHandler.GetAllProducts();
                    break;
                case "6":
                    EditProduct();
                    break;
                case "7":
                    FeatureProductCategory();
                    break;
                case "8":
                    DisplayListOfOrdersWithStautses();
                    break;
                case "9":
                    ReviewStatistics();
                    break;
                case "quit":
                    break;
                default:
                    _display.PrintMessage("Wrong option");
                    break;
            }
        }

        private void CreateNewProductCategory() 
            => _productCategoryHandler.AddCategory();
        private void DisplayAllProductCategories() 
            => _productCategoryHandler.GetAllCategories();
        private void FeatureProductCategory() 
            => _productCategoryHandler.MarkCategoryAsFeatured();
        private void CreateNewProduct() 
            => _productHandler.AddNewProduct();
        private void ChangeCategory()
        {
            ProductCategory productCategory = _productCategoryHandler.GetCategory();
            _display.PrintMessage($"What option do you like to change for category {productCategory.Category}");
            var selectedOption = _inputManager.FetchStringValue("1.Name\n2.Delete");
            switch (selectedOption)
            {
                case "1":
                    _productCategoryHandler.ChangeCategoryName(productCategory);
                    break;
                case "2":
                    _productCategoryHandler.DeleteCategory(productCategory);
                    break;
                default:
                    _display.PrintMessage("Wrong option");
                    break;
            }
        }

        private void EditProduct()
        {
            _productHandler.GetAllProducts();
            Product product = _productHandler.GetProduct();
            _display.PrintMessage($"What option do you like to change for product\n(Name:\t{product.Name}|Oryginal price:\t{product.OryginalPrice}|Discount:\t{product.Discount}|Quantity:\t{product.Quantity}):");
            var selectedOption = _inputManager.FetchStringValue("1.Name\n2.Category \n3.Price \n4.Quantity \n5.De/Activate \n6.Set discount");
            switch (selectedOption)
            {
                case "1":
                    _productHandler.ChangeName(product);
                    break;
                case "2":
                    _productHandler.ChangeProductCategory(product);
                    break;
                case "3":
                    _productHandler.ChangePrice(product);
                    break;
                case "4":
                    _productHandler.ChangeQuantity(product);
                    break;
                case "5":
                    _productHandler.ChangeActivity(product);
                    break;
                case "6":
                    _productHandler.ChangeDiscount(product);
                    break;
                case "quit":
                    break;
                default:
                    _display.PrintMessage("Wrong option");
                    break;
            }
        }
        private void DisplayListOfOrdersWithStautses() 
            => _viewOrder.ShowList(_orderHandler.GetAll());

        private void ReviewStatistics()
        {
            _display.PrintMessage("Choose option:");
            Product product;
            string option = _inputManager.FetchStringValue("1. Display all ratings.\n2. Display averge of all ratings."
                 + "\n3. Display all ratings for given product\n4. Display average of ratings for given product");
            switch (option)
            {
                case "1":
                    _rateHandler.DisplayAllRatings();
                    break;
                case "2":
                    _rateHandler.DisplayAvergeOfAllRatings();
                    break;
                case "3":
                    product = _productHandler.GetProduct();
                    _rateHandler.DisplayAllRatingsForGivenProduct(product);
                    break;
                case "4":
                    product = _productHandler.GetProduct();
                    _rateHandler.DisplayAverageOfRatingsForGivenProduct(product);
                    break;
                default:
                    _display.PrintMessage("Invalid input");
                    break;
            }
        }
    }
}
