﻿using System;

namespace DrinkFinder.Api.Models
{
    public class NewsDto
    {
        public Guid Id { get; set; }
        public DateTimeOffset PublicationDate { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public Uri Banner { get; set; }

        public Guid EstablishmentId { get; set; }
    }
}
