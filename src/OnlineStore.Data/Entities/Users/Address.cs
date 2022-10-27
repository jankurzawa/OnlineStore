namespace OnlineStore.Data.Entities.Users
{
    public class Address
    {
        [Key]
        public Guid AddressId { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public int HouseNumber { get; set; }
        public int DoorNumber { get; set; }
        public int PostalCode { get; set; }
        public Customer Customer { get; set; }
        public Guid CustomerId { get; set; }

        public Address(string country, string city, string street, int houseNumber, int doorNumber, int postalCode)
        {
            AddressId = Guid.NewGuid();
            Country = country;
            City = city;
            Street = street;
            HouseNumber = houseNumber;
            DoorNumber = doorNumber;
            PostalCode = postalCode;
        }

        public Address()
        {
        }
    }
}
