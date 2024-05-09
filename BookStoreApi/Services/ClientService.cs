using BookStoreApi.Models;
using BookStoreApi.StaticClasses;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace BookStoreApi.Services
{
    public class ClientService
    {
        private readonly IMongoCollection<Client>? _clientsCollection;

        public ClientService(IOptions<ClientsStoreDatabaseSettings> clientStoreDatabaseSettings)
        {
            var mongoClient = new MongoClient(clientStoreDatabaseSettings.Value.ConnectionString);
            var mongoDatabase = mongoClient.GetDatabase(clientStoreDatabaseSettings.Value.DatabaseName);
            _clientsCollection = mongoDatabase.GetCollection<Client>(clientStoreDatabaseSettings.Value.ClientsCollectionName);
        }

        public async Task<List<Client>> GetAsync() =>
            await _clientsCollection.Find(_ => true).ToListAsync();

        public async Task<Client?> GetAsync(string id) =>
            await _clientsCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

        public async Task CreateAsync(Client newClient) =>
            await DBCollections.clientCollection.InsertOneAsync(newClient);

        public async Task UpdateAsync(string id, Client updatedClient) =>
            await _clientsCollection.ReplaceOneAsync(x => x.Id == id, updatedClient);

        public async Task RemoveAsync(string id) =>
            await _clientsCollection.DeleteOneAsync(x => x.Id == id);
    }
}
