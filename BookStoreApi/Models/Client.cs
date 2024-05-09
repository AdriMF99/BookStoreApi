using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Text.Json.Serialization;

namespace BookStoreApi.Models
{
    public class Client
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [BsonElement("Nombre")]
        [JsonPropertyName("Nombre")]
        public string? Namecillo { get; set; } = null!;

        /*[BsonElement("Books")]
        [JsonPropertyName("Books")]
        public Book[]? Libros { get; set; }*/
    }
}
