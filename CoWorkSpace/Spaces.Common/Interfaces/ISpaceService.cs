using Spaces.Common.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Spaces.Common.Interfaces
{
    public interface ISpaceService
    {
        Task<IEnumerable<Space>> GetAllAsync();
        Task<bool> DeleteAsync(string Id);
        Task<Space> GetByIdAsync(string Id);
        Task InsertAsync(Space space);
        Task<bool> UpdateAsync(Space space);
    }
}
