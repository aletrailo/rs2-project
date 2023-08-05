namespace Spaces.Persistance.Helpers
{
    public class SpacesDbSettings
    {
        public string ConnectionString { get; set; } = null!;

        public string DatabaseName { get; set; } = null!;

        public string SpacesCollectionName { get; set; } = null!;
    }
}
