using System;
using Microsoft.AspNetCore.Mvc;
using MongoBackend.Services;
using MongoBackend.Models;

namespace MongoBackend.Controllers;

[Controller]
[Route("api/auth")]
public class UserController: Controller {

  private readonly MongoDBService _mongoDBService;

  public UserController(MongoDBService mongoDBService) {
    _mongoDBService = mongoDBService;
  }

  [HttpGet]
  [Route("get-user/{id}")]
  public async Task<IActionResult> GetUser(string id) {
    var user = await _mongoDBService.GetCurrentUser(id);
    return Ok(new {
      user = new {
        userId = id,
        userName = user[0].userName,
      }
    });
  }

  [HttpPost]
  [Route("login")]
  public async Task<IActionResult> Login([FromBody] Login loginData) {
    try {
      var user = await _mongoDBService.LoginUser(loginData);
      return Ok(new {
        userId = user[0].Id,
        userName = user[0].userName,
      });
    } catch (HttpRequestException exception) {
      return BadRequest(new {
        message = "Something went wrong, try again"
      });
    }
  }

  [HttpPost]
  [Route("register")]
  public async Task<IActionResult> Register([FromBody] Signup signupData) {
    try {
      var result = await _mongoDBService.RegisterUser(signupData);
      if (result as string != null) {
        return BadRequest(new {
          message = result,
        });
      } else {
        var user = result as List<User>;
        return Ok(new {
          userId = user![0].Id,
          userName = user[0].userName,
        });
      }
    } catch (HttpRequestException exception) {
      return BadRequest(new {
        message = "Something went wrong, try again"
      });
    }
  }

}