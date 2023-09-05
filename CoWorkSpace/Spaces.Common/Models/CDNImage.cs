using System;

namespace Spaces.Common.Models
{
    public sealed class CDNImage
    {
        public string Id { get; set; }

        public Guid BlobId { get; set; }

        public string Blob { get; set; }
    }
}
