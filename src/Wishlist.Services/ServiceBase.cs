using Wishlist.DB;

namespace Wishlist.Services
{
    public abstract class ServiceBase
    {
        protected readonly IDataContextFactory DataContextFactory;

        protected DataContext DataContext()
        {
            return DataContextFactory.GetContext();
        }

        protected ServiceBase(IDataContextFactory dataContextFactory)
        {
            DataContextFactory = dataContextFactory;
        }
    }
}
