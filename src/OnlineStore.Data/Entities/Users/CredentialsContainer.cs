namespace OnlineStore.Data.Entities.Users
{
    public class CredentialsContainer
    {
        [Key]
        public Guid CredentialsId { get; set; }
        public string Email { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public User User { get; set; }

        public Guid UserId { get; set; }

        public CredentialsContainer()
        {
        }

        public CredentialsContainer(string email, string login, string password)
        {
            CredentialsId = Guid.NewGuid();
            Email = email;
            Login = login;
            Password = password;
        }
    }
}
