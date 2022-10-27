namespace OnlineStore.Data.Entities.Services
{
    public class Order
    {
        [Key]
        public Guid OrderId { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime OrderPaidAt { get; set; }
        public OrderStatus Status {
            get
            {
                if (OrderPaidAt < DateTime.Now.AddDays(-1))
                {
                    return OrderStatus.OnTheWay;
                }
                else if (OrderPaidAt > DateTime.Now.AddDays(-3))
                {
                    return OrderStatus.Delivered;
                }
                return Status;
            }
            set { } }
        public string Country { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public int HouseNumber { get; set; }
        public int DoorNumber { get; set; }
        public int PostalCode { get; set; }
        public Customer Customer { get; set; }
        public Guid CustomerId { get; set; }

        public List<OrderProducts> Products { get; set; }
        public Order()
        {
        }
        public Order(Address address, Customer customer)
        {
            Status = OrderStatus.Submitted;
            OrderId = Guid.NewGuid();
            OrderDate = DateTime.Now;
            Country = address.Country;
            City = address.City;
            Street = address.Street;
            HouseNumber = address.HouseNumber;
            DoorNumber = address.DoorNumber;
            PostalCode = address.PostalCode;
            Products = new();
            CustomerId = customer.UserId;
        }
    }
}
