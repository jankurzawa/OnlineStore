namespace OnlineStore.Controler.Handlers
{
    public abstract class BaseUserHandler<T> : IUserDataManager
    {
        protected UserRepository userRepository;
        protected ProductHandler productHandler;
        protected ProductCategoryHandler productCategoryHandler;

        protected IInputManager _inputManager;
        protected IView<T> _view;
        protected MenuDisplay _display;
        protected string[] _availableCommands;

        protected BaseUserHandler(IInputManager inputManager, IView<T> view, MenuDisplay display)
        {
            _inputManager = inputManager;
            _view = view;
            _display = display;
            userRepository = new UserRepository();
        }

        public void Run()
        {
            string userInput = null;
            _display.ClearScreen();
            while (userInput != "quit")
            {
                _display.PrintOptions(new List<string>(_availableCommands));
                userInput = _inputManager.FetchStringValue("Provide option").Trim();

                RunFeatureBasedOn(userInput);

                _inputManager.FetchStringValue("Press enter to proceed...");
                _display.ClearScreen();
            }
        }

        public abstract void RunFeatureBasedOn(string userInput);
        protected abstract string[] GetAvailableCommands();
    }
}
