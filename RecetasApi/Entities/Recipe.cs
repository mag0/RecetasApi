using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace RecetasApi.Entities;

public class Recipe
{
    [BsonId]
    [BsonElement("_id"), BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }

    [BsonElement("title"), BsonRepresentation(BsonType.String)]
    public string Title { get; set; }

    [BsonElement("description")]
    public List<string> Description { get; set; }
}

