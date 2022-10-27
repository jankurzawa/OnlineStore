namespace OnlineStore.Controler.Factories
{
    public class UserFactory
    {
        private UserRepository userRepository;
        private CredentialRepository credentialRepository;
        private IInputManager inputManager;
        private Validation validation;
        private readonly AddressFactory addressFactory;


        public UserFactory()
        {
            userRepository = new();
            credentialRepository = new();
            inputManager = new ConsoleInputSystem();
            validation = new();
            addressFactory = new AddressFactory(inputManager);
        }
        public void CreateNewUser()
        {
            string name;
            string login;
            string surname;
            string email;
            string password;

            while (true)
            {
                Regex nameRegex = new Regex(@"^[A-Z]{2,20}\-?[A-Z]{0,20}$");
                name = validation.checkInputWithRegexString(nameRegex, "Name");

                Regex surnameRegex = nameRegex;
                surname = validation.checkInputWithRegexString(surnameRegex, "Surname");

                Regex mailRegex = new Regex(@"^[a-z0-9]+\.?[a-z0-9]+@[a-z]+\.[a-z]{2,3}$");
                email = validation.checkInputWithRegexString(mailRegex, "Mail");

                Regex regexLogin = new Regex(@"^[\S]{2,15}$");

                while (true)
                {
                    login = validation.checkInputWithRegexString(regexLogin, "Login");

                    var ableUser = credentialRepository.GetSingle(user => user.Login == login);
                    if (ableUser == null) break;
                }

                Regex regexPassword = regexLogin;
                password = validation.checkInputWithRegexString(regexPassword, "Password");

                if (inputManager.FetchStringValue("[y] - confirm") == "y") break;
            }

            var credentials = new CredentialsContainer(email, login, password);

            if (name == "ADMINADMIN")
            {
                var user = new Admin(name, surname, credentials);

                userRepository.Add(user);
                userRepository.Save();
            }
            else
            {
                var address = addressFactory.GetAddress();

                var basket = new Basket();
                var user = new Customer(name, surname, credentials, address, basket);

                userRepository.Add(user);
                userRepository.Save();
            }
        }
    }
}