namespace OnlineStore.Controler.Handlers
{
    public class CustomerHandler : BaseUserHandler<Customer>
    {
        private readonly RateHandler _rateHandler;
        private OrderHandler _orderHandler;
        private BasketHandler _basketHandler;
        private Customer _user;
        private readonly ProductCategoryHandler _productCategoryHandler;
        private readonly ProductHandler _productHandler;
        private readonly BasketRepository basketRepository = new BasketRepository();
        private readonly BasketProductRepository _basketProducRepository = new BasketProductRepository();
        private readonly AddressFactory _addressFactory;
        private readonly IView<Order> _orderView;
        private readonly ViewCustomer _customerConsoleView;

        public CustomerHandler(
            User user,
            ProductCategoryHandler productCategoryHandler,
            ProductHandler productHandler,
            IInputManager inputManager,
            IView<Customer> view,
            MenuDisplay display,
            AddressFactory addressFactory,
            IView<Order> orderView,
            OrderHandler orderHandler,
            RateHandler rateHandler
            )
            : base(inputManager, view, display)
        {
            _customerConsoleView = new ViewCustomer();
            _user = user as Customer;
            _productCategoryHandler = productCategoryHandler;
            _productHandler = productHandler;
            _basketHandler = new BasketHandler(_user, inputManager, display);
            _addressFactory = addressFactory;
            _orderView = orderView;
            _orderHandler = orderHandler;
            _availableCommands = GetAvailableCommands();
            _rateHandler = rateHandler;
        }
        public override void RunFeatureBasedOn(string userInput)
        {
            switch (userInput)
            {
                case "1":
                    GetAllCategories();
                    break;
                case "2":
                    GetListAllProductsFromCategory();
                    break;
                case "3":
                    _productHandler.ShowProductAvailability();
                    break;
                case "4":
                    AddProductToBasket();
                    break;
                case "5":
                    DeleteProductFromBasket();
                    break;
                case "6":
                    ClearBasket();
                    break;
                case "7":
                    DisplayAllProductsFromBasket();
                    break;
                case "8":
                    EditQuantityOfProduktInBasket();
                    break;
                case "9":
                    SubmitOrder(_user);
                    break;
                case "10":
                    DisplayPreviousOrders();
                    break;
                case "11":
                    RateProduct();
                    break;
                case "12":
                    PayForOrder(_user);
                    break;
                case "quit":
                    break;
                default:
                    _display.PrintMessage("Wrong option");
                    break;
            }
        }

        public void GetAllCategories() 
            => _productCategoryHandler.GetAllCategories();
        public void GetListAllProductsFromCategory() 
            => _productHandler.GetProductsFromCaterogy();
        private void RateProduct() 
            => _rateHandler.AddRate(_user);
        private void DisplayPreviousOrders()
        
            => _orderView.ShowList(_orderHandler.GetAllByCustomer(_user));
        private void AddProductToBasket()
        {
            GetListAllProductsFromCategory();
            Product product = _productHandler.GetProduct();
            int productCount = _inputManager.FetchIntValue("Provide product count:");
            _basketHandler.AddProduct(product, productCount);
            _productHandler.ChangeQuantity(product, -productCount);
        }
        public void EditQuantityOfProduktInBasket()
        {
            DisplayAllProductsFromBasket();
            Product product = _productHandler.GetProduct();
            _basketHandler.ChangeProductCount(product);
        }
        public void DeleteProductFromBasket()
        {
            DisplayAllProductsFromBasket();
            Product product = _productHandler.GetProduct();
            _basketHandler.RemoveProduct(product, out int quantity);
            _productHandler.ChangeQuantity(product, quantity);
        }
        public void ClearBasket() 
            => _basketHandler.ClearBasket();
        public void DisplayAllProductsFromBasket()
        {
            var basket = basketRepository.GetSingle(b => b.CustomerId == _user.UserId);
            double finalprice = 0;
            var basketProducts = _basketProducRepository.GetAllWithProp(x => x.BasketId == basket.BasketId);
            foreach (var item in basketProducts)
            {
                double totalprice = item.Product.Price * item.Quantity;
                finalprice += totalprice;
                Console.WriteLine("{0}, {1} per piece, {2} pieces. Total price {3} \n",
                    item.Product.Name, item.Product.Price, item.Quantity, totalprice);
            }
            Console.WriteLine("Final Price: " + finalprice);
        }

        protected override string[] GetAvailableCommands()
        {
            return new string[]
            {
                "1. Show all categories",
                "2. Show product from category",
                "3. Check product availability",
                "4. Add product to basket",
                "5. Remove product from basket",
                "6. Clear basket",
                "7. Display basket",
                "8. Change count product in basket",
                "9. Submit basket and make order",
                "10. Display previous orders",
                "11. Rate product",
                "12. Pay for order",
                "quit. to logout"
            };
        }

        public void SubmitOrder(Customer user)
        {
            var customer = getCustomerFromUser(user);

            var basket = basketRepository.GetSingle(b => b.CustomerId == customer.UserId);
            var basketProducts = _basketProducRepository.GetAllWithProp(b => b.BasketId == basket.BasketId);
            Address shipAddress = GetAddressForShipping(customer);
            var order = _orderHandler.CreateNewOrder(customer, shipAddress);
            foreach (var item in basketProducts)
            {
                order = _orderHandler.AddProduct(order, item.Product, item.Quantity);
            }
            _orderHandler.Submit(order, customer);
        }
        private Customer getCustomerFromUser(User user) 
            => userRepository.GetSingleCustomer(x => x.UserId == user.UserId);
        public Address GetAddressForShipping(Customer user)
        {
            Address address = null;
            if (user.Address != null)
            {
                _customerConsoleView.ShowAddress(user.Address);
                Console.WriteLine("Do you want to use you default address?");
                bool run = true;
                do
                {
                    switch (_inputManager.FetchStringValue("[yes] or [no]"))
                    {
                        case "yes":
                            address = user.Address;
                            run = false;
                            break;
                        case "no":
                            address = _addressFactory.GetAddress();
                            run = false;
                            break;
                        default:
                            break;
                    }
                } while (run);
            }
            else address = _addressFactory.GetAddress();
            return address;
        }
        private void PayForOrder(Customer user)
        {
            var orders = _orderHandler.GetAllByCustomer(user);
            _orderView.ShowList(orders);
            string userInput = _inputManager.FetchStringValue("Choose number of order or write [product] to show products in order:");
            int numberOfOrder = 0;
            switch (userInput)
            {
                case "1":
                    numberOfOrder = 0;
                    break;
                case "2":
                    numberOfOrder = 1;
                    break;
                case "product":
                    ShowProductInOrder(orders[_inputManager.FetchIntValue("Choose order to see products")-1], user);
                    break;
            }
            _orderHandler.PayFor(orders[numberOfOrder]);
        }
        public void ShowProductInOrder(Order order, Customer user)
        {
            var products = _orderHandler.GetProdFromOrder(order);
            ShowOrderProducts(products);
            Console.ReadLine();
            Console.Clear();
            PayForOrder(user);
        }
        public void ShowOrderProducts(List<OrderProducts> op)
            => op.ForEach(p => Console.WriteLine($"{p.Product.Name} : {p.Quantity}\n"));
    }
}
