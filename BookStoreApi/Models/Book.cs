﻿using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Text.Json.Serialization;

namespace BookStoreApi.Models
{
    public class Book
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }
        [BsonElement("Name")]
        [JsonPropertyName("Name")]
        public string BookName { get; set; } = null!;
        [BsonElement("TotalPrice")]
        [JsonPropertyName("TotalPrice")]
        public decimal Price { get; set; }
        [BsonElement("Worlds")]
        [JsonPropertyName("Worlds")]
        public string Category { get; set; } = null!;
        public string Author { get; set; } = null!;
    }
}