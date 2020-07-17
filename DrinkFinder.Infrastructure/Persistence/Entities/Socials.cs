using Microsoft.EntityFrameworkCore;
using System;

namespace DrinkFinder.Infrastructure.Persistence.Entities
{
    [Owned]
    public class Socials
    {
        public Uri InstagramUri { get; set; }
        public Uri FacebookUri { get; set; }
        public Uri TwitterUri { get; set; }
        public Uri LinkedInUri { get; set; }
    }
}
