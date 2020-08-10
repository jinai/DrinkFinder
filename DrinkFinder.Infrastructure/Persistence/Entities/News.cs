﻿using DrinkFinder.Infrastructure.Persistence.Interfaces;
using System;

namespace DrinkFinder.Infrastructure.Persistence.Entities
{
    public class News : IEntity<Guid>
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public Uri Banner { get; set; }
        public DateTimeOffset AddedDate { get; set; }
        public DateTimeOffset? ModifiedDate { get; set; }
        public bool IsDeleted { get; set; }

        public Guid UserId { get; set; }
        public Establishment Establishment { get; set; }
    }
}
