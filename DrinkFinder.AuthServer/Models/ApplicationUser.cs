using DrinkFinder.Common.Enums;
using Microsoft.AspNetCore.Identity;
using System;

namespace DrinkFinder.AuthServer.Models
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
        [PersonalData]
        public bool IsProfessional { get; set; }
        public DateTimeOffset RegistrationDate { get; set; }
        public DateTimeOffset? LastLogin { get; set; }
    }
}
