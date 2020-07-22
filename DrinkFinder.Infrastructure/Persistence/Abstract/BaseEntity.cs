using DrinkFinder.Common.Interfaces;
using System;

namespace DrinkFinder.Infrastructure.Persistence.Abstract
{
    public abstract class BaseEntity : IEntity<Guid>
    {
        public Guid Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? LastEditedAt { get; set; }
        public bool SoftDeleted { get; set; }
    }
}
