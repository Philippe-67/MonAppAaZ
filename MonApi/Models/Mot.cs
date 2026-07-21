using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MonApi.Models;

public class Mot
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }
    public string MotFr { get; set; } = null!;
    public string MotEn { get; set; } = null!;
}
