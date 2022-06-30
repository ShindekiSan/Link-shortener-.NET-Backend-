using System;
using Microsoft.AspNetCore.Mvc;
using MongoBackend.Services;
using MongoBackend.Models;

namespace MongoBackend.Controllers;

[Controller]
[Route("api/link")]
public class LinksController: Controller {

  private readonly MongoDBService _mongoDBService;

  public LinksController(MongoDBService mongoDBService) {
    _mongoDBService = mongoDBService;
  }

  [HttpPost]
  [Route("generate")]
  public async Task<IActionResult> GenerateLink([FromBody] NewLink newLink) {
    try {
      await _mongoDBService.GenerateLink(newLink);
      return Ok(new {
        message = "Your link has been shortened successfully! Check profile"
      });
    } catch (HttpRequestException exception) {
      return BadRequest(new {
        message = "Something went wrong, try again",
        error = exception.Data,
      });
    }
  }

  [HttpPost]
  [Route("edit")]
  public async Task<IActionResult> EditLink([FromBody] UpdateLink updateLink) {
    try {
      var newLink = await _mongoDBService.EditLink(updateLink);
      return Ok(new {
        message = "Link has been successfully edited!",
        link = newLink
      });
    } catch (HttpRequestException exception) {
      return BadRequest(new {
        message = "Something went wrong, try again",
        error = exception.Data,
      });
    }
  }

  [HttpGet]
  [Route("")]
  public async Task<List<Link>> GetUserLinks([FromQuery] string userId) {
    return await _mongoDBService.GetUserLinks(userId);
  }

  [HttpGet]
  [Route("{linkId}")]
  public async Task<List<Link>> GetUserLink([FromQuery] string userId, string linkId) {
    return await _mongoDBService.GetUserLink(userId, linkId);
  }

  [HttpGet]
  [Route("search/{searchTag}")]
  public async Task<List<Link>> GetSearchedLinks(string searchTag) {
    return await _mongoDBService.GetSearchedLinks(searchTag);
  }

  [HttpGet]
  [Route("link-info/{linkId}")]
  public async Task<List<Link>> GetSearchedLink(string linkId) {
    return await _mongoDBService.GetSearchedLink(linkId);
  }

}