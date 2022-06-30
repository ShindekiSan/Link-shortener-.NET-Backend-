using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MongoBackend.Models;

public class Tag {
  public string tagName { get; set; } = null!;

}