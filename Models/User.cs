using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Text.Json.Serialization;

namespace MongoBackend.Models;

public class User {

  [BsonId]
  [BsonRepresentation(BsonType.ObjectId)]
  public string? Id { get; set; }
  public string userName { get; set; } = null!;
  public string email { get; set; } = null!;
  public string password { get; set; } = null!;
  public int? __v { get; set; }
  public List<string> links { get; set; } = null!;

  public User(string userEmail, string userPassword, string name) {
    userName = name;
    email = userEmail;
    password = userPassword;
    __v = 0;
    links = new List<string>(0);
  }

}
