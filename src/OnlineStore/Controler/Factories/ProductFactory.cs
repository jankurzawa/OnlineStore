namespace OnlineStore.Controler.Factories
{
    public class ProductFactory
    {
        private readonly IInputManager _inputManager;
        private readonly ProductCategoryHandler _productCategoryHandler;

        public ProductFactory(IInputManager inputManager, ProductCategoryHandler productCategoryHandler)
        {
            _inputManager = inputManager;
            _productCategoryHandler = productCategoryHandler;
        }
        public Product CreateProduct()
        {
            var productIsActive = true;
            string productName;
            while (true)
            {
                productName = _inputManager.FetchStringValue("Product name:"); ;
                if (productName.Length < 2)
                    Console.WriteLine("Incorrect product name. Try again");
                else  break; 
            }
            double productPrice;
            while (true)
            {
                productPrice = _inputManager.FetchDoubleValue("Product price");
                if (productPrice > 0) break;
            }
            var productQuantity = _inputManager.FetchUintValue("Product quantity:");

            var productCategoryId = _productCategoryHandler.GetCategory().ProductCategoryId;
            return new Product(productName, productPrice, productQuantity)
            {
                ProductCategoryId = productCategoryId,
                IsActive = productIsActive,
                Discount = 0
            };
        }
    }
}
