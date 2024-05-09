using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ProjectTracker.SignalR.Models
{
    public class StatusConnectionSignalR
    {

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }


        [BsonElement("IdUser")]
        public string? IdUser { get; set; }

        [BsonElement("ConnectionId")]
        public string? ConnectionId { get; set; }

        [BsonElement("Connected")]
        public bool? IsConnected { get; set; }

        [BsonElement("Created")]
        public DateTime Created { get; set; } = DateTime.UtcNow;
    }
}
