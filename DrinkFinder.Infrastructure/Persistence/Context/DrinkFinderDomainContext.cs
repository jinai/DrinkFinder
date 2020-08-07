using DrinkFinder.Common.Constants;
using DrinkFinder.Common.Interfaces;
using DrinkFinder.Infrastructure.Persistence.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace DrinkFinder.Infrastructure.Persistence.Context
{
    public class DrinkFinderDomainContext : DbContext
    {
        public DbSet<Establishment> Establishments { get; set; }
        public DbSet<BusinessHours> BusinessHours { get; set; }
        public DbSet<Picture> Pictures { get; set; }
        public DbSet<News> News { get; set; }

        public DrinkFinderDomainContext() { }

        public DrinkFinderDomainContext(DbContextOptions<DrinkFinderDomainContext> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (optionsBuilder is null)
            {
                throw new ArgumentNullException(nameof(optionsBuilder));
            }

            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(
                    @"Server=(localdb)\mssqllocaldb;Database=DrinkFinder;Trusted_Connection=True;MultipleActiveResultSets=true",
                    b => b.MigrationsHistoryTable("__EFMigrationsHistory", Schemas.Domain));
                optionsBuilder.EnableSensitiveDataLogging();
            }
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            builder.HasDefaultSchema(Schemas.Domain);
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(builder);

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
                        entry.Entity.AddedDate = DateTimeOffset.Now;
                        entry.Entity.IsDeleted = false;
                        break;
                    case EntityState.Modified:
                        entry.Entity.ModifiedDate = DateTimeOffset.Now;
                        break;
                    case EntityState.Deleted:
                        entry.State = EntityState.Modified;
                        entry.Entity.IsDeleted = true;
                        break;
                }
            }

            return base.SaveChangesAsync(cancellationToken);
        }
    }
}