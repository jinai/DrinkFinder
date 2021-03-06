﻿using DrinkFinder.Common.Enums;
using DrinkFinder.MvcClient.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DrinkFinder.MvcClient.Services
{
    public interface IEstablishmentService
    {
        Task<IEnumerable<EstablishmentModel>> GetAll();
        Task<EstablishmentModel> GetById(Guid establishmentId);
        Task<EstablishmentModel> GetByShortCode(string shortCode);
        Task<IEnumerable<EstablishmentModel>> GetAllOpenOn(IsoDay day);
        Task<IEnumerable<EstablishmentModel>> GetAllForUser(Guid userId);
    }
}
