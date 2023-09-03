using System;

namespace CDN.Common.Models
{
    public sealed class Image
    {
        public string Id { get; set; }

        public Guid BlobId { get; set; }

        public string Blob { get; set; }
    }
}
