using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace BookStoreApi.Models
{
    public class ProjectModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [BsonElement("ProjectName")]
        public required string ProjectName { get; set; }

        [BsonElement("Description")]
        public string? Description { get; set; }

        [BsonElement("ApiKey")]
        [BsonIgnoreIfNull]
        public string? ApiKey { get; set; }

        [BsonElement("CreatedAt")]
        public DateTime? CreatedAt { get; set; } = DateTime.UtcNow;

        [BsonIgnoreIfNull]
        [BsonElement("UpdatedAt")]
        public DateTime? UpdatedAt { get; set; }

    }
}
