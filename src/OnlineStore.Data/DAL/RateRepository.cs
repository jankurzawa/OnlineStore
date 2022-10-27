namespace OnlineStore.Data.DAL
{
    public class RateRepository : IBaseRepository<Rating>
    {
        private readonly StoreContext _storeContext;

        public RateRepository() => _storeContext = new StoreContext();
        public void Add(Rating entity) => _storeContext.Ratings.Add(entity);
        public void Delete(Rating entity) => _storeContext.Ratings.Remove(entity);
        public void Edit(Rating entity) => _storeContext.Ratings.Update(entity);
        public List<Rating> GetAll() => _storeContext.Ratings.AsNoTracking().ToList();
        public List<Rating> GetAllBy(Func<Rating, bool> condition)
            => _storeContext.Ratings
                .Include(singleRating => singleRating.Product)
                .Include(singleRating => singleRating.Customer)
                .AsNoTracking()
                .Where(condition)
                .ToList();
        public double GetAverageOfAllRatings()
        {
            if (_storeContext.Ratings.Any())
                return _storeContext.Ratings.AsNoTracking().Average(x => (double)x.Rate);
            return default;
        }
        public Rating GetSingle(Func<Rating, bool> condition)
            => _storeContext.Ratings
                .Include(singleRating => singleRating.Product)
                .Include(singleRating => singleRating.Customer)
                .Where(condition).FirstOrDefault();
        public void Save() => _storeContext.SaveChanges();
    }
}
