﻿using Spaces.Common.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Spaces.Common.Interfaces
{
    public interface ISpaceService
    {
        Task<IEnumerable<Space>> GetAllAsync();
        Task<bool> DeleteAsync(string Id);
        Task<Space> GetByIdAsync(string Id);
        Task AddAsync(CreationInfo creationInfo, Guid imageId);
        Task<bool> UpdateAsync(Space space);


        void DeleteAllFromDatabase();
    }
}
