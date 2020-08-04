using DrinkFinder.AuthServer.Models;
using DrinkFinder.Common.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;

namespace DrinkFinder.AuthServer.Data
{
    public class AuthServerDbContext : IdentityDbContext<ApplicationUser, IdentityRole<Guid>, Guid>
    {
        public AuthServerDbContext() { }

        public AuthServerDbContext(DbContextOptions<AuthServerDbContext> options) : base(options) { }

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
                    b => b.MigrationsHistoryTable("__EFMigrationsHistory", nameof(Schema.Auth)));
                optionsBuilder.EnableSensitiveDataLogging();
            }
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.HasDefaultSchema(nameof(Schema.Auth));
            base.OnModelCreating(builder);
        }
    }
}
