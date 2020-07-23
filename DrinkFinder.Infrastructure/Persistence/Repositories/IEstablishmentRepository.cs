﻿using DrinkFinder.Common.Interfaces;
using DrinkFinder.Infrastructure.Persistence.Entities;
using System;

namespace DrinkFinder.Infrastructure.Persistence.Repositories
{
    public interface IEstablishmentRepository : IAsyncRepository<Establishment, Guid>
    {
    }
}
