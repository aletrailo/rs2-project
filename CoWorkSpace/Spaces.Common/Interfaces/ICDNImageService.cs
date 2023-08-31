using Spaces.Common.Models;
using System;

namespace Spaces.Common.Interfaces
{
    public interface ICDNImageService
    {
        CDNImage GetAsync(Guid id);

        void AddAsync(CDNImageCreationInfo cdnImageCreationInfo);
    }
}
