using MongoDB.Bson.Serialization.Attributes;
using System.Text.Json.Serialization;

namespace BookStoreApi.Models
{
    public class UserVerificationCode
    {
        [JsonIgnore]
        [BsonElement("Verification")]
        public string? Verification { get; set; } = "Account";

        [BsonElement("VerificationCode")]
        public int? VerificationCode { get; set; }

        [JsonIgnore]
        [BsonElement("Created")]
        public DateTime? Created { get; set; } = DateTime.Now;

        [JsonIgnore]
        [BsonElement("Expires")]
        public DateTime Expires { get; set; } = DateTime.UtcNow.AddMinutes(10);
    }
}
