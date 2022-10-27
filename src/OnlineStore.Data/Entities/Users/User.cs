namespace OnlineStore.Data.Entities.Users
{
    public abstract class User
    {
        [Key]
        public Guid UserId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public AccessLevel AccessLevel { get; set; }
        public CredentialsContainer Credentials { get; set; }
        public Guid CredentialId { get; set; }

        public User()
        {
        }
        public User(string name, string surname, CredentialsContainer credentialsContainer)
        {
            UserId = Guid.NewGuid();
            Name = name;
            Surname = surname;
            Credentials = credentialsContainer;
            CredentialId = credentialsContainer.CredentialsId;
        }
    }
}
