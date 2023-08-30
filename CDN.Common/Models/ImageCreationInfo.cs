using System;

namespace CDN.Common.Models
{
    public sealed class ImageCreationInfo
    {
        public Guid BlobId { get; set; }

        public string Blob { get; set; }
    }
}
