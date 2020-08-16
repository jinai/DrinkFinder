using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DrinkFinder.Infrastructure.ShortCode
{
    public interface IShortCodeService
    {
        Task<bool> IsAvailable(string shortCode);
        Task<string> NewShortCode();
    }
}
