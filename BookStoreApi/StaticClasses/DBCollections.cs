using BookStoreApi.Models;
using MongoDB.Driver;

namespace BookStoreApi.StaticClasses
{
    public class DBCollections
    {
        // MONGO DB COLLECTIONS
        private static MongoClient client = new MongoClient("mongodb://localhost:27017");
        public static IMongoDatabase database = client.GetDatabase("ProjectTrackerDB");
        public static IMongoCollection<Client> clientCollection = database.GetCollection<Client>("Client");
        public static IMongoCollection<ProjectModel> projectCollection = database.GetCollection<ProjectModel>("Projects");
        public static IMongoCollection<Book> bookCollection = database.GetCollection<Book>("Libros");
        public static IMongoCollection<CodeModel> codeCollection = database.GetCollection<CodeModel>("Codes");
        public static IMongoCollection<UserM> userCollection = database.GetCollection<UserM>("Users");
    }
}
