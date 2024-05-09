using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace BookStoreApi.Models
{
    public class UserM
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [BsonElement("UserName")]
        public string UserName { get; set; }

        [BsonElement("Email")]
        public string Email { get; set; }

        [BsonElement("Password")]
        public string Password { get; set; }

        [BsonElement("Status")]
        public string Status { get; set; } = "Registered"; // Registered or Verified.

        [BsonIgnoreIfNull]
        [BsonElement("VerificationCodes")]
        public List<UserVerificationCode>? VerificationCodes { get; set; } // Se van borrando según se usan.

        [BsonIgnoreIfNull]
        [BsonElement("TVCode")]
        public string? TVCode { get; set; }

        [BsonElement("Roles")]
        public List<string> Roles { get; set; } = new List<string>() { "User" }; // Admin, User, etc.

        [BsonElement("Joined")]
        public DateTime? Joined { get; set; } = DateTime.UtcNow; // Cuando se unió.

        [BsonIgnoreIfNull]
        [BsonElement("UpdatedAt")]
        public DateTime? UpdatedAt { get; set; } // Cuando se ha actualizado

        [BsonElement("TwoFactorSecret")]
        public string? TwoFactorSecret { get; set; }  //Codigo al hacer la Auth por QR
    }
}
