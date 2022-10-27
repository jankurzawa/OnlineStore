namespace OnlineStore.Data.Entities.Users
{
    public class Admin : User
    {
        public Admin()
        {
        }
        public Admin(string name, string surname, CredentialsContainer credentialsContainer)
            : base(name, surname, credentialsContainer)
        {
            AccessLevel = AccessLevel.Admin;
        }
    }
}
