namespace OnlineStore.Data.Context.Seeders
{
    public static class StoreSeeder
    {
        public static void SeedDatabase(this ModelBuilder builder)
        {
            Random random = new();
            Guid userId = Guid.NewGuid();
            Guid credentialsId = Guid.NewGuid();
            Guid addressId = Guid.NewGuid();
            Guid basketId = Guid.NewGuid();
            Guid productId = Guid.NewGuid();

            List<User> users = new();
            List<CredentialsContainer> credentials = new();
            List<Address> addresses = new();


            users.Add(new Customer()
            {
                UserId = userId, 
                Name = "Anna",
                Surname = "Kowalska",
                AccessLevel = AccessLevel.Customer,
                Credentials = null,
                CredentialId = credentialsId, 
                AddressId = addressId,
                BasketId = basketId,
                
            });

            addresses.Add(new Address()
            {
                AddressId = addressId,
                Country = "Poland",
                City = "Kraków",
                Street = "Wrzosowa",
                HouseNumber = 8,
                DoorNumber = 9,
                PostalCode = 44266,
            });

            Guid userId2 = Guid.NewGuid();
            Guid addressId2 = Guid.NewGuid();

            users.Add(new Customer()
            {
                UserId = userId2, 
                Name = "Anna",
                Surname = "Nowak",
                AccessLevel = AccessLevel.Customer,
                Credentials = null,
                CredentialId = Guid.NewGuid(), 
                AddressId = addressId2,
                BasketId = Guid.NewGuid(),
                
            });

            addresses.Add(new Address()
            {
                AddressId = addressId2,
                Country = "Poland",
                City = "Rybnik",
                Street = "Kwiarowa",
                HouseNumber = 8,
                DoorNumber = 9,
                PostalCode = 47266,
            });

            Guid userId3 = Guid.NewGuid();
            Guid addressId3 = Guid.NewGuid();
            users.Add(new Customer()
            {
                UserId = userId3, 
                Name = "Aniela",
                Surname = "Marek",
                AccessLevel = AccessLevel.Customer,
                Credentials = null,
                CredentialId = Guid.NewGuid(), 
                AddressId = addressId3,
                BasketId = Guid.NewGuid(),
                
            });

            addresses.Add(new Address()
            {
                AddressId = addressId3,
                Country = "Poland",
                City = "Kraków",
                Street = "Wrzosowa",
                HouseNumber = 8,
                DoorNumber = 9,
                PostalCode = 44266,
            });

            Guid userId4 = Guid.NewGuid();
            Guid addressId4 = Guid.NewGuid();
            users.Add(new Customer()
            {
                UserId = userId4, 
                Name = "Anna",
                Surname = "Mika",
                AccessLevel = AccessLevel.Customer,
                Credentials = null,
                CredentialId = Guid.NewGuid(), 
                AddressId = addressId4,
                BasketId = Guid.NewGuid(),
                
            });

            addresses.Add(new Address()
            {
                AddressId = addressId4,
                Country = "Poland",
                City = "Katowice",
                Street = "Wrzosowa",
                HouseNumber = 8,
                DoorNumber = 9,
                PostalCode = 44266,
            });

            builder.Entity<Customer>().HasData(users);
            builder.Entity<Address>().HasData(addresses);
        }
    }
}
