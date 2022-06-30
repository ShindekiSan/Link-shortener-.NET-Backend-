using MongoBackend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Web;
using MongoDB.Driver;
using MongoDB.Bson;

namespace MongoBackend.Services;

public class MongoDBService {

  private readonly IMongoCollection<Link> _linksCollection;
  private readonly IMongoCollection<User> _usersCollection;

  public MongoDBService(IOptions<MongoDBSettings> mongoDBSettings) {
    MongoClient client = new MongoClient(mongoDBSettings.Value.ConnectionURI);
    IMongoDatabase database = client.GetDatabase(mongoDBSettings.Value.DatabaseName);
    _linksCollection = database.GetCollection<Link>(mongoDBSettings.Value.CollectionsName[0]);
    _usersCollection = database.GetCollection<User>(mongoDBSettings.Value.CollectionsName[1]);
  }

  public async Task GenerateLink (NewLink newLink) {
    Link generateLink = new Link(newLink.description, newLink.tags, newLink.from, newLink.ownerId);
    await _linksCollection.InsertOneAsync(generateLink);
    return;
  }

    public async Task<Link> EditLink (UpdateLink updateLink) {
    FilterDefinition<Link> filter = Builders<Link>.Filter.Eq("code", updateLink.code);
    UpdateDefinition<Link> update = Builders<Link>.Update.Set("description", updateLink.description).Set("tags", updateLink.tags);
    return await _linksCollection.FindOneAndUpdateAsync(filter, update, options: new FindOneAndUpdateOptions<Link, Link> {
      IsUpsert = true,
      ReturnDocument = ReturnDocument.After,
    });
  }

  public async Task<List<Link>> GetUserLinks(string userId) {
    return await _linksCollection.Find(link => link.owner == userId).ToListAsync();
  } 

  public async Task<List<Link>> GetUserLink(string userId, string linkId) {
    return await _linksCollection.Find(link => link.owner == userId && link._id == linkId).ToListAsync();
  } 

  public async Task<List<Link>> GetSearchedLinks(string searchTag) {
    FilterDefinition<Link> filter = Builders<Link>.Filter.ElemMatch(link => link.tags, Builders<MongoBackend.Models.Tag>.Filter.Eq(tag => tag.tagName, searchTag));
    return await _linksCollection.Find(filter).ToListAsync();
  }

  public async Task<List<Link>> GetSearchedLink(string linkId) {
    return await _linksCollection.Find(link => link._id == linkId).ToListAsync();
  }

  public async Task<string> RedirectOnShortenedLink(string code) {
    var links = await _linksCollection.Find(link => link.code == code).ToListAsync();
    FilterDefinition<Link> filter = Builders<Link>.Filter.Eq("code", code);
    if (links[0] != null) {
      UpdateDefinition<Link> update = Builders<Link>.Update.Set("clicks", links[0].clicks + 1);
      await _linksCollection.UpdateOneAsync(filter, update);
      return links[0].from;
    }
    return "";
  }

  public async Task<List<User>> GetCurrentUser (string userId) {
    return await _usersCollection.Find(user => user.Id == userId).ToListAsync();
  }

  public async Task<List<User>> LoginUser (Login loginData) {
    return await _usersCollection.Find(user => user.password == loginData.password && user.email == loginData.email).ToListAsync();
  }

  public async Task<object> RegisterUser (Signup signupData) {
    var userNameMatch = await _usersCollection.Find(user => user.userName == signupData.userName).ToListAsync();
    var emailMatch = await _usersCollection.Find(user => user.email == signupData.email).ToListAsync();
    if (userNameMatch.Count != 0) {
      return "That username is unavaliable";
    } else if (emailMatch.Count != 0) {
      return "That email is unavaliable";
    } else {
      await _usersCollection.InsertOneAsync(new User(signupData.email, signupData.password, signupData.userName));
      return await _usersCollection.Find(user => user.userName == signupData.userName).ToListAsync();
    }
  }
}