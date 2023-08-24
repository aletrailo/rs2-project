using Spaces.Common.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Spaces.Common.Interfaces
{
    public interface ISpaceRepository
    {
        Task<IEnumerable<Space>> GetAllSpaces();
        Task AddSpaceAsync(CreationInfo creationinfo);
        Task<bool> UpdateSpaceAsync(CreationInfo creationinfo); 
        Task<bool> DeleteSpaceAsync(string Id);
        Task<Space> GetSpaceByIdAsync(string Id);
    }
}
