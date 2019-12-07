namespace Wishlist.DB
{
    public interface IDataContextFactory
    {
        DataContext GetContext();
    }
}