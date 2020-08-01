using DrinkFinder.Common.Enums;
using DrinkFinder.Common.ValueObjects;
using DrinkFinder.Infrastructure.Persistence.Context;
using DrinkFinder.Infrastructure.Persistence.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace DrinkFinder.Infrastructure.Persistence.Extensions
{
    public static class DatabaseInitializer
    {
        public static IHost InitializeDatabase(this IHost host)
        {
            if (host is null)
            {
                throw new ArgumentNullException(nameof(host));
            }

            using var scope = host.Services.CreateScope();

            var context = scope.ServiceProvider.GetRequiredService<DrinkFinderContext>();
            context.Database.EnsureDeleted();
            context.Database.Migrate();

            var culture = CultureInfo.CurrentCulture;

            var e1 = new Establishment
            {
                Id = Guid.NewGuid(),
                ShortCode = "shortcode1",
                Name = "Bar 1",
                Description = "Le super Bar 1",
                Type = EstablishmentType.Bar,
                Status = EstablishmentStatus.Approved,
                VATNumber = "BE0148584865",
                Address = new Address("Rue Truc", "N° 1", "1000", "Bruxelles", "Belgique"),
                Socials = new Socials(new Uri("https://www.instagram.com/bar1"), new Uri("https://www.facebook.com/bar1"), new Uri("https://www.twitter.com/bar1"), null),
                ContactInfo = new ContactInfo("bar1pro@email.com", "bar1@email.com", "0460225254"),
                AddedDate = DateTime.Parse("31/07/2020 14:22:16", culture)
            };
            var e2 = new Establishment
            {
                Id = Guid.NewGuid(),
                ShortCode = "shortcode2",
                Name = "Bar 2",
                Description = "Le super bar 2",
                Type = EstablishmentType.Bar,
                Status = EstablishmentStatus.Approved,
                VATNumber = "BE0245583865",
                Address = new Address("Rue Bidule", "N° 2", "1000", "Bruxelles", "Belgique"),
                Socials = new Socials(new Uri("https://www.instagram.com/bar2"), new Uri("https://www.facebook.com/bar2"), new Uri("https://www.twitter.com/bar2"), null),
                ContactInfo = new ContactInfo("bar2pro@email.com", "bar2@email.com", "0487625954"),
                AddedDate = DateTime.Parse("31/07/2020 15:22:16", culture)
            };

            var bh1 = new List<BusinessHours>
            {
                new BusinessHours
                {
                    Id = Guid.NewGuid(),
                    Day = Day.Monday,
                    OpeningHour = TimeSpan.Parse("09:00", culture),
                    ClosingHour = TimeSpan.Parse("19:00", culture),
                    AddedDate = DateTime.Now,
                    Establishment = e1
                },
                new BusinessHours
                {
                    Id = Guid.NewGuid(),
                    Day = Day.Tuesday,
                    OpeningHour = TimeSpan.Parse("09:00", culture),
                    ClosingHour = TimeSpan.Parse("19:00", culture),
                    AddedDate = DateTime.Now,
                    Establishment = e1
                },
                new BusinessHours
                {
                    Id = Guid.NewGuid(),
                    Day = Day.Wednesday,
                    OpeningHour = TimeSpan.Parse("09:00", culture),
                    ClosingHour = TimeSpan.Parse("19:00", culture),
                    AddedDate = DateTime.Now,
                    Establishment = e1
                },
                new BusinessHours
                {
                    Id = Guid.NewGuid(),
                    Day = Day.Thursday,
                    OpeningHour = TimeSpan.Parse("09:00", culture),
                    ClosingHour = TimeSpan.Parse("19:00", culture),
                    AddedDate = DateTime.Now,
                    Establishment = e1
                },
                new BusinessHours
                {
                    Id = Guid.NewGuid(),
                    Day = Day.Friday,
                    OpeningHour = TimeSpan.Parse("09:00", culture),
                    ClosingHour = TimeSpan.Parse("19:00", culture),
                    AddedDate = DateTime.Now,
                    Establishment = e1
                },
                new BusinessHours
                {
                    Id = Guid.NewGuid(),
                    Day = Day.Saturday,
                    OpeningHour = TimeSpan.Parse("09:00", culture),
                    ClosingHour = TimeSpan.Parse("19:00", culture),
                    AddedDate = DateTime.Now,
                    Establishment = e1
                },
                new BusinessHours
                {
                    Id = Guid.NewGuid(),
                    Day = Day.Sunday,
                    OpeningHour = null, // Closed
                    ClosingHour = null, // Closed
                    AddedDate = DateTime.Now,
                    Establishment = e1
                }
            };
            var bh2 = new List<BusinessHours>
            {
                new BusinessHours
                {
                    Id = Guid.NewGuid(),
                    Day = Day.Monday,
                    OpeningHour = TimeSpan.Parse("08:00", culture),
                    ClosingHour = TimeSpan.Parse("18:00", culture),
                    AddedDate = DateTime.Now,
                    Establishment = e2
                },
                new BusinessHours
                {
                    Id = Guid.NewGuid(),
                    Day = Day.Tuesday,
                    OpeningHour = TimeSpan.Parse("08:00", culture),
                    ClosingHour = TimeSpan.Parse("18:00", culture),
                    AddedDate = DateTime.Now,
                    Establishment = e2
                },
                new BusinessHours
                {
                    Id = Guid.NewGuid(),
                    Day = Day.Wednesday,
                    OpeningHour = TimeSpan.Parse("08:00", culture),
                    ClosingHour = TimeSpan.Parse("18:00", culture),
                    AddedDate = DateTime.Now,
                    Establishment = e2
                },
                new BusinessHours
                {
                    Id = Guid.NewGuid(),
                    Day = Day.Thursday,
                    OpeningHour = TimeSpan.Parse("08:00", culture),
                    ClosingHour = TimeSpan.Parse("18:00", culture),
                    AddedDate = DateTime.Now,
                    Establishment = e2
                },
                new BusinessHours
                {
                    Id = Guid.NewGuid(),
                    Day = Day.Friday,
                    OpeningHour = TimeSpan.Parse("08:00", culture),
                    ClosingHour = TimeSpan.Parse("18:00", culture),
                    AddedDate = DateTime.Now,
                    Establishment = e2
                },
                new BusinessHours
                {
                    Id = Guid.NewGuid(),
                    Day = Day.Saturday,
                    OpeningHour = TimeSpan.Parse("08:00", culture),
                    ClosingHour = TimeSpan.Parse("18:00", Properties.Resources.Culture),
                    AddedDate = DateTime.Now,
                    Establishment = e2
                },
                new BusinessHours
                {
                    Id = Guid.NewGuid(),
                    Day = Day.Sunday,
                    OpeningHour = null, // Closed
                    ClosingHour = null, // Closed
                    AddedDate = DateTime.Now,
                    Establishment = e2
                }
            };

            var n1 = new News
            {
                Id = Guid.NewGuid(),
                Title = "Bar1 News1",
                Content = Properties.Resources.LoremIpsum,
                Banner = new Uri("https://via.placeholder.com/500x80.png?text=Banner+Placeholder"),
                AddedDate = DateTime.Now,
                Establishment = e1
            };
            var n2 = new News
            {
                Id = Guid.NewGuid(),
                Title = "Bar2 News1",
                Content = Properties.Resources.LoremIpsum,
                Banner = new Uri("https://via.placeholder.com/500x80.png?text=Banner+Placeholder"),
                AddedDate = DateTime.Now,
                Establishment = e2
            };

            var p1 = new Picture
            {
                Id = Guid.NewGuid(),
                Location = new Uri("https://loremflickr.com/320/240/bar,nightclub"),
                AddedDate = DateTime.Now,
                Establishment = e1
            };
            var p2 = new Picture
            {
                Id = Guid.NewGuid(),
                Location = new Uri("https://loremflickr.com/320/240/nightclub,bar"),
                AddedDate = DateTime.Now,
                Establishment = e2
            };

            e1.BusinessHours = bh1;
            e2.BusinessHours = bh2;

            e1.Pictures = new List<Picture> { p1 };
            e2.Pictures = new List<Picture> { p2 };

            e1.News = new List<News> { n1 };
            e2.News = new List<News> { n2 };

            context.Establishments.AddRange(new Establishment[] { e1, e2 });
            context.BusinessHours.AddRange(bh1);
            context.BusinessHours.AddRange(bh2);
            context.Pictures.AddRange(new Picture[] { p1, p2 });
            context.News.AddRange(new News[] { n1, n2 });
            context.SaveChanges();

            return host;
        }
    }
}
