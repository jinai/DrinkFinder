using DrinkFinder.Common.Enums;
using DrinkFinder.Infrastructure.Persistence.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace DrinkFinder.Infrastructure.Persistence.Identity
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        [PersonalData]
        public string FirstName { get; set; }
        [PersonalData]
        public string LastName { get; set; }
        [PersonalData]
        public Gender Gender { get; set; }
        [PersonalData]
        public DateTime DateOfBirth { get; set; }
        public bool IsProfessional { get; set; }

        public ICollection<Establishment> Establishments { get; set; }
        public ICollection<News> News { get; set; }
    }
}
