namespace BookStoreApi.Models
{
    public class ClientsStoreDatabaseSettings
    {
        public string ConnectionString { get; set; } = null!;
        public string DatabaseName { get; set; } = null!;
        public string ClientsCollectionName { get; set; } = null!;
    }
}
