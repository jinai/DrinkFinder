﻿using DrinkFinder.Common.Interfaces;
using System;

namespace DrinkFinder.Infrastructure.Persistence.Entities
{
    public class News : IEntity<Guid>
    {
        public Guid Id { get; set; }
        public DateTime PublicationDate { get; set; }
        public Uri Banner { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime AddedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public bool IsDeleted { get; set; }

        public Establishment Establishment { get; set; }
    }
}
