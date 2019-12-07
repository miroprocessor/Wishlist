using Wishlist.DB;
using Wishlist.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Linq;

namespace Wishlist.Services
{
    public class ItemsService : ServiceBase
    {
        public ItemsService(IDataContextFactory dataContextFactory)
            : base(dataContextFactory)
        { }

        public async Task<List<Item>> Get(string userId)
        {
            using (var dbContext = DataContext())
            {
                return await dbContext.Items.Where(i => i.UserId == userId)
                    .OrderByDescending(i => i.CreatedOn)
                    .ToListAsync();
            }
        }

        public async Task<Item> Get(int itemId)
        {
            using (var dbContext = DataContext())
            {
                return await dbContext.Items.SingleOrDefaultAsync(i => i.Id == itemId);
            }
        }

        public async Task Add(Item entity)
        {
            using (var dbContext = DataContext())
            {
                dbContext.Items.Add(entity);
                await dbContext.SaveChangesAsync();
            }
        }

        public async Task Edit(Item entity)
        {
            using (var dbContext = DataContext())
            {
                dbContext.SetModified(entity);
                await dbContext.SaveChangesAsync();
            }
        }

        public async Task Delete(Item entity)
        {
            using (var dbContext = DataContext())
            {
                dbContext.Items.Attach(entity);
                dbContext.Items.Remove(entity);
                await dbContext.SaveChangesAsync();
            }
        }
    }
}
