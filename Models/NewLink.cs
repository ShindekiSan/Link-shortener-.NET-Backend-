using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MongoBackend.Models;

public class NewLink {

  [BsonRepresentation(BsonType.ObjectId)]
  public string ownerId { get; set; } = null!;
  public string description { get; set; } = null!;
  public List<Tag> tags { get; set; } = null!;
  public string from { get; set; } = null!;

}