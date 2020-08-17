﻿using DrinkFinder.Common.Extensions;
using DrinkFinder.Infrastructure.Persistence.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace DrinkFinder.Infrastructure.ShortCode
{
    public class ShortCodeService : IShortCodeService
    {
        public const string AllowedCharacters = "abcdefghijklmnopqrstuvwxyz0123456789-_";
        public const int MaxSize = 20;
        public const int MinSize = 3;
        private readonly IUnitOfWork _unitOfWork;
        private const int _generationTimeout = 5000; // Milliseconds

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

            foreach (char c in shortCode)
            {
                if (!AllowedCharacters.Contains(c, StringComparison.Ordinal))
                {
                    return false;
                }
            }

            return true;
        }

        public async Task<bool> IsAvailable(string shortCode)
        {
            var establishment = await _unitOfWork.EstablishmentRepo.GetWhere(e => e.ShortCode == shortCode).SingleOrDefaultAsync();
            return establishment == null;
        }

        public async Task<string> NewShortCode()
        {
            var timeout = TimeSpan.FromMilliseconds(_generationTimeout);
            var task = TryGenerateShortCode();
            string shortCode;

            try
            {
                shortCode = await task.TimeoutAfter(timeout);
            }
            catch (TimeoutException)
            {
                // If we're here there are 2 options:
                // 1) We ran out of valid short codes. Given the current maxSize this is unlikely.
                // 2) We ran out of time to find a short code that's not already taken. This is more likely and means we should probably change the implementation.
                throw;
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
                for (int i = 0; i < buffer.Length; i++)
                {
                    buffer[i] = AllowedCharacters[random.Next(AllowedCharacters.Length)];
                }

                var candidate = new String(buffer);
                var available = await IsAvailable(candidate);
                if (available)
                {
                    shortCode = candidate;
                    break;
                }
            }

            return shortCode;
        }
    }
}