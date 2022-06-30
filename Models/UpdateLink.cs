using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MongoBackend.Models;

public class UpdateLink {

  public string code { get; set; } = null!;
  public string description { get; set; } = null!;
  public List<Tag> tags { get; set; } = null!;

}