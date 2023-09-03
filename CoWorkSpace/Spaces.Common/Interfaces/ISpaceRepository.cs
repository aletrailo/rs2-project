using Spaces.Common.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Spaces.Common.Interfaces
{
    public interface ISpaceRepository
    {
        Task<IEnumerable<Space>> GetAllSpaces();
        Task AddSpaceAsync(CreationInfo creationinfo, Guid imageId);
        Task<bool> UpdateSpaceAsync(Space space); 
        Task<bool> DeleteSpaceAsync(string Id);
        Task<Space> GetSpaceByIdAsync(string Id);

        void DeleteAllromDatabase();

    }
}
