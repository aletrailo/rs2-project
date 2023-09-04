using CDN.Common.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CDN.Common.Interfaces
{
    public interface IImageService
    {
        Task<IEnumerable<Image>> GetAllAsync();

        Task<Image> GetAsync(Guid blobId);

        Task<Image> AddAsync(ImageCreationInfo imageCreationInfo);

        Task UpdateAsync(Image image);

        Task DeleteAsync(Guid blobId);

        void DeleteAllFromDatabase();
    }
}
