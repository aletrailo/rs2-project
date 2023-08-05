using Spaces.Common.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Spaces.Common.Interfaces
{
    public interface ISpaceRepository
    {
        Task<IEnumerable<Space>> GetAllSpaces();
    }
}
