using Spaces.Common.Models;
using System;
using System.Threading.Tasks;

namespace Spaces.Common.Interfaces
{
    public interface ICDNImageService
    {
        Task<CDNImage> GetAsync(Guid id);

        Task AddAsync(CDNImageCreationInfo cdnImageCreationInfo);
    }
}
