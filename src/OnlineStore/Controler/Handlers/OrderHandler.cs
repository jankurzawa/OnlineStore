namespace OnlineStore.Controler.Handlers
{
    public class OrderHandler
    {
        private OrderRepository _orderRepository;
        public OrderHandler(OrderRepository orderRepository)
            => _orderRepository = orderRepository;

        public List<Order> GetAll() => _orderRepository.GetAll();
        public List<Order> GetAllByCustomer(Customer customer) => _orderRepository.GetAllBy(order => order.Customer.UserId == customer.UserId);
        public Order CreateNewOrder(Customer customer, Address address) => new Order(address, customer);
        public Order AddProduct(Order order, Product product, uint quantity)
        {
            var resultOrder = order;
            var currentOrderProduct = order.Products.Find(x => x.ProductId == product.ProductId);
            if (currentOrderProduct == null)
            {
                currentOrderProduct = new OrderProducts(order, product, quantity);
                resultOrder.Products.Add(currentOrderProduct);
            }
            else resultOrder.Products.Find(x => x.ProductId == product.ProductId).Quantity += quantity;
            return resultOrder;
        }
        public void Submit(Order order, Customer customer)
        {
            customer.Orders.Add(order);
            _orderRepository.Add(order);
            _orderRepository.Save();
        }
        public bool PayFor(Order order)
        {
            bool payd;
            if (order == null) payd = false;
            else
            {
                order.Status = OrderStatus.Paid;
                order.OrderPaidAt = DateTime.Now;
                try
                {
                    _orderRepository.Edit(order);
                    _orderRepository.Save();
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                } 
                payd = true;
            }
            return payd;
        }
        public List<OrderProducts> GetProdFromOrder(Order order) 
            => _orderRepository.GetProdactFromOrder(order);
    }
}
