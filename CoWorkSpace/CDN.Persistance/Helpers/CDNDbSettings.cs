namespace CDN.Persistance.Helpers
{
    public class CDNDbSettings
    {
        public string ConnectionString { get; set; } = null!;

        public string DatabaseName { get; set; } = null!;

        public string CDNCollectionName { get; set; } = null!;
    }
}
