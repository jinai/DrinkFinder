using DrinkFinder.Common.Extensions;
using DrinkFinder.Infrastructure.Persistence.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace DrinkFinder.Infrastructure.ShortCode
{
    public class ShortCodeService : IShortCodeService
    {
        public const string AllowedCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-_";
        public const int MaxSize = 20;
        public const int MinSize = 3;
        private readonly IUnitOfWork _unitOfWork;
        private const int GenerationTimeout = 5000; // Milliseconds

        public ShortCodeService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public static bool IsValid(string shortCode)
        {
            if (shortCode is null || shortCode.Length < MinSize || shortCode.Length > MaxSize)
            {
                return false;
            }

            return shortCode.All(c => AllowedCharacters.Contains(c, StringComparison.Ordinal));
        }

        public async Task<bool> IsAvailable(string shortCode)
        {
            var establishment = await _unitOfWork.EstablishmentRepo.GetWhere(e => e.ShortCode == shortCode)
                .SingleOrDefaultAsync();
            return establishment == null;
        }

        public async Task<string> NewShortCode()
        {
            var timeout = TimeSpan.FromMilliseconds(GenerationTimeout);
            var task = TryGenerateShortCode();
            string shortCode;

            try
            {
                shortCode = await task.TimeoutAfter(timeout);
            }
            catch (TimeoutException)
            {
                // If we're here there are 2 options:
                //   1) We ran out of valid short codes. Given the current MaxSize this is unlikely.
                //   2) We ran out of time to find a short code that's not already taken. Although more likely,
                //      it shouldn't happen before a very long time, at which point we should change the implementation.
                throw new ShortCodeServiceException("Timed out while trying to generate a short code.");
            }

            return shortCode;
        }

        private async Task<string> TryGenerateShortCode()
        {
            var buffer = new char[MaxSize];
            var random = new Random();
            string shortCode;

            while (true)
            {
                for (var i = 0; i < buffer.Length; i++)
                {
                    buffer[i] = AllowedCharacters[random.Next(AllowedCharacters.Length)];
                }

                var candidate = new string(buffer);
                var available = await IsAvailable(candidate);

                if (!available) continue;

                shortCode = candidate;
                break;
            }

            return shortCode;
        }
    }
}
