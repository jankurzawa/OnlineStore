namespace OnlineStore.Controler.Handlers
{
    public class RateHandler
    {
        private RateRepository _RateRepository;
        private ProductHandler _ProductHandler;
        private IInputManager _InputManager;

        public RateHandler(RateRepository rateRepository, ProductHandler productHandler, IInputManager inputSystem)
        {
            _RateRepository = rateRepository;
            _ProductHandler = productHandler;
            _InputManager = inputSystem;
        }
        public void AddRate(Customer customer)
        {
            var rating = CreateRating(customer);
            _RateRepository.Add(rating);
            _RateRepository.Save();
        }
        public void DisplayAvergeOfAllRatings() 
            => Console.WriteLine($"Average of all ratings: {_RateRepository.GetAverageOfAllRatings()}");
        public void DisplayAllRatings() 
            => _RateRepository.GetAll().ForEach(x => Console.WriteLine(x.ToString()));
        public void DisplayAllRatingsForGivenProduct(Product product) 
            => _RateRepository.GetAllBy(x => x.ProductId == product.ProductId).ForEach(x => Console.WriteLine(x));
        public void DisplayAverageOfRatingsForGivenProduct(Product product) 
            => Console.WriteLine($"Average of ratings for {product.Name}: {_RateRepository.GetAllBy(r => r.ProductId == product.ProductId).Average(p => (int)p.Rate)}");
        private Rating CreateRating(Customer customer)
        {
            _ProductHandler.GetAllProducts();
            return new Rating(GetLevelOfRating(), _ProductHandler.GetProduct(), customer);
           
        }
        private RatingLevel GetLevelOfRating()
        {
            int levelOfRating = -1;
            while (levelOfRating  <0 || levelOfRating > 4)
            {
                levelOfRating = _InputManager.FetchIntValue("Choose level of Rating (0 - 4)");
            }
            switch (levelOfRating)
            {
                case 0:
                    return RatingLevel.VeryBad;
                case 1:
                    return RatingLevel.Bad;
                case 2:
                    return RatingLevel.Good;
                case 3:
                    return RatingLevel.Great;
                case 4:
                    return RatingLevel.Excellent;
                default:
                    return default;
            }
        }
    }
}
