using Wishlist.Models;
using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace Wishlist.DB
{
    public class DataContext : DbContext
    {
        public DataContext()
            : base("DefaultConnection")
        { }

        public IDbSet<Item> Items { get; set; }


        public static DataContext Create()
        {
            return new DataContext();
        }
        
        public override int SaveChanges()
        {
            UpdateDates();
            try
            {
                return base.SaveChanges();
            }
            catch
            {
                return 0;
            }
        }

        public override async Task<int> SaveChangesAsync()
        {
            UpdateDates();
            return await base.SaveChangesAsync();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
               base.OnModelCreating(modelBuilder);
        }

        public void SetModified(object entity)
        {
            Entry(entity).State = EntityState.Modified;
        }

        private void UpdateDates()
        {
            var modifiedEntities = ChangeTracker.Entries().Where(i => i.State == EntityState.Modified).ToList();
            var modelBaseEntities = modifiedEntities.Where(i => i.Entity is ModelBase);
            foreach (var entity in modelBaseEntities)
            {
                ((ModelBase)entity.Entity).ModifiedOn = DateTime.Now;
            }
            var addedEntities = ChangeTracker.Entries().Where(i => i.State == EntityState.Added).ToList();
            modelBaseEntities = addedEntities.Where(i => i.Entity is ModelBase);
            foreach (var entity in modelBaseEntities)
            {
                ((ModelBase)entity.Entity).CreatedOn = DateTime.Now;
            }
        }
    }
}
