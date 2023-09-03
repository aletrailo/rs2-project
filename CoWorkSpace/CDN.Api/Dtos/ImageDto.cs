namespace CDN.Api.Dtos
{
    public sealed class ImageDto
    {
        public string Id { get; set; }

        public Guid BlobId { get; set; }

        public string Blob { get; set; }
    }
}
