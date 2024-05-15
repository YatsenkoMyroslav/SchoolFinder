namespace SchoolFinder.Core
{
    public class ServiceCollectionOptions
    {
        public string DbConnectionString { get; set; } = string.Empty;
        public string BlobStorageConnectionString { get; set; } = string.Empty;
    }
}