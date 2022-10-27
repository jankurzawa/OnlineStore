namespace OnlineStore.Controler.Handlers
{
    public class CredentialHandler
    {
        private UserFactory userFactory;
        private CredentialRepository credentialRepository;
        private UserRepository userRepository;
        private ConsoleInputSystem inputManager;

        public CredentialHandler()
        {
            userFactory = new();
            credentialRepository = new();
            userRepository = new();
            inputManager = new();
        }
        public User Login()
        {
            User wantedUser = null;

            while (wantedUser == null)
            {
                string login = inputManager.FetchStringValue("login:");
                string password = inputManager.FetchStringValue("password:");
                var wantedUserCredentials = credentialRepository.GetSingle(credentials => credentials.Login == login && credentials.Password == password);
                if (wantedUserCredentials != null) wantedUser = userRepository.GetSingle(user => user.CredentialId == wantedUserCredentials.CredentialsId);
            }
            return wantedUser;
        }
        public void Register()
            => userFactory.CreateNewUser();
    }
}
