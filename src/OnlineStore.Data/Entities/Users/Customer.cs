namespace OnlineStore.Data.Entities.Users
{
    public class Customer : User
    {
        public Basket Basket { get; set; }
        public Guid BasketId { get; set; }
        public Address Address { get; set; }
        public Guid AddressId { get; set; }
        public ICollection<Order> Orders { get; set; }

        public Customer()
        {
        }
        public Customer(string name, string surname, CredentialsContainer credentialsContainer, Address address, Basket basket) : base(name, surname, credentialsContainer)
        {
            AccessLevel = AccessLevel.Customer;
            Basket = basket;
            BasketId = basket.BasketId;
            Address = address;
            AddressId = address.AddressId;
            Orders = new List<Order>();
        }
    }
}
