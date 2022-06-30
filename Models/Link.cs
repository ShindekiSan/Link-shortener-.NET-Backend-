using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using shortid;
using System.Text.Json.Serialization;

namespace MongoBackend.Models;

public class Link {

  [BsonId]
  [BsonRepresentation(BsonType.ObjectId)]
  public string? _id { get; set; }
  public string from { get; set; } = null!;
  public string to { get; set; } = null!;
  public string code { get; set; } = null!;
  public int clicks { get; set; }
  public List<Tag> tags { get; set; } = null!;
  public string description { get; set; } = null!;
  [BsonRepresentation(BsonType.ObjectId)]
  public string owner { get; set; }  = null!;
  public DateTime date { get; set; }
  public int? __v { get; set; }

  public Link(string linkDescription, List<Tag> linkTags, string linkFrom, string ownerId) {
    from = linkFrom;
    description = linkDescription;
    tags = linkTags;
    owner = ownerId;
    date = DateTime.Now;
    __v = 0;
    code = ShortId.Generate();
    to = $"https://localhost:7279/t/{code}";
    clicks = 0;
  }

}
