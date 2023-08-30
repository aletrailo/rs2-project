using CDN.Common.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CDN.Common.Interfaces
{
    public interface IImageRepository
    {
        Task<IEnumerable<Image>> GetAllAsync();

        Task<Image> GetAsync(Guid blobId);

        Task<Image> AddAsync(ImageCreationInfo imageCreationInfo);
    }
}
