using NinjaDomain.Classes;
using System;
using System.Data;
using System.Linq;
using NinjaDomain.Services;
using System.Data.Entity;

namespace NinjaDomain.Data
{
    public class NinjaContext : DbContext
    {
        public NinjaContext()
        : base("ApplicationDBConn")
        {
        }
        public DbSet<Ninja> Ninjas { get; set; }
        public DbSet<Clan> Clans { get; set; }
        public DbSet<NinjaEquipment> Equipment { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Types()
                .Configure(c => c.Ignore("IsDirty"));
            base.OnModelCreating(modelBuilder);
        }

        public override int SaveChanges()
        {
            foreach (var history in this.ChangeTracker.Entries()
              .Where(e => e.Entity is IModificationHistory && (e.State == EntityState.Added ||
                      e.State == EntityState.Modified))
               .Select(e => e.Entity as IModificationHistory)
              )
            {
                history.DateModified = DateTime.Now;
                if (history.DateCreated == DateTime.MinValue)
                {
                    history.DateCreated = DateTime.Now;
                }
            }
            int result = base.SaveChanges();
            foreach (var history in this.ChangeTracker.Entries()
                                          .Where(e => e.Entity is IModificationHistory)
                                          .Select(e => e.Entity as IModificationHistory)
            )
            {
                history.IsDirty = false;
            }
            return result;
        }

    }
}
