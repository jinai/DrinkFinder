using DrinkFinder.Infrastructure.Persistence.Context;
using DrinkFinder.Infrastructure.Persistence.Entities;
using System;

namespace DrinkFinder.Infrastructure.Persistence.Repositories
{
    public class EstablishmentRepository : AsyncRepository<Establishment, Guid>, IEstablishmentRepository
    {
        public EstablishmentRepository(DrinkFinderContext context) : base(context)
        {
        }
    }
}
