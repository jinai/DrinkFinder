using DrinkFinder.Infrastructure.Persistence.Context;
using DrinkFinder.Infrastructure.Persistence.Entities;
using System;

namespace DrinkFinder.Infrastructure.Persistence.Repositories
{
    public class TimetableRepository : AsyncRepository<Timetable, Guid>, ITimetableRepository
    {
        public TimetableRepository(DrinkFinderContext context) : base(context)
        {
        }
    }
}
