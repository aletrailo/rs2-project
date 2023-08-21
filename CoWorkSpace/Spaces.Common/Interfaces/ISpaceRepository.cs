using Spaces.Common.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Spaces.Common.Interfaces
{
    public interface ISpaceRepository
    {
        Task<IEnumerable<Space>> GetAllSpaces();
        Task InsertSpace(CreationInfo creationinfo);
        Task<bool> UpdateSpace(CreationInfo creationinfo); 
        Task<bool> DeleteSpace(string Id);
        Task<Space> GetSpaceById(string Id);
    }
}
