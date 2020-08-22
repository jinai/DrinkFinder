using DrinkFinder.Infrastructure.Persistence.Entities;
using DrinkFinder.Infrastructure.Persistence.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace DrinkFinder.Infrastructure.Persistence
{
    public class DrinkFinderDomainContext : DbContext
    {
        public DbSet<Establishment> Establishments { get; set; }
        public DbSet<BusinessHours> BusinessHours { get; set; }
        public DbSet<Picture> Pictures { get; set; }
        public DbSet<News> News { get; set; }

        public DrinkFinderDomainContext(DbContextOptions<DrinkFinderDomainContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            // Filter out soft deleted entities
            builder.Entity<Establishment>().HasQueryFilter(e => !e.IsDeleted);
            builder.Entity<News>().HasQueryFilter(e => !e.IsDeleted);
            builder.Entity<Picture>().HasQueryFilter(e => !e.IsDeleted);
            builder.Entity<BusinessHours>().HasQueryFilter(e => !e.IsDeleted);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            foreach (var entry in ChangeTracker.Entries<IEntity<Guid>>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        // Only overwrite if it wasn't explicitly set
                        if (entry.Entity.AddedDate == default)
                        {
                            entry.Entity.AddedDate = DateTimeOffset.Now;
                        }

                        entry.Entity.IsDeleted = false;
                        break;
                    case EntityState.Modified:
                        // Only overwrite if it wasn't explicitly set
                        entry.Entity.ModifiedDate ??= DateTimeOffset.Now;
                        break;
                    case EntityState.Deleted:
                        entry.State = EntityState.Unchanged;
                        entry.Entity.IsDeleted = true;
                        entry.Entity.ModifiedDate = DateTimeOffset.Now;
                        break;
                }
            }

            return base.SaveChangesAsync(cancellationToken);
        }
    }
}