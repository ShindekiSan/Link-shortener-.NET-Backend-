using System;
using Microsoft.AspNetCore.Mvc;
using MongoBackend.Services;
using MongoBackend.Models;

namespace MongoBackend.Controllers;

[Controller]
[Route("t")]
public class RedirectController: Controller {

  private readonly MongoDBService _mongoDBService;

  public RedirectController(MongoDBService mongoDBService) {
    _mongoDBService = mongoDBService;
  }

  [HttpGet]
  [Route("{code}")]
  public ActionResult RedirectOnLink(string code) {
    var redirectLink = _mongoDBService.RedirectOnShortenedLink(code);
    return Redirect(redirectLink.Result);
  }

}