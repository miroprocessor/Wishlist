namespace Wishlist.DB
{
    public class DataContextFactory : IDataContextFactory
    {
        public DataContext GetContext()
        {
            return new DataContext();
        }
    }
}