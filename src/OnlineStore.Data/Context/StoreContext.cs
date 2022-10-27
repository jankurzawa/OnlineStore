namespace OnlineStore.Data.Context
{
    public class StoreContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Customer> Customer { get; set; }
        public DbSet<Admin> Admin { get; set; }
        public DbSet<CredentialsContainer> CredentialsContainers { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Rating> Ratings { get; set; }
        public DbSet<ProductCategory> ProductCategories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Basket> Baskets { get; set; }
        public DbSet<OrderProducts> OrderProducts { get; set; }
        public DbSet<BasketProduct> BasketProducts { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
                @"Server=(localdb)\mssqllocaldb;Database=OnlineStore;Integrated Security=True");
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<CredentialsContainer>()
                .HasOne(u => u.User)
                .WithOne(c => c.Credentials)
                .HasForeignKey<CredentialsContainer>(c => c.UserId)
                .HasPrincipalKey<User>(c => c.CredentialId);

            builder.Entity<Customer>()
                .HasOne(b => b.Basket)
                .WithOne(c => c.Customer)
                .HasForeignKey<Basket>(c => c.CustomerId);

            builder.Entity<Address>()
                .HasOne(a => a.Customer)
                .WithOne(c => c.Address)
                .HasPrincipalKey<Address>(c => c.AddressId)
                .HasForeignKey<Customer>(c => c.AddressId);

            builder.Entity<Rating>()
                .HasOne(r => r.Product)
                .WithMany(p => p.Ratings)
                .HasForeignKey(r => r.ProductId);

            builder.Entity<Rating>()
                .HasOne(r => r.Customer)
                .WithMany()
                .HasForeignKey(r => r.CustomerId);

            builder.Entity<BasketProduct>()
                .HasOne(bp => bp.Product)
                .WithMany(p => p.BasketProducts)
                .HasForeignKey(bp => bp.ProductId);

            builder.Entity<BasketProduct>()
                .HasOne(bp => bp.Basket)
                .WithMany(p => p.BasketProducts)
                .HasForeignKey(bp => bp.BasketId);

            builder.Entity<Order>()
                .HasMany(o => o.Products)
                .WithOne(p => p.Order)
                .HasForeignKey(o => o.OrderId);

            builder.Entity<Product>()
                .HasMany(p => p.Orders)
                .WithOne(o => o.Product)
                .HasForeignKey(o => o.ProductId);

            builder.Entity<Customer>()
                .HasMany(c => c.Orders)
                .WithOne(o => o.Customer)
                .HasForeignKey(o => o.CustomerId)
                .HasPrincipalKey(c => c.UserId);

            builder.SeedDatabase();
        }
    }
}