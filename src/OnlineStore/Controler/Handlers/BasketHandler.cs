namespace OnlineStore.Controler.Handlers
{
    public class BasketHandler
    {
        private Basket _basket;
        private readonly IInputManager _inputManager;
        private BasketRepository _basketRepository;
        private BasketProductRepository _basketProductRepository;

        public BasketHandler(Customer customer, IInputManager inputManager, MenuDisplay display)
        {
            _inputManager = inputManager;
            _basketRepository = new BasketRepository();
            _basketProductRepository = new();
            Guid customerID = customer.UserId;
            Console.WriteLine(customerID);

            _basket = _basketRepository.GetSingle(basket => basket.Customer.UserId == customerID);
        }

        public void AddProduct(Product product, int count)
        {
            BasketProduct basketProduct = _basketProductRepository.GetSingle(bp => bp.ProductId == product.ProductId);
            if (basketProduct == null)
            {
                _basketProductRepository.Add(new BasketProduct(_basket.BasketId, product.ProductId, (uint)count));
                _basketProductRepository.Save();
            }
            else
            {
                ChangeQuantity(product, count);
            }
        }

        public void RemoveProduct(Product product, out int quantity)
        {
            quantity = 0;
            for (int i = _basket.BasketProducts.Count - 1; i >= 0; i--)
            {
                if (product.ProductId == _basket.BasketProducts[i].ProductId)
                {
                    quantity = (int)_basket.BasketProducts[i].Quantity;
                    _basketProductRepository.Delete(_basket.BasketProducts[i]);
                }
            }
            _basketProductRepository.Save();
        }
        public void ChangeQuantity(Product product, int quantity)
        {
            foreach (BasketProduct basketProduct in _basket.BasketProducts)
            {
                if (basketProduct.ProductId == product.ProductId)
                {
                    basketProduct.Quantity += (uint)quantity;
                    _basketProductRepository.Edit(basketProduct);
                    _basketProductRepository.Save();
                }
            }
        }
        public void ChangeProductCount(Product product)
        {
            int quantity = _inputManager.FetchIntValue("Enter the number of product quantity:");

            foreach (BasketProduct basketProduct in _basket.BasketProducts)
            {
                if (basketProduct.ProductId == product.ProductId)
                {
                    basketProduct.Quantity = (uint)quantity;
                    _basketProductRepository.Edit(basketProduct);
                    _basketProductRepository.Save();
                }
            }
        }
        public void ClearBasket()
        {
            foreach (BasketProduct basketProduct in _basket.BasketProducts)
            {
                _basketProductRepository.Delete(basketProduct);
            }
            _basketProductRepository.Save();
        }
    }
}
