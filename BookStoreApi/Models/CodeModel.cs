using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace BookStoreApi.Models
{
    public class CodeModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? CodeId { get; set; }

        [BsonElement("TVCode")]
        public string? TVCode { get; set; }

        [BsonElement("ConnectionId")]
        public string? ConnectionId { get; set; }
    }
}
