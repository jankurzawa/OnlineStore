namespace OnlineStore.Controler.Handlers
{
    public class AppHandler
    {
        private CredentialHandler credentialHandler;
        private IInputManager inputManager;
        private IUserDataManager currentHandler;
        private MenuDisplay _menuDisplay;

        public AppHandler()
        {
            credentialHandler = new CredentialHandler();
            inputManager = new ConsoleInputSystem();
            _menuDisplay = new MenuDisplay();
        }
        public void Run()
        {
            string input = "";
            while (!input.Equals("QUIT"))
            {
                System.Console.Clear();
                input = inputManager.FetchStringValue(
                    "Welcome to system. Do you want to [R]egister, [L]ogin or [Quit]\nEnter your option:")
                    .ToUpper();

                switch (input)
                {
                    case "R":
                        credentialHandler.Register();
                        break;
                    case "L":
                        AssignUserBasedOnCredential();
                        currentHandler.Run();
                        break;
                    case "QUIT":
                        break;
                    default:
                        inputManager.FetchStringValue("Incorrect option - press enter to proceed");
                        break;
                }
            }
        }
        private void AssignUserBasedOnCredential()
        {
            var loggedUser = credentialHandler.Login();
            ProductCategoryHandler productCategoryHandler = new ProductCategoryHandler(
                inputManager, new ViewProductCategory(), _menuDisplay);

            ProductHandler productHandler = new ProductHandler(new ProductRepository(),
                productCategoryHandler, inputManager, new ViewProduct(), _menuDisplay);
            OrderHandler orderHandler = new(new OrderRepository(), new UserRepository());
            RateHandler rateHandler = new(new RateRepository(), productHandler, inputManager);

            switch (loggedUser.AccessLevel)
            {
                case AccessLevel.Admin:
                    currentHandler = new AdminHandler(productCategoryHandler, productHandler, orderHandler,
                        rateHandler, inputManager, new ViewAdmin(), _menuDisplay);
                    break;
                case AccessLevel.Customer:
                    currentHandler = new CustomerHandler(
                        loggedUser,
                        productCategoryHandler,
                        productHandler,
                        inputManager,
                        new ViewCustomer(),
                        _menuDisplay,
                        new AddressFactory(inputManager),
                        new ViewOrder(),
                        new OrderHandler(new OrderRepository(), new UserRepository()),
                        new RateHandler(new RateRepository(), productHandler, inputManager)
                        );
                    break;
            }
        }
    }
}
