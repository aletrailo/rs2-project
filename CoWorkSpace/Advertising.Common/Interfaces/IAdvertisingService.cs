using Advertising.Common.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Advertising.Common.Interfaces
{
    public interface IAdvertisingService
    {
        Task<IEnumerable<AdSpace>> GetAllAsync();
        Task<bool> AddAsync(AdSpaceInfo adSpaceInfo);
    }
}
